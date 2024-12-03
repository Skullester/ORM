// ReSharper disable ParameterHidesMember

namespace BLL.Mapping;

public class DefaultMapper
{
    public static IMapper Mapper { get; set; }

    public static IFromMapper<T>? From<T>(T value) => Mapper.From(value);

    static DefaultMapper()
    {
        Mapper = new Mapper();
    }
}