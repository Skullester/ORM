namespace BLL.Providers.Container;

public interface IProviderContainer
{
    IGradeProvider GradeProvider { get; }
    IStudentProvider StudentProvider { get; }
    IDisciplineProvider DisciplineProvider { get; }
    IGradeStudentDisciplineProvider GradeStudentDisciplineProvider { get; }
}