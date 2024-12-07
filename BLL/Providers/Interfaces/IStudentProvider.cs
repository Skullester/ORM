using BLL.DTO;

namespace BLL.Providers;

public interface IStudentProvider
{
    ILookup<int, StudentDTO> GetLookupByGroup();
    IEnumerable<StudentDTO> GetStudentsByGroup(int groupId);
}