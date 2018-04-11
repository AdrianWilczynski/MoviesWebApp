using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using MoviesWebApp.Configuration;

namespace MoviesWebApp.Controllers
{
    public class AuthorController : Controller
    {
        private readonly string contactEmail;

        public AuthorController(IOptions<ContactOptions> databaseOptions)
        {
            contactEmail = databaseOptions.Value.Email;
        }

        public IActionResult Contact() => View(contactEmail);
    }
}
