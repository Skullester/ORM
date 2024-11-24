using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ORM;

public class GroupConfiguration : IEntityTypeConfiguration<Group>
{
    public void Configure(EntityTypeBuilder<Group> builder)
    {
        Group[] groups =
        [
            new Group("Б21-191-1", 7) { Id = 1 },
            new Group("Б22-191-2", 5) { Id = 2 },
            new Group("Б23-191-3", 3) { Id = 3 },
        ];
        builder.HasData(groups);
    }
}