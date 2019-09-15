using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

using MetricsApi.Utilities;
using MetricsApi.DataAccess;
using MetricsApi.DataAccess.EntityModels;
using MetricsApi.DataAccess.Repositories;
using MetricsApi.DataService;
using MetricsApi.DataService.Models;

namespace MetricsApi
{
    public class Startup
    {
        private readonly IConfiguration _configuration;

        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            var appSettings = _configuration.Get<AppSettings>();
            services.AddSingleton<IAppSettings>(t => appSettings);

            services.AddTransient<IDbConnectionHelper, DbConnectionHelper>();
            services.AddTransient<IUserRepository, UserRepository>();
            services.AddTransient<IUserService, UserService>();

            services.Configure<AuthenticationConfig>(_configuration.GetSection("Authentication"));
            services.AddAuthentication()
                .AddGoogle(options =>
                {
                    options.ClientId = appSettings.AuthenticationGoogleClientId;
                    options.ClientSecret = appSettings.AuthenticationGoogleClientSecret;
                });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCors(x => x.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
            app.UseMvc();
        }
    }
}
