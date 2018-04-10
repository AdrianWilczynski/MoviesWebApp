using System.ComponentModel.DataAnnotations;

namespace MoviesWebApp.Models
{
    public class User
    {
        public int UserId { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }
        public string PasswordHash { get; set; }
    }
}
