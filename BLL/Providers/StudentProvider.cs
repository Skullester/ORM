using BLL.DTO;
using BLL.Mapping;
using DAL.ORM;
using DAL.ORM.Repository;

namespace BLL.Providers;

public class StudentProvider : IStudentProvider
{
    private readonly IStudentRepository repo;
    private readonly IMapper mapper;

    public StudentProvider(IUnitWork unitWork, IMapper mapper)
    {
        repo = unitWork.StudentRepository;
        this.mapper = mapper;
    }

    public ILookup<int, StudentDTO> GetLookupByGroup()
    {
        return repo.GetAll()
            .ToLookup(x => x.GroupId, student => mapper.From(student)!.To<StudentDTO>());
    }

    public IEnumerable<StudentDTO> GetStudentsByGroup(int groupId)
    {
        return repo.GetAll()
            .Where(x => x.GroupId == groupId)
            .Select(x =>
                mapper.From(x)!
                    .To<StudentDTO>());
    }
}