using BLL.DTO;
using DAL.Entities;

namespace BLL.Mapping.Entities;

internal class GradeStudentDisciplineEntityMapper : IEntityMapper<GradeStudentDiscipline, GradeStudentDisciplineDTO>
{
    private readonly IMapper mapper;

    public GradeStudentDisciplineEntityMapper(IMapper mapper)
    {
        this.mapper = mapper;
    }

    public GradeStudentDisciplineDTO Map(GradeStudentDiscipline from) =>
        new GradeStudentDisciplineDTO(from.StudentId, from.DisciplineId, from.GradeId)
        {
            Id = from.Id, Grade = mapper.From(from.Grade)!.To<GradeDTO>()
        };
}