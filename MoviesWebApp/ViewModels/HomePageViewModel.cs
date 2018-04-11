using System.Collections.Generic;

namespace MoviesWebApp.ViewModels
{
    public class HomePageViewModel
    {
        public int Page { get; set; }
        public IEnumerable<MovieCardViewModel> MovieCards { get; set; }
    }
}
