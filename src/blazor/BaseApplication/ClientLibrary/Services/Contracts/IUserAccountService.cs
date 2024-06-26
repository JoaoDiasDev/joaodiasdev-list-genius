﻿using BaseLibrary.Entities;

namespace ClientLibrary.Services.Contracts;

public interface IUserAccountService
{
    Task<GeneralResponse> CreateAsync(RegisterDto user);
    Task<LoginResponse> SignInAsync(LoginDto user);
    Task<LoginResponse> RefreshTokenAsync(RefreshTokenDto token);
    Task<List<ManageUserDto>> GetUsers();
    Task<GeneralResponse> UpdateUser(ManageUserDto user);
    Task<List<SystemRole>> GetRoles();
    Task<GeneralResponse> DeleteUser(int id);
}