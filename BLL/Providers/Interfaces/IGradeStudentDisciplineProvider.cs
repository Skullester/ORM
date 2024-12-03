using BLL.DTO;

namespace BLL.Providers;

public interface IGradeStudentDisciplineProvider
{
    GradeStudentDisciplineDTO? GetByStudentAndDiscipline(int studentId, int disciplineId);
}