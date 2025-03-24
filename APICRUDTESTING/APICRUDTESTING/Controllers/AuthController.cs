using APICRUDTESTING.Models;
using APICRUDTESTING.Services.UserServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace APICRUDTESTING.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        public static User user = new User();
        private readonly IConfiguration _configuration;
        private readonly IUserService _userService;

        public AuthController(IConfiguration configuration, IUserService userService)
        {
            _configuration = configuration;
            _userService = userService;
        }

        // Register User
        [HttpPost("register")]
        public async Task<ActionResult<User>> Register(UserDto request)
        {
            // Cek apakah username sudah ada
            string existingUser = await _userService.CekUsername(request.Username);
            if (existingUser != null)
                return BadRequest(existingUser);

            // Enkripsi password
            string passwordHash = BCrypt.Net.BCrypt.HashPassword(request.Password);

            // Buat user baru
            var newUser = new User
            {
                Username = request.Username,
                PasswordHash = passwordHash
            };

            // Simpan user ke dalam database
            await _userService.AddUser(newUser);

            return Ok(newUser);
        }

        [HttpPost("login")]
        public async Task<ActionResult<string>> Login(UserDto request)
        {
            var user = await _userService.GetUser(request);

            if (user == null)
            {
                return BadRequest("User not found.");
            }

            if (!BCrypt.Net.BCrypt.Verify(request.Password, user.PasswordHash))
            {
                return BadRequest("Wrong password.");
            }

            string token = CreateToken(user);

            return Ok(token);
        }

        // Generate JWT Token
        private string CreateToken(User user)
        {
            List<Claim> claims = new List<Claim> {
                new Claim(ClaimTypes.Name, user.Username),
                new Claim(ClaimTypes.Role, "Admin"),
                new Claim(ClaimTypes.Role, "User"),
            };

            // Ensure the key is at least 64 bytes (512 bits) long
            var keyString = _configuration.GetSection("AppSettings:Token").Value!;
            byte[] keyBytes = Encoding.UTF8.GetBytes(keyString);

            // If the key is too short, pad it to be at least 64 bytes
            if (keyBytes.Length < 64)
            {
                Array.Resize(ref keyBytes, 64); // Resize to 64 bytes (512 bits)
            }

            var key = new SymmetricSecurityKey(keyBytes);

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var token = new JwtSecurityToken(
                    claims: claims,
                    expires: DateTime.Now.AddDays(1),
                    signingCredentials: creds
                );

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);

            return jwt;
        }

    }
}
