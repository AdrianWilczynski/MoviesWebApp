using System.ComponentModel.DataAnnotations;

namespace MoviesWebApp.Models
{
    public class User
    {
        public string UserName { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
    }
}
