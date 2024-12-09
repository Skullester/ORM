using BLL.Mapping;

namespace BLL.Distributors;

public abstract class DTODistributor<T>
{
    protected readonly IMapper mapper;

    protected DTODistributor(IMapper mapper)
    {
        this.mapper = mapper;
    }

    public abstract IEnumerable<T> Get();
}