﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

using AutoMapper;

using MetricsApi.Utilities;
using MetricsApi.DataAccess;
using MetricsApi.DataAccess.EntityModels;
using MetricsApi.DataAccess.Repositories;
using MetricsApi.DataService;
using MetricsApi.Models;

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
            services.AddCors();
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
            services.AddAutoMapper(typeof(Startup));

            var appSettings = _configuration.Get<AppSettings>();
            services.AddSingleton<IAppSettings>(t => appSettings);

            services.AddTransient<IDbConnectionHelper, DbConnectionHelper>();
            services.AddTransient<IUserRepository, UserRepository>();
            services.AddTransient<IUserService, UserService>();

            var jwtSecret = appSettings.JwtSecret;
            var key = Encoding.ASCII.GetBytes(jwtSecret);
            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })            
            .AddJwtBearer(x =>
             {
                 x.Events = new JwtBearerEvents
                 {
                     OnTokenValidated = context =>
                     {
                         var userService = context.HttpContext.RequestServices.GetRequiredService<IUserService>();
                         var userId = int.Parse(context.Principal.Identity.Name);
                         var user = userService.GetById(userId);
                         if (user == null)
                         {
                             // return unauthorized if user no longer exists
                             context.Fail("Unauthorized");
                         }
                         return Task.CompletedTask;
                     }
                 };
                 x.RequireHttpsMetadata = false;
                 x.SaveToken = true;
                 x.TokenValidationParameters = new TokenValidationParameters
                 {
                     ValidateIssuerSigningKey = true,
                     IssuerSigningKey = new SymmetricSecurityKey(key),
                     ValidateIssuer = false,
                     ValidateAudience = false
                 };
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
