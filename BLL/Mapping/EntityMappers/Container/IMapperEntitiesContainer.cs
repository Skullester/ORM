using BLL.Mapping.Entities;

namespace BLL.Mapping;

public interface IMapperEntitiesContainer
{
    TEntity Get<TEntity>() where TEntity : IEntityMapper;
}