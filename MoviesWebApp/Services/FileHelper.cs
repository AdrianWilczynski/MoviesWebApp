using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.IO;

namespace MoviesWebApp.Services
{
    public class FileHelper : IFileHelper
    {
        private readonly IHostingEnvironment hostingEnvironment;

        public FileHelper(IHostingEnvironment hostingEnvironment)
        {
            this.hostingEnvironment = hostingEnvironment;
        }

        public string Save(IFormFile file, string relativeDestinationDir)
        {
            var uniqueFileName = GetUniqueName(file.FileName);
            var destinationDir = Path.Combine(hostingEnvironment.WebRootPath, relativeDestinationDir);
            var filePath = Path.Combine(destinationDir, uniqueFileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                file.CopyTo(stream);
            }

            return Path.Combine(relativeDestinationDir, uniqueFileName);
        }

        public void Delete(string relativeFilePath)
        {
            var filePath = Path.Combine(hostingEnvironment.WebRootPath, relativeFilePath);
            File.Delete(filePath);
        }

        private string GetUniqueName(string fileName)
        {
            fileName = Path.GetFileName(fileName);

            return (Path.GetFileNameWithoutExtension(fileName)
                      + "_"
                      + Guid.NewGuid().ToString().Substring(0, 10)
                      + Path.GetExtension(fileName))
                      .Replace(" ", "_");
        }
    }
}
