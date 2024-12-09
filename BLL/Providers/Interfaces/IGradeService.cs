using BLL.DTO;

namespace BLL.Services;

public interface IGradeService
{
    void SetGradeTo(GradeInfoDTO gradeInfoDTO, string gradeName);

    GradeInfoDTO? GetGradeInfo(int studentId, int disciplineId);
}