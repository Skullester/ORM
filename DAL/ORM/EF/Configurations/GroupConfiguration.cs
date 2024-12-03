using DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DAL.ORM.Configurations;

public class GroupConfiguration : IEntityTypeConfiguration<Group>
{
    public void Configure(EntityTypeBuilder<Group> builder)
    {
        Group[] groups =
        [
            new Group("Б21-191-1", 7) { Id = 1 },
            new Group("Б20-191-4", 8) { Id = 2 },
            new Group("Б22-191-2", 5) { Id = 3 },
            new Group("Б23-191-3", 3) { Id = 4 },
            new Group("Б23-021-1", 4) { Id = 5 },
            new Group("Б22-021-2", 6) { Id = 6 },
        ];
        builder.Property(x => x.Name)
            .HasMaxLength(ConfigurationsPresets.MaxNameLength);
        builder.HasData(groups);
    }
}