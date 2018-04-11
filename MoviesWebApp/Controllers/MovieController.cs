using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MoviesWebApp.DataAccess.Repositories;
using MoviesWebApp.Models;
using MoviesWebApp.Services;
using MoviesWebApp.ViewModels;
using System.IO;

namespace MoviesWebApp.Controllers
{
    public class MovieController : Controller
    {
        private readonly IMovieRepository movieRepository;
        private readonly IFileHelper fileHelper;

        public MovieController(IMovieRepository movieRepository, IFileHelper fileHelper)
        {
            this.movieRepository = movieRepository;
            this.fileHelper = fileHelper;
        }

        [HttpGet]
        [Authorize]
        public IActionResult Add() => View();

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public IActionResult Add(AddMovieViewModel addMovieViewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(addMovieViewModel);
            }

            var destinationDir = Path.Combine("img", "posters");
            string posterPath;
            try
            {
                posterPath = fileHelper.Save(addMovieViewModel.Poster, destinationDir);
            }
            catch
            {
                ModelState.AddModelError("", "W czasie zapisywania plakatu na serwerze, wystąpił błąd.");
                return View(addMovieViewModel);
            }

            var movie = new Movie
            {
                Title = addMovieViewModel.Title,
                Description = addMovieViewModel.Description,
                PosterPath = posterPath,
                ReleaseDate = addMovieViewModel.ReleaseDate.Value
            };

            try
            {
                var movieId = movieRepository.AddMovie(movie);
                return RedirectToAction(nameof(Details), new { id = movieId });
            }
            catch
            {
                fileHelper.Delete(posterPath);

                ModelState.AddModelError("", "W czasie dodawania filmu do bazy danych, wystąpił błąd.");
                return View(addMovieViewModel);
            }           
        }

        [Route("[controller]/[action]/{id:int}")]
        public IActionResult Details(int id)
        {
            var movie = movieRepository.GetMovieById(id);

            if (movie == null)
            {
                return NotFound();
            }

            var addMovieViewModel = new MovieDetailsViewModel
            {
                Title = movie.Title,
                Description = movie.Description,
                PosterPath = movie.PosterPath,
                ReleaseDate = movie.ReleaseDate
            };

            return View(addMovieViewModel);
        }
    }
}
