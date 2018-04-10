using MoviesWebApp.Models;

namespace MoviesWebApp.DataAccess.Repositories
{
    public interface IUserRepository
    {
        void AddUser(User user);
        User GetUserByEmail(string email);
    }
}
