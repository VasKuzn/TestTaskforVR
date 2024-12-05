using Microsoft.EntityFrameworkCore;
using System.Reflection;
using TestTaskVR.Models;

namespace TodoApi.Models
{
    public class TodoContext : DbContext
    {
        public DbSet<TodoItem> TodoItems { get; set; }
        //Onmodelcreating более универсален и логичен, нежели аттрибуты по типу [key], однако
        //в случае расширения системы введем конфигурационный файл и будем execute все конфигурационные файлы сборки
        //так избежим большого и неудобного к поддержке метода OnModelCreating
        protected override void OnModelCreating(ModelBuilder modelBuilder) 
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
        public TodoContext(DbContextOptions<TodoContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }
    }
}

