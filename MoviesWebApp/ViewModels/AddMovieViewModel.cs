using Microsoft.AspNetCore.Http;
using System;
using System.ComponentModel.DataAnnotations;

namespace MoviesWebApp.ViewModels
{
    public class AddMovieViewModel
    {
        [MaxLength(255, ErrorMessage = "Tytuł nie może być dłuższy niż 255 znaków.")]
        [Required(ErrorMessage = "Tytuł jest wymagany.")]
        [Display(Name = "Tytuł")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Opis jest wymagany.")]
        [Display(Name = "Opis")]
        public string Description { get; set; }

        [DataType(DataType.Date)]
        [Required(ErrorMessage = "Data jest wymagana.")]
        [Display(Name = "Premiera")]
        public DateTime? ReleaseDate { get; set; }

        [Required(ErrorMessage = "Plakat jest wymagany.")]
        [Display(Name = "Plakat")]
        public IFormFile Poster { get; set; }
    }
}
