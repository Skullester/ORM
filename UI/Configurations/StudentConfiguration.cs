using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ORM;

public class StudentConfiguration : IEntityTypeConfiguration<Student>
{
    public void Configure(EntityTypeBuilder<Student> builder)
    {
        Student[] students =
        [
            new Student("Игорь Шип", 1) { Id = 1 },
            new Student("Антоний Яськов", 2) { Id = 2 },
            new Student("Сергей Ментолов", 2) { Id = 3 },
            new Student("Алексей Ментолов", 2) { Id = 4 },
            new Student("Максим Ментолов", 2) { Id = 5 },
            new Student("Людмила Красова", 3) { Id = 6 },
            new Student("Алексей Светлаков", 3) { Id = 7 },
            new Student("Александр Петров", 3) { Id = 8 },
        ];
        builder.HasData(students);
    }
}