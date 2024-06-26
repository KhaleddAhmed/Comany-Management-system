using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using RouteC41.G02.BLL.Interfaces;
using RouteC41.G02.BLL.Repositries;
using RouteC41.G02.DAL.Data;
using RouteC41.G02.DAL.Models;
using RouteC41.G02.PL.Extenstions;
using RouteC41.G02.PL.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RouteC41.G02.PL
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();//Register BUILT-In Services Required For Mvc
            ///services.AddTransient<ApplicationDbContext>();
            /// services.AddScoped<ApplicationDbContext>();
            ///services.AddSingleton<ApplicationDbContext>();
            ///services.AddScoped<DbContextOptions<ApplicationDbContext>>();
            services.AddDbContext<ApplicationDbContext>(options=>
            {
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"));
            });


            services.AddApplicationServies();//ExtensionMethod inside Folder Extension
            services.AddAutoMapper(M => M.AddProfile(new MappingProfiles()));
            services.AddScoped<UserManager<ApplicationUser>>();

            services.AddIdentity<ApplicationUser, IdentityRole>(options=>
            {
                options.Password.RequiredUniqueChars = 2;
                options.Password.RequireDigit= true;
                options.Password.RequireLowercase= true;
                options.Password.RequireUppercase= true;
                options.Password.RequireNonAlphanumeric= true;
                options.Password.RequiredLength = 5;

                options.Lockout.AllowedForNewUsers= true;
                options.Lockout.MaxFailedAccessAttempts= 5;
                options.Lockout.DefaultLockoutTimeSpan= TimeSpan.FromDays(5);

                //options.User.AllowedUserNameCharacters = "asdfghk9876";
                options.User.RequireUniqueEmail= true;


                
            }).AddEntityFrameworkStores<ApplicationDbContext>();

            //services.AddAuthentication();
       
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();


            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    } 
}
