using BLL.DTO;
using BLL.Mapping.Entities;
using DAL.Entities;

namespace BLL.Mapping;

public class GradeInfoDTOEntityMapper : IEntityMapper<GradeInfoDTO, GradeInfo>
{
    private readonly IMapper mapper;

    public GradeInfoDTOEntityMapper(IMapper mapper)
    {
        this.mapper = mapper;
    }

    public GradeInfo Map(GradeInfoDTO from) =>
        new GradeInfo(from.StudentId, from.DisciplineId, from.GradeId)
            { Id = from.Id, };
}