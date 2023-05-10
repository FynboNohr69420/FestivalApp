using Common.Model;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.EntityFrameworkCore;
using Server.Repositories;

namespace Server
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();
            builder.Services.AddRazorPages();
            builder.Services.AddControllers();
            builder.Services.AddSingleton<IBruger, BrugerRepositorySQL>();

          
            builder.Services.AddCors(options => // policy der sl�r CORS fra, s� der er adgang fra alle sources
            {
                options.AddPolicy("policy",
                                policy =>
                                {
                                    policy.AllowAnyOrigin();
                                    policy.AllowAnyHeader();
                                    policy.AllowAnyMethod();
                                }
                                );
            }) ;

            var app = builder.Build();

            // Configure the HTTP request pipeline.

            app.UseHttpsRedirection();

            app.UseCors("policy"); // Anvender ovenst�ende policy

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}