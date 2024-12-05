
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

            string? connectionString = builder.Configuration.GetConnectionString("DefaultConnection"); //connection ����� �� JSON. ���������� SQLite + EF

            builder.Services.AddDbContext<TodoContext>(options =>
                options.UseSqlite(connectionString));
            builder.Services.AddScoped<ITodoService, TodoService>(); // �� ������� ��������� TodoService, � ������� ������ ����������� ����������� ��� ���������� ������
            builder.Services.AddControllers(); //������������� VS ��������� ���������� ��� ����������� �������� ���������
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(); // ������� swagger ��� �������� �������������� � �����������
            var app = builder.Build();
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.UseMiddleware<RequestLoggingMiddleware>(); // ����� ��������� middleware �� ������� ��� ����������� ��������. 

            app.MapControllers();

            app.Run();
        }
    }
}
