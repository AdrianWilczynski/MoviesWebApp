using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MoviesWebApp.Configuration;
using MoviesWebApp.Controllers;
using MoviesWebApp.DataAccess;
using MoviesWebApp.DataAccess.Repositories;
using MoviesWebApp.Models;
using MoviesWebApp.Services;

namespace MoviesWebApp
{
    public class Startup
    {
        private readonly IConfiguration configuration;

        public Startup(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddOptions();
            services.Configure<DatabaseOptions>(configuration.GetSection("Database"));
            services.Configure<ContactOptions>(configuration.GetSection("Contact"));

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(options =>
                {
                    options.LoginPath = "/Account/Login";
                });

            services.AddTransient<IConnectionFactory, ConnectionFactory>();

            services.AddTransient<IMovieRepository, MovieRepository>();
            services.AddTransient<IUserRepository, UserRepository>();

            services.AddTransient<IFileHelper, FileHelper>();
            services.AddTransient<IUserManager, UserManager>();

            services.AddTransient<IPasswordHasher<User>, PasswordHasher<User>>();

            services.AddMvc();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseBrowserLink();
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/System/Error");
            }

            app.UseStatusCodePages();
            app.UseStaticFiles();
            app.UseAuthentication();
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "Default",
                    template: "{controller}/{action=Index}");
            });
        }
    }
}
