using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ORM;

public class SemesterConfiguration : IEntityTypeConfiguration<Semester>
{
    public void Configure(EntityTypeBuilder<Semester> builder)
    {
        Semester[] semesters =
        [
            new Semester("1") { Id = 1, },
            new Semester("2") { Id = 2 },
            new Semester("3") { Id = 3 },
            new Semester("4") { Id = 4 },
            new Semester("5") { Id = 5 },
            new Semester("6") { Id = 6 },
            new Semester("7") { Id = 7 },
            new Semester("8") { Id = 8 },
        ];
        builder.HasData(semesters);
    }
}