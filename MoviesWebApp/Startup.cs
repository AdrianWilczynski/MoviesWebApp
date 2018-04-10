using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MoviesWebApp.Configuration;
using MoviesWebApp.DataAccess;
using MoviesWebApp.DataAccess.Repositories;
using Microsoft.AspNetCore.Authentication.Cookies;
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

            services.AddTransient<IConnectionFactory, ConnectionFactory>();

            services.AddTransient<IMovieRepository, MovieRepository>();

            services.AddTransient<IFileHelper, FileHelper>();

            services.AddMvc();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseBrowserLink();
                app.UseDeveloperExceptionPage();
            }

            app.UseStatusCodePages();
            app.UseStaticFiles();
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "Default",
                    template: "{controller}/{action=Index}");
            });
        }
    }
}
