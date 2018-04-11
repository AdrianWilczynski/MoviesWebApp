using Microsoft.AspNetCore.Mvc;

namespace MoviesWebApp.Controllers
{
    public class SystemController : Controller
    {
        public IActionResult Error() => View();
    }
}
