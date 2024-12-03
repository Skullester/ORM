using BLL.DTO;
using DAL.Entities;

namespace BLL.Mapping.Entities;

internal class TeacherEntityMapper : IEntityMapper<Teacher, TeacherDTO>
{
    private readonly IMapper mapper;

    public TeacherEntityMapper(IMapper mapper)
    {
        this.mapper = mapper;
    }

    public TeacherDTO Map(Teacher from) => new TeacherDTO(from.Name, from.PostId)
    {
        Id = from.Id,
        Post = mapper.From(from.Post)!
            .To<PostDTO>()
    };
}