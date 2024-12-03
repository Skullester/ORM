using BLL.Mapping.Entities;

namespace BLL.Mapping;

internal sealed class FromMapper<TFrom> : IFromMapper<TFrom>
{
    private static IMapperEntitiesContainer entitiesContainer = null!;
    public TFrom Value { get; }

    public FromMapper(TFrom value, IMapper mapper)
    {
        entitiesContainer ??= new MapperEntitiesEntitiesContainer(mapper);
        Value = value;
    }

    public TTo To<TTo>()
    {
        var entityMapper = entitiesContainer.Get<IEntityMapper<TFrom, TTo>>();
        return entityMapper.Map(Value);
    }
}