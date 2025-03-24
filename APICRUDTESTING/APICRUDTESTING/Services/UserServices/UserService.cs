using APICRUDTESTING.Data;
using APICRUDTESTING.Models;
using Microsoft.EntityFrameworkCore;

namespace APICRUDTESTING.Services.UserServices
{
    public class UserService : IUserService
    {
        private readonly DataContext _context;
        public UserService(DataContext context)
        {
            _context = context;
        }

        public async Task<string> CekUsername(string username)
        {
            var product = await _context.Users.FirstOrDefaultAsync(u => u.Username == username);
            if (product != null)
                return "username already exist";

            return null;
        }

        public async Task AddUser(User user)
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
        }

        public async Task<User?> GetUser(UserDto user)
        {
            var product = await _context.Users.FirstOrDefaultAsync(u => u.Username == user.Username);
            if (product == null)
                return null;

            return product;
        }
    }
}
