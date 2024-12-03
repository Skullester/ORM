using BLL.DTO;

namespace BLL.Providers;

public interface IStudentProvider
{
    ILookup<int, StudentDTO> GetStudentsByGroup();
    IEnumerable<StudentDTO> GetStudentsByGroup(int groupId);
}