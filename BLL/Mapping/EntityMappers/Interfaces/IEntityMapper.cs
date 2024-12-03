namespace BLL.Mapping.Entities;

public interface IEntityMapper<in TFrom, out TTo> : IEntityMapper
{
    TTo Map(TFrom from);
}

public interface IEntityMapper;