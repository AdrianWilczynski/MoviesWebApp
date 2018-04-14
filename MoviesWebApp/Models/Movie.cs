using System;

namespace MoviesWebApp.Models
{
    public class Movie
    {
        public int MovieId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string PosterPath { get; set; }
        public DateTime ReleaseDate { get; set; }
        public string Country { get; set; }
    }
}
