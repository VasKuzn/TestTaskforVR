using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Reflection.Emit;
using TestTaskVR.Models;
using TodoApi.Models;

namespace TestTaskVR.Models.Configurations
{
    public class TodoitemConfiguration : IEntityTypeConfiguration<TodoItem> 
    {
        public void Configure(EntityTypeBuilder<TodoItem> builder)
        {
            //Id
            builder.HasKey(e => e.Id); // Primary key
            builder.Property(e => e.Id).ValueGeneratedOnAdd();
            builder.ToTable(t => t.HasCheckConstraint("Title", "LENGTH(Title) BETWEEN 1 AND 100")); // из-за особенностей sqlite пришлось дописать constraint
            //Title
            builder.Property(e => e.Title)
                .HasMaxLength(100) // Ограничение на длину в 100 символов
                .IsRequired(); // Поле обязательно
                                  

            //IsCompleted
            builder.Property(e => e.IsCompleted)
                   .HasDefaultValue(false); // Дефолтное значение - false

            //CreatedAt
            builder.Property(e => e.CreatedAt)
                   .HasDefaultValueSql("GETDATE()"); // Дефолтное значение - текущая дата в формате sqlite

        }
    }
}
