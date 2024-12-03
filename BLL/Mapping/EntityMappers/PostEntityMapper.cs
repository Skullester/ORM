using BLL.DTO;
using DAL.Entities;

namespace BLL.Mapping.Entities;

internal class PostEntityMapper : IEntityMapper<Post, PostDTO>
{
    public PostDTO Map(Post from) => new PostDTO(from.Name) { Id = from.Id };
}