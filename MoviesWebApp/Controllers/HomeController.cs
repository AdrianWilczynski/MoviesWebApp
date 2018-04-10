using Microsoft.AspNetCore.Mvc;
using MoviesWebApp.DataAccess.Repositories;
using MoviesWebApp.ViewModels;
using System.Linq;

namespace MoviesWebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly IMovieRepository movieRepository;

        public HomeController(IMovieRepository movieRepository)
        {
            this.movieRepository = movieRepository;
        }

        [Route("Page/{pageIndex:int}")]
        [Route("/")]
        public IActionResult Index(int page = 1, int pageSize = 10)
        {

            var basicMovieViewModels = movieRepository.GetMovies((page - 1) * pageSize, pageSize)
                .Select(m => new BasicMovieViewModel
                {
                    Title = m.Title,
                    MovieId = m.MovieId
                })
                .ToList();

            return View(basicMovieViewModels);
        }
    }
}
