using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MoviesWebApp.ViewModels
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "Nazwa użytkownika jest wymagana.")]
        [MaxLength(255, ErrorMessage = "Nazwa użytkownika nie może być dłuższa niż 255 znaków.")]
        [Display(Name = "Nazwa użytkownika")]
        public string UserName { get; set; }

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

        [Compare(nameof(Password), ErrorMessage = "Hasła muszą się ze sobą zgadzać.")]
        [Required(ErrorMessage = "Powtórzenie hasła jest wymagane.")]
        [DataType(DataType.Password)]
        [Display(Name = "Powtórz hasło")]
        public string RepeatPassword { get; set; }
    }
}
