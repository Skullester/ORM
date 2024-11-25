using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ORM;

public class DisciplineConfiguration : IEntityTypeConfiguration<Discipline>
{
    public void Configure(EntityTypeBuilder<Discipline> builder)
    {
        var disciplines = new[]
        {
            new Discipline("Линейная алгебра", 1) { Id = 1 },
            new Discipline("Аналитическая геометрия", 1) { Id = 2 },
            new Discipline("Мат. анализ", 2) { Id = 3 },
            new Discipline("Программирование", 2) { Id = 4 },
            new Discipline("История", 2) { Id = 5 },
            new Discipline("Философия", 3) { Id = 6 },
            new Discipline("Операционные системы", 3) { Id = 7 },
            new Discipline("Базы данных", 3) { Id = 8 },
            new Discipline("Криптография", 4) { Id = 9 },
            new Discipline("ОБЖ", 5) { Id = 10 },
            new Discipline("Ассемблер и Brainfuck", 5) { Id = 11 },
            new Discipline("C++", 6) { Id = 12 },
            new Discipline("Scratch", 6) { Id = 13 },
            new Discipline("Pascal", 7) { Id = 14 },
            new Discipline("Успешных успех", 8) { Id = 15 },
            new Discipline("Маркетинг", 8) { Id = 16 },
        };
        builder.HasData(disciplines);
    }
}