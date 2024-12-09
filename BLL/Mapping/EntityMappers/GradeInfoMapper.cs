using BLL.DTO;
using DAL.Entities;

namespace BLL.Mapping.Entities;

internal class GradeInfoMapper : IEntityMapper<GradeInfo, GradeInfoDTO>
{
    private readonly IMapper mapper;

    public GradeInfoMapper(IMapper mapper)
    {
        this.mapper = mapper;
    }

    public GradeInfoDTO Map(GradeInfo from) =>
        new GradeInfoDTO(from.StudentId, from.DisciplineId, from.GradeId)
        {
            Id = from.Id, Grade = mapper.From(from.Grade)!.To<GradeDTO>()
        };
}