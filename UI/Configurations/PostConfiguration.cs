using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ORM;

public class PostConfiguration : IEntityTypeConfiguration<Post>
{
    public void Configure(EntityTypeBuilder<Post> builder)
    {
        var posts = new[]
        {
            new Post("Профессор") { Id = 1 },
            new Post("Старший преподаватель") { Id = 2 },
            new Post("Доцент") { Id = 3 }
        };
        builder.HasData(posts);
    }
}