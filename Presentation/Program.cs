using BusinessLogic.Services.Classes;
using BusinessLogic.Services.Interfaces;
using DataAccess.Data.DbContexts;
using DataAccess.Repositories.Classes;
using DataAccess.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Presentation
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);


            #region Add services to the container.
            builder.Services.AddControllersWithViews();

            //// Register To Service in DI Container
            //builder.Services.AddScoped<AppDbContext>();
            builder.Services.AddDbContext<AppDbContext>(dbContextOptions =>
            {
                //dbContextOptions.UseSqlServer(builder.Configuration["ConnectionStrings:DefaultConnection"]);
                //dbContextOptions.UseSqlServer(builder.Configuration.GetSection("ConnectionStrings")["DefaultConnection"]);
                dbContextOptions.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
            });

            builder.Services.AddScoped<IDepartmentRepo, DepartmentRepo>();
            builder.Services.AddScoped<IDepartmentService, DepartmentService>();

            #endregion

            var app = builder.Build();


            #region Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            //app.UseAuthentication();
            //app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            #endregion

            app.Run();

            //Add-Migration "InitialCreate" -OutputDir "Data/Migrations" -Project "DataAccess" -StartupProject "Presentation" -Verbose
            //Update-Database -Project "DataAccess" -StartupProject "Presentation" -Verbose
        }
    }
}