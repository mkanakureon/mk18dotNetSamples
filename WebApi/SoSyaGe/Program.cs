using Microsoft.EntityFrameworkCore;
using SoSyaGeApp.Models;
using System.Diagnostics;

namespace SoSyaGeApp
{
    public partial class Program
    {
        public static void Main(string[] args)
        {
            Debug.WriteLine("Main");

            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            builder.Services.AddDbContext<TodoContext>(opt => opt.UseInMemoryDatabase("TodoList"));
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseMiddleware<RequestResponseLoggingMiddleware>();

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllers();
            Debug.WriteLine("Main-2");
            app.Run();

            Debug.WriteLine("Main end");
        }
    }
}
