using System.Threading.Tasks;

namespace MoviesWebApp.Services
{
    public interface IUserManager
    {
        Task<bool> TryLoginAsync(string email, string password);
        Task LogoutAsync();
        bool TryRegister(string email, string userName, string password);
    }
}
