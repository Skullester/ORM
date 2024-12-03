using BLL.DTO;
using BLL.Extensions;
using BLL.Mapping;
using DAL.Entities;
using DAL.ORM;
using DAL.ORM.Repository;

namespace BLL.Providers;

public class GradeProvider : IGradeProvider
{
    private readonly IUnitWork db;
    private readonly IGradeRepository gradeRepository;
    private readonly IGradeStudentDisciplineRepository gsdRepository;
    private readonly IMapper mapper = new Mapper();

    public GradeProvider(IUnitWork db)
    {
        this.db = db;
        gradeRepository = db.GradeRepository;
        gsdRepository = db.GradeStudentDisciplineRepository;
    }

    public void SetGradeTo(GradeStudentDisciplineDTO gsdDto, string gradeName)
    {
        var grade = gradeRepository.GetByName(gradeName)!;
        gsdDto.Grade = mapper.From(grade)!.To<GradeDTO>();
        var gsd = gsdRepository.GetBy(gsdDto.Id);
        if (gsd is null)
        {
            gsd = mapper.From(gsdDto)!.To<GradeStudentDiscipline>();
            gsdRepository.Add(gsd);
        }

        gsd.Grade = grade;
        db.Save();
    }
}