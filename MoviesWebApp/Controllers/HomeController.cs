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

        [Route("Page/{page:int}")]
        [Route("/")]
        public IActionResult Index(int page = 1, int pageSize = 10)
        {
            var movieCardViewModels = movieRepository.GetMovies((page - 1) * pageSize, pageSize)
                .Select(m => new MovieCardViewModel
                {
                    Title = m.Title,
                    PosterPath = m.PosterPath,
                    MovieId = m.MovieId
                })
                .ToList();

            var isAjaxRequest = HttpContext.Request.Headers["X-Requested-With"] == "XMLHttpRequest";
            if (isAjaxRequest)
            {
                return PartialView("MovieCardsPartial", movieCardViewModels);
            }

            var homePageViewModel = new HomePageViewModel
            {
                Page = page,
                MovieCards = movieCardViewModels
            };

            return View(homePageViewModel);
        }
    }
}
