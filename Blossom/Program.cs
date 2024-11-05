using DAO;
using BusinessObjects.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using Services.Interfaces;
using Services;

namespace Blossom
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            /************* ADD SERVICES *************/

            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ??
                throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

            var migrationsAssembly = typeof(Program).GetTypeInfo().Assembly.GetName().Name;

            builder.Services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseSqlServer(connectionString, b => b.MigrationsAssembly(migrationsAssembly));
            });

            // Add Identity
            builder.Services.AddIdentity<Account, Role>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            // Configure Identity options and password complexity here
            builder.Services.Configure<IdentityOptions>(options =>
            {
                // User settings
                options.User.RequireUniqueEmail = true;

                options.Password.RequireDigit = false;
                options.Password.RequiredLength = 0;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireLowercase = false;


            });

            builder.Services.AddScoped<IAuthService, AuthService>();

            // Add cors
            builder.Services.AddCors();

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore-swashbuckle
            builder.Services.AddEndpointsApiExplorer();

            builder.Services.AddAutoMapper(typeof(Program));

            // Add services to the container
            builder.Services.AddRazorPages();

            var app = builder.Build();

            // Configure the HTTP request pipeline
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseCors(builder => builder
               .AllowAnyOrigin()
               .AllowAnyHeader()
               .AllowAnyMethod());

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapRazorPages();

            app.Run();
        }
    }
}
