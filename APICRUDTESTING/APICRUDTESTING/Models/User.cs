using System.ComponentModel.DataAnnotations;

namespace APICRUDTESTING.Models
{
    public class User
    {
        [Key] // Menandai Username sebagai Primary Key
        public string Username { get; set; }

        public string PasswordHash { get; set; }
    }
}
