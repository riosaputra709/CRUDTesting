using APICRUDTESTING.Models;

namespace APICRUDTESTING.Services.UserServices
{
    public interface IUserService
    {
        Task AddUser(User newUser);
        Task<string> CekUsername(string username);
        Task<User> GetUser(UserDto request);
    }
}
