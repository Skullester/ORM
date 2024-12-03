namespace BLL.Mapping;

public interface IFromMapper<out TValue>
{
    TValue Value { get; }
    TTo To<TTo>();
}