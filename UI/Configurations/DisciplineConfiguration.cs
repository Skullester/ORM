using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ORM;

public class DisciplineConfiguration : IEntityTypeConfiguration<Discipline>
{
    public void Configure(EntityTypeBuilder<Discipline> builder)
    {
        var disciplines = new[]
        {
            new Discipline("Программирование") { Id = 1 },
            new Discipline("Математика") { Id = 2 },
            new Discipline("Базы данных") { Id = 3 },
            new Discipline("Криптография") { Id = 4 },
            new Discipline("Ассемблер и Brainfuck") { Id = 5 },
        };
        builder.HasData(disciplines);
    }
}