using BLL.DTO;

namespace BLL.Providers;

public interface IGradeProvider
{
    void SetGradeTo(GradeStudentDisciplineDTO gsdDto, string gradeName);
}