using DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DAL.ORM.Configurations;

public class GradeConfiguration : IEntityTypeConfiguration<Grade>
{
    public void Configure(EntityTypeBuilder<Grade> builder)
    {
        var grades = new[]
        {
            new Grade("2") { Id = 1 },
            new Grade("3") { Id = 2 },
            new Grade("4") { Id = 3 },
            new Grade("5") { Id = 4 }
        };
        builder.Property(x => x.Name)
            .HasMaxLength(ConfigurationsPresets.MaxNameLength);
        builder.HasData(grades);
    }
}