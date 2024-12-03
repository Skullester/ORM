using BLL.DTO;
using BLL.Mapping;
using DAL.ORM;
using DAL.ORM.Repository;

namespace BLL.Distributors;

public class TeacherDTODistributor : DTODistributor<TeacherDTO>
{
    private readonly ITeacherRepository repo;

    public TeacherDTODistributor(IMapper mapper, IUnitWork unitWork) : base(mapper)
    {
        repo = unitWork.TeacherRepository;
    }


    public override IEnumerable<TeacherDTO> Get() => repo.GetAll().Select(x => mapper.From(x)!.To<TeacherDTO>());
}