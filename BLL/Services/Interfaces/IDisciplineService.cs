using BLL.DTO;

namespace BLL.Services;

public interface IDisciplineService
{
    IEnumerable<DisciplineDTO> GetDisciplinesBySemester(int semesterId);
    ILookup<int, DisciplineDTO> GetLookupBySemester();
}