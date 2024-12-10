using BLL.DTO;

namespace BLL.Services;

public interface IStudentService
{
    ILookup<int, StudentDTO> GetLookupByGroup();
    IEnumerable<StudentDTO> GetStudentsByGroup(int groupId);
}