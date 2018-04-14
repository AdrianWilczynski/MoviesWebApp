using Dapper;
using MoviesWebApp.Models;
using System.Linq;

namespace MoviesWebApp.DataAccess.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly IConnectionFactory connectionFactory;

        public UserRepository(IConnectionFactory connectionFactory)
        {
            this.connectionFactory = connectionFactory;
        }

        public void AddUser(User user)
        {
            using (var connection = connectionFactory.GetConnection())
            {
                connection.Execute(@"INSERT INTO Users (Email, UserName, PasswordHash)
                    VALUES(@Email, @UserName, @PasswordHash);",
                    new { user.Email, user.UserName, user.PasswordHash });
            }
        }

        public User GetUserByEmail(string email)
        {
            using (var connection = connectionFactory.GetConnection())
            {
                return connection.QuerySingleOrDefault<User>(@"SELECT * FROM Users
                    WHERE Email = @Email;",
                    new { Email = email });
            }
        }
    }
}
