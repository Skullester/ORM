using BLL.DTO;

namespace BLL.Providers;

public interface IDisciplineProvider
{
    IEnumerable<DisciplineDTO> GetDisciplinesBySemester(int semesterId);
    ILookup<int, DisciplineDTO> GetLookupBySemester();
}