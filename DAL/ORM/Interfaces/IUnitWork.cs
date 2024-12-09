using DAL.ORM.Repository;

namespace DAL.ORM;

public interface IUnitWork : IDisposable
{
    IDisciplineRepository DisciplineRepository { get; }
    IGradeRepository GradeRepository { get; }
    IGradeInfoRepository GradeInfoRepository { get; }
    IGroupRepository GroupRepository { get; }
    IPersonRepository PersonRepository { get; }
    IPostRepository PostRepository { get; }
    ISemesterRepository SemesterRepository { get; }
    IStudentRepository StudentRepository { get; }
    ITeacherRepository TeacherRepository { get; }
    void Save();
}