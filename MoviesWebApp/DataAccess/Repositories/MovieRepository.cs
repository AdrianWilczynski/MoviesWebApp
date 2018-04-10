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

        public void AddMovie(Movie movie)
        {
            using (var connection = connectionFactory.GetConnection())
            {
                connection.Execute(@"INSERT INTO Movies (Title, Description, PosterPath, ReleaseDate)
                    VALUES(@Title, @Description, @PosterPath,  @ReleaseDate);",
                    new { movie.Title, movie.Description, movie.PosterPath, movie.ReleaseDate });
            }
        }

        public Movie GetMovieById(int id)
        {
            using (var connection = connectionFactory.GetConnection())
            {
                return connection.Query<Movie>(@"SELECT * FROM Movies
                    WHERE MovieId = @Id",
                    new { Id = id }).SingleOrDefault();
            }
        }

        public IEnumerable<Movie> GetMovies()
        {
            using (var connection = connectionFactory.GetConnection())
            {
                return connection.Query<Movie>("SELECT * FROM Movies");
            }
        }

        public IEnumerable<Movie> GetMovies(int skip, int take)
        {
            using (var connection = connectionFactory.GetConnection())
            {
                return connection.Query<Movie>(@"SELECT * FROM Movies
                    ORDER BY MovieId
                    OFFSET @Skip ROWS
                    FETCH NEXT @Take ROWS ONLY",
                    new { Skip = skip, Take = take });
            }
        }
    }
}
