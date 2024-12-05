
namespace TestTaskVR
{
    using Microsoft.EntityFrameworkCore;
    using TestTaskVR.Interfaces;
    using TestTaskVR.Middleware;
    using TodoApi.Models;
    using static Microsoft.AspNetCore.Http.StatusCodes;
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            string? connectionString = builder.Configuration.GetConnectionString("DefaultConnection"); //connection берем из JSON. Используем SQLite + EF

            builder.Services.AddDbContext<TodoContext>(options =>
                options.UseSqlite(connectionString));
            builder.Services.AddScoped<ITodoService, TodoService>(); // по заданию добавляем TodoService, в который пойдут возможности контроллера для разделения логики
            builder.Services.AddControllers(); //возможностями VS добавляем контроллер для оптимизации процесса написания
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(); // добавим swagger для удобного взаимодействия с приложением
            var app = builder.Build();
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.UseMiddleware<RequestLoggingMiddleware>(); // здесь добавляем middleware по заданию для регистрации запросов. 

            app.MapControllers();

            app.Run();
        }
    }
}
