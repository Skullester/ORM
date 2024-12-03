namespace BLL.Providers.Container;

public class ProviderContainer : IProviderContainer
{
    public IGradeProvider GradeProvider { get; }

    public IStudentProvider StudentProvider { get; }

    public IDisciplineProvider DisciplineProvider { get; }
    public IGradeStudentDisciplineProvider GradeStudentDisciplineProvider { get; }

    public ProviderContainer(IGradeProvider gradeProvider, IStudentProvider studentProvider,
        IDisciplineProvider disciplineProvider, IGradeStudentDisciplineProvider gradeStudentDisciplineProvider)
    {
        GradeProvider = gradeProvider;
        StudentProvider = studentProvider;
        DisciplineProvider = disciplineProvider;
        GradeStudentDisciplineProvider = gradeStudentDisciplineProvider;
    }

    public void Dispose()
    {
    }
}