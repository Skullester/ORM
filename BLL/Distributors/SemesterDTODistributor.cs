using BLL.DTO;
using BLL.Mapping;
using DAL.ORM;
using DAL.ORM.Repository;

namespace BLL.Distributors;

public class SemesterDTODistributor : DTODistributor<SemesterDTO>
{
    private readonly ISemesterRepository repo;

    public SemesterDTODistributor(IMapper mapper, IUnitWork unitWork) : base(mapper)
    {
        repo = unitWork.SemesterRepository;
    }


    public override IEnumerable<SemesterDTO> Get()
    {
        var semesters = repo.GetAll();
        var semestersDTO = semesters.Select(group => mapper.From(group)!.To<SemesterDTO>());
        return semestersDTO;
    }
}