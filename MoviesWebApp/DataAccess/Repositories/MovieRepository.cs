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
                return connection.QuerySingle<int>(@"INSERT INTO Movies (Title, Description, PosterPath, ReleaseDate, Country)
                    OUTPUT INSERTED.MovieId
                    VALUES(@Title, @Description, @PosterPath,  @ReleaseDate, @Country);",
                    new { movie.Title, movie.Description, movie.PosterPath, movie.ReleaseDate, movie.Country });
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

        public IEnumerable<Movie> GetLikedMovies(int userId)
        {
            using (var connection = connectionFactory.GetConnection())
            {
                return connection.Query<Movie>(
                    @"SELECT Movies.MovieId, Title, Description, PosterPath, ReleaseDate, Country FROM Movies
                    JOIN Likes ON Movies.MovieId = Likes.MovieId
                    JOIN Users ON Likes.UserId = Users.UserId
                    WHERE Users.UserId = 1;",
                    new { UserId = userId });
            }
        }

        public void LikeMovie(int movieId, int userId)
        {
            using (var connection = connectionFactory.GetConnection())
            {
                connection.Execute(@"INSERT INTO Likes(UserId, MovieId)
                    VALUES (@UserId, @MovieId);",
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
