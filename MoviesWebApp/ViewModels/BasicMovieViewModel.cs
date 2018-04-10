namespace MoviesWebApp.ViewModels
{
    public class BasicMovieViewModel
    {
        private string _posterPath;

        public int MovieId { get; set; }

        public string Title { get; set; }

        public string PosterPath { get => _posterPath; set => _posterPath = value.Replace(@"\", "/"); }
    }
}
