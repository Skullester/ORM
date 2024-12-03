using BLL.DTO;
using BLL.Extensions;

namespace BLL.Distributors;

public class Distributor : IDistributor
{
    private readonly DTODistributor<GroupDTO> groupsDistributor;
    private readonly DTODistributor<TeacherDTO> teachersDistributor;
    private readonly DTODistributor<SemesterDTO> semesterDistributor;

    public Distributor(DTODistributor<GroupDTO> groupsDistributor,
        DTODistributor<TeacherDTO> teachersDistributor, DTODistributor<SemesterDTO> semesterDistributor)
    {
        this.groupsDistributor = groupsDistributor;
        this.teachersDistributor = teachersDistributor;
        this.semesterDistributor = semesterDistributor;
    }

    public IEnumerable<T> Get<T>()
    {
        var dtoDistributor = GetType().Get<DTODistributor<T>>(this);
        return dtoDistributor.Get();
    }
}