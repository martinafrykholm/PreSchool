using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.Extensions.Configuration;
using PreSchoolApp.Models;
using PreSchoolApp.Models.Entities;

namespace PreSchoolApp
{
    public class Startup
    {

        IConfiguration config;

        public Startup(IConfiguration config)
        {
            this.config = config;

        }
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            var configAzure = config.GetConnectionString("connString");
            services.AddDbContext<PreSchoolAppContext>(o => o.UseSqlServer(configAzure));
            services.AddDbContext<IdentityDbContext>(o => o.UseSqlServer(configAzure));


            services.AddIdentity<IdentityUser, IdentityRole>(
                o =>
                {
                    o.Password.RequireNonAlphanumeric = false;
                    o.Password.RequiredLength = 0;
                    o.Password.RequireUppercase = false;
                    o.Password.RequireDigit = false;
                }
                ).AddEntityFrameworkStores<IdentityDbContext>()
                .AddDefaultTokenProviders();

            //services.Configure<IdentityOptions>(options =>
            //{
            //    options.User.RequireUniqueEmail = true;

            //});

            //services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                //.AddCookie(o => o.LoginPath = "/Login/index");

            services.ConfigureApplicationCookie(options =>
            {
                options.LoginPath = "/Login/index";
                options.LogoutPath = "/Login/LogOut";
                options.AccessDeniedPath = "/Login/NoAccess";
            });

            services.AddTransient<PreSchoolDBRepository>();

            //services.ConfigureApplicationCookie(o => o.LoginPath = "/Home/Login")
            //Använd denna om Identity är använd och URL:en annan äb /Account/Login
            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseStaticFiles();
            app.UseAuthentication();
            app.UseMvcWithDefaultRoute();
        }
    }
}
