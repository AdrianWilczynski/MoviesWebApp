using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MoviesWebApp.DataAccess.Repositories;
using MoviesWebApp.Models;
using MoviesWebApp.Services;
using MoviesWebApp.ViewModels;
using System.IO;
using System.Security.Claims;

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

            bool? liked = null;
            if (User.Identity.IsAuthenticated)
            {
                var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
                liked = movieRepository.IsLiked(movie.MovieId, userId);
            }

            var addMovieViewModel = new MovieDetailsViewModel
            {
                MovieId = movie.MovieId,
                Title = movie.Title,
                Description = movie.Description,
                PosterPath = movie.PosterPath,
                ReleaseDate = movie.ReleaseDate,
                Liked = liked
            };

            return View(addMovieViewModel);
        }

        [HttpPost]
        [Authorize]
        public IActionResult Like(int? movieId)
        {
            if (movieId != null && movieRepository.GetMovieById(movieId.Value) == null)
            {
                return NotFound();
            }

            var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));

            if (movieRepository.IsLiked(movieId.Value, userId))
            {
                return BadRequest("Movie already liked");
            }

            movieRepository.LikeMovie(movieId.Value, userId);

            return Ok();
        }

        [HttpPost]
        [Authorize]
        public IActionResult Unlike(int movieId)
        {
            var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            movieRepository.UnlikeMovie(movieId, userId);

            return Ok();
        }
    }
}
