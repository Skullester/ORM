namespace BLL.Services.Container;

public class ServiceContainer : IServiceContainer
{
    public IGradeService GradeService { get; }

    public IStudentService StudentService { get; }

    public IDisciplineService DisciplineService { get; }

    public ServiceContainer(IGradeService gradeService, IStudentService studentService,
        IDisciplineService disciplineService)
    {
        GradeService = gradeService;
        StudentService = studentService;
        DisciplineService = disciplineService;
    }
}