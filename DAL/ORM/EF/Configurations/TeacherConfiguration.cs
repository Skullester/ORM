using DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DAL.ORM.Configurations;

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
        builder.Property(x => x.Name)
            .HasMaxLength(ConfigurationsPresets.MaxNameLength);
        builder.HasData(teachers);
    }
}