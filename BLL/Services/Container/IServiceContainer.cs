namespace BLL.Services.Container;

public interface IServiceContainer
{
    IGradeService GradeService { get; }
    IStudentService StudentService { get; }
    IDisciplineService DisciplineService { get; }
}