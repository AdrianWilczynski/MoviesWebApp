using Microsoft.AspNetCore.Http;

namespace MoviesWebApp.Services
{
    public interface IFileHelper
    {
        string Save(IFormFile file, string relativeDestinationDir);
        void Delete(string relativeFilePath);
    }
}
