namespace BLL.Mapping;

public interface IMapper
{
    IFromMapper<T>? From<T>(T value);
}