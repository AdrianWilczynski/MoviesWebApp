using Dapper;
using MoviesWebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MoviesWebApp.DataAccess.Repositories
{
    public class MovieRepository : IMovieRepository
    {
        private readonly IConnectionFactory connectionFactory;

        public MovieRepository(IConnectionFactory connectionFactory)
        {
            this.connectionFactory = connectionFactory;
        }

        public int AddMovie(Movie movie)
        {
            using (var connection = connectionFactory.GetConnection())
            {
                return connection.QuerySingle<int>(@"INSERT INTO Movies (Title, Description, PosterPath, ReleaseDate)
                    OUTPUT INSERTED.MovieId
                    VALUES(@Title, @Description, @PosterPath,  @ReleaseDate);",
                    new { movie.Title, movie.Description, movie.PosterPath, movie.ReleaseDate });
            }
        }

        public Movie GetMovieById(int id)
        {
            using (var connection = connectionFactory.GetConnection())
            {
                return connection.QuerySingleOrDefault<Movie>(@"SELECT * FROM Movies
                    WHERE MovieId = @Id;",
                    new { Id = id });
            }
        }

        public IEnumerable<Movie> GetMovies(int skip, int take)
        {
            using (var connection = connectionFactory.GetConnection())
            {
                return connection.Query<Movie>(@"SELECT * FROM Movies
                    ORDER BY MovieId
                    OFFSET @Skip ROWS
                    FETCH NEXT @Take ROWS ONLY;",
                    new { Skip = skip, Take = take });
            }
        }

        public void LikeMovie(int movieId, int userId)
        {
            using (var connection = connectionFactory.GetConnection())
            {
                connection.Execute(@"INSERT INTO Likes(UserId, MovieId)
                    VALUES (@MovieId, @UserId);",
                    new { MovieId = movieId, UserId = userId });
            }
        }

        public void UnlikeMovie(int movieId, int userId)
        {
            using (var connection = connectionFactory.GetConnection())
            {
                connection.Execute(@"DELETE FROM Likes
                    WHERE MovieId = @MovieId AND UserId = @UserId;",
                    new { MovieId = movieId, UserId = userId });
            }
        }

        public bool IsLiked(int movieId, int userId)
        {
            using (var connection = connectionFactory.GetConnection())
            {
                var like = connection.QuerySingleOrDefault(@"SELECT * FROM Likes
                    WHERE MovieId = @MovieId AND UserId = @UserId;",
                    new { MovieId = movieId, UserId = userId });

                return like != null;
            }
        }
    }
}
