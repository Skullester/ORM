using DAL.ORM;

// ReSharper disable ReturnValueOfPureMethodIsNotUsed

namespace BLL.Cache;

public class Cacher : ICacher
{
    private readonly IUnitWork db;

    public Cacher(IUnitWork db)
    {
        this.db = db;
    }

    public void CacheAllEntities()
    {
        db.DisciplineRepository.GetAll().ToList();
        db.GradeRepository.GetAll().ToList();
        db.GroupRepository.GetAll().ToList();
        db.SemesterRepository.GetAll().ToList();
        db.GradeInfoRepository.GetAll().ToList();
        db.TeacherRepository.GetAll().ToList();
        db.StudentRepository.GetAll().ToList();
        db.PostRepository.GetAll().ToList();
        db.PersonRepository.GetAll().ToList();
    }
}