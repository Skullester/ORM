using BLL.DTO;
using BLL.Mapping;
using DAL.ORM;
using DAL.ORM.Repository;

namespace BLL.Providers;

public class DisciplineProvider : IDisciplineProvider
{
    private readonly IDisciplineRepository repo;
    private readonly IMapper mapper;

    public DisciplineProvider(IUnitWork unitWork, IMapper mapper)
    {
        repo = unitWork.DisciplineRepository;
        this.mapper = mapper;
    }

    public IEnumerable<DisciplineDTO> GetDisciplinesBySemester(int semesterId)
    {
        return repo.GetAll()
            .Where(x => x.SemesterId == semesterId)
            .Select(x =>
                mapper.From(x)!
                    .To<DisciplineDTO>());
    }

    public ILookup<int, DisciplineDTO> GetLookupBySemester()
    {
        return repo.GetAll()
            .ToLookup(x => x.SemesterId, x =>
                mapper.From(x)!
                    .To<DisciplineDTO>());
    }
}