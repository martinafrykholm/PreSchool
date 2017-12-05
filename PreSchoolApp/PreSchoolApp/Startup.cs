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

namespace PreSchoolApp
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            var connString = @"Server=tcp:preschoolserver.database.windows.net,1433;Initial Catalog=PreSchoolDB;Persist Security Info=False;User ID=preschoolAdmin;Password=Grupp1C#;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30";
            services.AddDbContext<IdentityDbContext>(o => o.UseSqlServer(connString));

            services.AddIdentity<IdentityUser, IdentityRole>(
                o =>
                {
                    o.Password.RequireNonAlphanumeric = false;
                    o.Password.RequiredLength = 7;
                }
                ).AddEntityFrameworkStores<IdentityDbContext>()
                .AddDefaultTokenProviders();


            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(o => o.LoginPath = "/Login/index");



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
