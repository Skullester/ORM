using BLL.DTO;
using BLL.Mapping;
using DAL.Entities;
using DAL.ORM;
using DAL.ORM.Repository;

namespace BLL.Providers;

public class GradeStudentDisciplineProvider : IGradeStudentDisciplineProvider
{
    private readonly IGradeStudentDisciplineRepository repo;
    private readonly IMapper mapper;

    public GradeStudentDisciplineProvider(IUnitWork unitWork, IMapper mapper)
    {
        repo = unitWork.GradeStudentDisciplineRepository;
        this.mapper = mapper;
    }

    public GradeStudentDisciplineDTO? GetByStudentAndDiscipline(int studentId, int disciplineId)
    {
        var gsd = repo.GetAll()
            .FirstOrDefault(x => x.StudentId == studentId && x.DisciplineId == disciplineId);
        return mapper.From(gsd)?.To<GradeStudentDisciplineDTO>();
    }
}