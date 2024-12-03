namespace BLL.Mapping;

public class Mapper : IMapper
{
    public IFromMapper<T>? From<T>(T? value) => value is null ? null : new FromMapper<T>(value, this);
}