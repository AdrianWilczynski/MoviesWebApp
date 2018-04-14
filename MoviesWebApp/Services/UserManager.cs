using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using MoviesWebApp.DataAccess.Repositories;
using MoviesWebApp.Models;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace MoviesWebApp.Services
{
    public class UserManager : IUserManager
    {
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly IPasswordHasher<User> passwordHasher;
        private readonly IUserRepository userRepository;

        public UserManager(IHttpContextAccessor httpContextAccessor, IPasswordHasher<User> passwordHasher,
            IUserRepository userRepository)
        {
            this.httpContextAccessor = httpContextAccessor;
            this.passwordHasher = passwordHasher;
            this.userRepository = userRepository;
        }

        public async Task<bool> TryLoginAsync(string email, string password)
        {
            var user = userRepository.GetUserByEmail(email);

            if (user == null)
            {
                return false;
            }

            if (passwordHasher.VerifyHashedPassword(
                null, user.PasswordHash, password) != PasswordVerificationResult.Success)
            {
                return false;
            }

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.NameIdentifier, user.UserId.ToString()),
                new Claim(ClaimTypes.Name, user.UserName)
            };

            var userIdentity = new ClaimsIdentity(claims, "email");

            var claimsPrincipal = new ClaimsPrincipal(userIdentity);
            await httpContextAccessor.HttpContext.SignInAsync(claimsPrincipal);

            return true;
        }

        public bool TryRegister(string email, string userName, string password)
        {
            var passwordHash = passwordHasher.HashPassword(null, password);

            var user = new User
            {
                Email = email,
                UserName = userName,
                PasswordHash = passwordHash
            };

            if (userRepository.GetUserByEmail(email) != null)
            {
                return false;
            }

            try
            {
                userRepository.AddUser(user);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task LogoutAsync() => await httpContextAccessor.HttpContext.SignOutAsync();
    }
}
