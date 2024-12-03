using BLL.DTO;
using BLL.Mapping.Entities;
using DAL.Entities;

namespace BLL.Mapping;

public class GsdDTOEntityMapper : IEntityMapper<GradeStudentDisciplineDTO, GradeStudentDiscipline>
{
    private readonly IMapper mapper;

    public GsdDTOEntityMapper(IMapper mapper)
    {
        this.mapper = mapper;
    }

    public GradeStudentDiscipline Map(GradeStudentDisciplineDTO from) =>
        new GradeStudentDiscipline(from.StudentId, from.DisciplineId, from.GradeId)
            { Id = from.Id, };
}