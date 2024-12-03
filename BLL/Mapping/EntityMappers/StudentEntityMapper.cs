using BLL.DTO;
using DAL.Entities;

namespace BLL.Mapping.Entities;

internal class StudentEntityMapper : IEntityMapper<Student, StudentDTO>
{
    private readonly IMapper mapper;

    public StudentEntityMapper(IMapper mapper)
    {
        this.mapper = mapper;
    }

    public StudentDTO Map(Student from) => new StudentDTO(from.Name, from.GroupId)
    {
        Id = from.Id,
        Group = mapper.From(from.Group)!
            .To<GroupDTO>()
    };
}