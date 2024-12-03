using DAL.ORM.Repository;

namespace DAL.ORM;

public class EFUnitWork : IUnitWork
{
    private readonly EFContext context;

    public EFUnitWork()
    {
        context = new EFContext();
        InitializeRepositories();
    }

    public IDisciplineRepository DisciplineRepository { get; private set; } = null!;

    public IGradeRepository GradeRepository { get; private set; } = null!;

    public IGradeStudentDisciplineRepository GradeStudentDisciplineRepository { get; private set; } = null!;

    public IGroupRepository GroupRepository { get; private set; } = null!;

    public IPersonRepository PersonRepository { get; private set; } = null!;

    public IPostRepository PostRepository { get; private set; } = null!;

    public ISemesterRepository SemesterRepository { get; private set; } = null!;

    public IStudentRepository StudentRepository { get; private set; } = null!;

    public ITeacherRepository TeacherRepository { get; private set; } = null!;

    public void Save() => context.SaveChanges();

    private void InitializeRepositories()
    {
        DisciplineRepository = new EFDisciplineRepository(context);
        GradeRepository = new EFGradeRepository(context);
        GradeStudentDisciplineRepository = new EFGradeStudentDisciplineRepository(context);
        GroupRepository = new EFGroupRepository(context);
        PersonRepository = new EFPersonRepository(context);
        PostRepository = new EFPostRepository(context);
        SemesterRepository = new EFSemesterRepository(context);
        StudentRepository = new EFStudentRepository(context);
        TeacherRepository = new EFTeacherRepository(context);
    }

    void IDisposable.Dispose() => context.Dispose();
}