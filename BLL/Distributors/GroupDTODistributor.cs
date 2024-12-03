using BLL.DTO;
using BLL.Mapping;
using DAL.ORM;
using DAL.ORM.Repository;

namespace BLL.Distributors;

public class GroupDTODistributor : DTODistributor<GroupDTO>
{
    private readonly IGroupRepository repo;

    public GroupDTODistributor(IMapper mapper, IUnitWork unitWork) : base(mapper)
    {
        repo = unitWork.GroupRepository;
    }

    public override IEnumerable<GroupDTO> Get()
    {
        var groups = repo.GetAll();
        var groupsDto = groups.Select(group => mapper.From(group)!.To<GroupDTO>());
        return groupsDto;
    }
}