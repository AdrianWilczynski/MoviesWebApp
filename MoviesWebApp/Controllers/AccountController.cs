using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MoviesWebApp.Services;
using MoviesWebApp.ViewModels;
using System.Threading.Tasks;

namespace MoviesWebApp.Controllers
{
    public class AccountController : Controller
    {
        private readonly IUserManager userManager;

        public AccountController(IUserManager userManager)
        {
            this.userManager = userManager;
        }

        [HttpGet]
        public IActionResult Register() => View();

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Register(RegisterViewModel registerViewModel)
        {
            if(!ModelState.IsValid)
            {
                return View(registerViewModel);
            }

            var isSuccessful = userManager.TryRegister(registerViewModel.Email,
                registerViewModel.UserName, registerViewModel.Password);

            if(!isSuccessful)
            {
                ModelState.AddModelError("", "Nieudana próba rejestracji.");
                return View(registerViewModel);
            }

            return RedirectToAction(nameof(Login));
        }

        [HttpGet]
        public IActionResult Login() => View();

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel loginViewModel, string returnUrl)
        {
            if (!ModelState.IsValid)
            {
                return View(loginViewModel);
            }

            await userManager.LogoutAsync();

            var isSuccessful = await userManager.TryLoginAsync(loginViewModel.Email, loginViewModel.Password);

            if (!isSuccessful)
            {
                ModelState.AddModelError("", "Nieudana próba logowania.");
                return View(loginViewModel);
            }

            return Redirect(Url.IsLocalUrl(returnUrl) ? returnUrl : "/");
        }

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await userManager.LogoutAsync();
            return Redirect("/");
        }
    }
}
