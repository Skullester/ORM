using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ORM;

public class TeacherConfiguration : IEntityTypeConfiguration<Teacher>
{
    public void Configure(EntityTypeBuilder<Teacher> builder)
    {
        Teacher[] teachers =
        [
            new Teacher("Алексей Благовайт", 3) { Id = 9 },
            new Teacher("Дарья Сверчкова", 1) { Id = 10 },
            new Teacher("Максим Миксушин", 2) { Id = 11 },
        ];
        builder.HasData(teachers);
    }
}