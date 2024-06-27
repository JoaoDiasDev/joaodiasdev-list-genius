namespace ServerLibrary.Repositories.Implementations;

public class UserAccountRepository(IOptions<JwtSection> configJwt,
    AppDbContext appDbContext) : IUserAccountRepository
{
    private const string AuthUrl = "api/authentication";
    public async Task<GeneralResponse> CreateAsync(RegisterDto user)
    {
        if (user is null)
        {
            return new GeneralResponse(false, "Model is empty");
        }

        var checkUser = await FindUserByEmail(user.Email!);
        if (checkUser is not null)
        {
            return new GeneralResponse(false, "User registered already");
        }

        // Save user to db
        var applicationUser = await AddToDatabase(new ApplicationUser()
        {
            FullName = user.FullName,
            Email = user.Email,
            Password = BCrypt.Net.BCrypt.HashPassword(user.Password)
        });

        //check,create and assign role
        var checkAdminRole = await appDbContext.SystemRoles.FirstOrDefaultAsync(r =>
            r.Name!.Equals(Constants.Admin));
        if (checkAdminRole is null)
        {
            var createAdminRole = await AddToDatabase(new SystemRole() { Name = Constants.Admin });
            await AddToDatabase(new UserRole() { RoleId = createAdminRole.Id, UserId = applicationUser.Id });
            return new GeneralResponse(true, "Account created!");
        }

        var checkUserRole = await appDbContext.SystemRoles.FirstOrDefaultAsync(r =>
            r.Name!.Equals(Constants.User));
        if (checkUserRole is null)
        {
            var response = await AddToDatabase(new SystemRole() { Name = Constants.User });
            await AddToDatabase(new UserRole() { RoleId = response.Id, UserId = applicationUser.Id });
        }
        else
        {
            await AddToDatabase(new UserRole() { RoleId = checkUserRole.Id, UserId = applicationUser.Id });
        }

        return new GeneralResponse(true, "Account created!");
    }

    private async Task<T> AddToDatabase<T>(T model)
    {
        var result = appDbContext.Add(model!);
        await appDbContext.SaveChangesAsync();
        return (T)result.Entity;
    }

    private async Task<ApplicationUser> FindUserByEmail(string email)
    {
        return (await appDbContext.ApplicationUsers.FirstOrDefaultAsync(u => u.Email!.ToLower()!.Equals(email!)))!;
    }

    public async Task<LoginResponse> SignInAsync(LoginDto user)
    {
        if (user is null)
        {
            return new LoginResponse(false, "Model is empty");
        }

        var applicationUser = await FindUserByEmail(user.Email!);
        if (applicationUser is null)
        {
            return new LoginResponse(false, "User not found");
        }

        // Verify password
        if (!BCrypt.Net.BCrypt.Verify(user.Password, applicationUser.Password))
        {
            return new LoginResponse(false, "Email/Password not valid");
        }

        var getUserRole = await FindUserRole(applicationUser.Id);
        if (getUserRole is null)
        {
            return new LoginResponse(false, "User role not found");
        }

        var getRoleName = await FindRoleName(getUserRole.RoleId);
        if (getRoleName is null)
        {
            return new LoginResponse(false, "User role not found");
        }

        var jwtToken = GenerateToken(applicationUser, getRoleName!.Name!);
        var refreshToken = GenerateRefreshToken();

        //Save the refresh token to database
        var findUser =
            await appDbContext.RefreshTokenInfos.FirstOrDefaultAsync(rti => rti.UserId.Equals(applicationUser.Id));
        if (findUser is not null)
        {
            findUser!.RefreshToken = refreshToken;
            await appDbContext.SaveChangesAsync();
        }
        else
        {
            await AddToDatabase(new RefreshTokenInfo() { RefreshToken = refreshToken, UserId = applicationUser.Id });
        }

        return new LoginResponse(true, "Login successfully", jwtToken, refreshToken);
    }

    private async Task<UserRole> FindUserRole(int userId)
    {
        return (await appDbContext.UserRoles.FirstOrDefaultAsync(ur => ur.UserId.Equals(userId)))!;
    }

    private async Task<SystemRole> FindRoleName(int roleId)
    {
        return (await appDbContext.SystemRoles.FirstOrDefaultAsync(ur => ur.Id.Equals(roleId)))!;
    }

    public async Task<LoginResponse> RefreshTokenAsync(RefreshTokenDto token)
    {
        if (token is null) return new LoginResponse(false, "Model is empty");

        var findToken = await appDbContext.RefreshTokenInfos.FirstOrDefaultAsync(rti => rti.RefreshToken!.Equals(token.RefreshToken));
        if (findToken is null) return new LoginResponse(false, "Refresh token is required");

        //Get user details
        var user = await appDbContext.ApplicationUsers.FirstOrDefaultAsync(au => au.Id.Equals(findToken.UserId));
        if (user is null)
            return new LoginResponse(false, "Refresh token could not be generated because user not found");

        var userRole = await FindUserRole(user.Id);
        var roleName = await FindRoleName(userRole.RoleId);
        var jwtToken = GenerateToken(user, roleName.Name!);
        var refreshToken = GenerateRefreshToken();

        var updateRefreshToken =
            await appDbContext.RefreshTokenInfos.FirstOrDefaultAsync(rti => rti.UserId.Equals(user.Id));
        if (updateRefreshToken is null)
            return new LoginResponse(false, "Refresh token could not be generated because user has not signed in");

        updateRefreshToken.RefreshToken = refreshToken;
        await appDbContext.SaveChangesAsync();
        return new LoginResponse(true, "RefreshToken refreshed successfully", jwtToken, refreshToken);

    }

    private static string GenerateRefreshToken()
    {
        return Convert.ToBase64String(RandomNumberGenerator.GetBytes(64));
    }

    private string GenerateToken(ApplicationUser applicationUser, string role)
    {
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configJwt.Value.Key!));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
        var userClaims = new[]
        {
            new Claim(ClaimTypes.NameIdentifier, applicationUser.Id.ToString()),
            new Claim(ClaimTypes.Name, applicationUser.FullName!),
            new Claim(ClaimTypes.Email, applicationUser.Email!),
            new Claim(ClaimTypes.Role, role),
        };

        var token = new JwtSecurityToken(
            issuer: configJwt.Value.Issuer,
            audience: configJwt.Value.Audience,
            claims: userClaims,
            expires: DateTime.Now.AddDays(7),
            signingCredentials: credentials
        );
        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    public async Task<List<ManageUserDto>> GetUsers()
    {
        var allUsers = await GetApplicationUsers();
        var allUsersRoles = await UserRoles();
        var allRoles = await SystemRoles();

        if (allUsers.Count == 0 || allRoles.Count == 0)
        {
            throw new ArgumentNullException(nameof(ManageUserDto), "No users or roles found");
        }

        var users = new List<ManageUserDto>();
        foreach (var user in allUsers)
        {
            var userRole = allUsersRoles.FirstOrDefault(u => u.UserId == user.Id);
            var roleName = allRoles.FirstOrDefault(r => r.Id == userRole!.RoleId);
            users.Add(new ManageUserDto
            {
                UserId = user.Id,
                Name = user.FullName!,
                Email = user.Email!,
                Role = roleName!.Name!
            }
            );
        }

        return users;
    }

    public async Task<GeneralResponse> UpdateUser(ManageUserDto user)
    {
        var getRole = (await GetRoles()).FirstOrDefault(r => r.Name!.Equals(user.Role));
        var userRole = await appDbContext.UserRoles.FirstOrDefaultAsync(u => u.UserId == user.UserId);
        userRole!.RoleId = getRole!.Id;
        await appDbContext.SaveChangesAsync();
        return new GeneralResponse(true, "User role updated successfully");
    }

    public async Task<List<SystemRole>> GetRoles() => await SystemRoles();

    public async Task<GeneralResponse> DeleteUser(int id)
    {
        var user = await appDbContext.ApplicationUsers.FirstOrDefaultAsync(u => u.Id == id);
        appDbContext.ApplicationUsers.Remove(user!);
        await appDbContext.SaveChangesAsync();
        return new GeneralResponse(true, "User successfully deleted!");
    }
    private async Task<List<SystemRole>> SystemRoles() => await appDbContext.SystemRoles.AsNoTracking().ToListAsync();
    private async Task<List<UserRole>> UserRoles() => await appDbContext.UserRoles.AsNoTracking().ToListAsync();
    private async Task<List<ApplicationUser>> GetApplicationUsers() => await appDbContext.ApplicationUsers.AsNoTracking().ToListAsync();
    public async Task<string> GetUserImage(int id) => (await GetApplicationUsers()).FirstOrDefault(u => u.Id.Equals(id))!.Image!;
    public async Task<bool> UpdateProfile(UserProfileDto profile)
    {
        var user = await appDbContext.ApplicationUsers.FirstOrDefaultAsync(u => u.Id.Equals(int.Parse(profile.Id)));
        user!.Email = profile.Email;
        user.FullName = profile.Name;
        user.Image = profile.Image;
        await appDbContext.SaveChangesAsync();
        return true;
    }
}