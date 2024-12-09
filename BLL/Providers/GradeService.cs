using BLL.DTO;
using BLL.Mapping;
using DAL.Entities;
using DAL.ORM;
using DAL.ORM.Repository;

namespace BLL.Services;

public class GradeService : IGradeService
{
    private readonly IUnitWork db;
    private readonly IGradeRepository gradeRepository;
    private readonly IGradeInfoRepository gradeInfoRepository;
    private readonly IMapper mapper = new Mapper();

    public GradeService(IUnitWork db)
    {
        this.db = db;
        gradeRepository = db.GradeRepository;
        gradeInfoRepository = db.GradeInfoRepository;
    }

    public void SetGradeTo(GradeInfoDTO gradeInfoDTO, string gradeName)
    {
        var grade = gradeRepository.GetByName(gradeName)!;
        gradeInfoDTO.Grade = mapper.From(grade)!.To<GradeDTO>();
        var gradeInfo = gradeInfoRepository.GetBy(gradeInfoDTO.Id);
        if (gradeInfo is null)
        {
            gradeInfo = mapper.From(gradeInfoDTO)!.To<GradeInfo>();
            gradeInfoRepository.Add(gradeInfo);
        }

        gradeInfo.Grade = grade;
        db.Save();
    }

    public GradeInfoDTO? GetGradeInfo(int studentId, int disciplineId)
    {
        var gradeInfo = gradeInfoRepository.GetAll()
            .FirstOrDefault(x => x.StudentId == studentId && x.DisciplineId == disciplineId);
        return mapper.From(gradeInfo)?.To<GradeInfoDTO>();
    }
}