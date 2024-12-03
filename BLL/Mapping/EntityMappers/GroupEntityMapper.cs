using BLL.DTO;
using DAL.Entities;

namespace BLL.Mapping.Entities;

internal class GroupEntityMapper : IEntityMapper<Group, GroupDTO>
{
    private readonly IMapper mapper;

    public GroupEntityMapper(IMapper mapper)
    {
        this.mapper = mapper;
    }

    public GroupDTO Map(Group from) => new GroupDTO(from.Name, from.SemesterId)
    {
        Id = from.Id,
        Semester = mapper.From(from.Semester)!
            .To<SemesterDTO>()
    };
}