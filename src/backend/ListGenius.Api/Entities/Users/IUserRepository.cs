namespace ListGenius.Api.Entities.Users
{
    public interface IUserRepository
    {
        Task<ApplicationUser> FindByFullNameAsync(string fullName);
    }
}
