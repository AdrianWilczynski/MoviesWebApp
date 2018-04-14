using MoviesWebApp.Models;
using System.Collections.Generic;

namespace MoviesWebApp.DataAccess.Repositories
{
    public interface IMovieRepository
    {
        int AddMovie(Movie movie);
        Movie GetMovieById(int id);
        IEnumerable<Movie> GetMovies(int skip, int take);
        void LikeMovie(int movieId, int userId);
        void UnlikeMovie(int movieId, int userId);
        bool IsLiked(int movieId, int userId);
    }
}
