using System.Reflection;

namespace Infrastructure;

public static class Extensions
{
    public static FieldInfo? GetFieldInfoIListWith<T>(this Type type)
    {
        return type.GetFields(BindingFlags.Instance | BindingFlags.NonPublic)
            .FirstOrDefault(x => x.FieldType == typeof(IList<T>));
    }

    public static IList<T> GetIListWithOf<T>(this Type type, object obj)
    {
        var fi = GetFieldInfoIListWith<T>(type);
        return fi?.GetValue(obj) as IList<T> ??
               throw new ArgumentException($"IList with {nameof(T)} parameter has not been found");
    }
}