using System.ComponentModel.DataAnnotations;

namespace MoviesWebApp.ViewModels
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Adres e-mail jest wymagany.")]
        [DataType(DataType.EmailAddress, ErrorMessage = "Musisz podać poprawny adres e-mail.")]
        [MaxLength(255, ErrorMessage = "Adres e-mail nie może być dłuższy niż 255 znaków.")]
        [Display(Name = "Adres e-mail")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Hasło jest wymagane.")]
        [MinLength(8, ErrorMessage = "Hasło musi mieć przynajmniej 8 znaków.")]
        [DataType(DataType.Password)]
        [Display(Name = "Hasło")]
        public string Password { get; set; }
    }
}
