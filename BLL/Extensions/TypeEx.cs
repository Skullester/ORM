using System.Reflection;

namespace BLL.Extensions;

internal static class TypeEx
{
    public static TTarget Get<TTarget>(this Type type, object obj)
    {
        var field = GetFieldOf<TTarget>(type);
        return (TTarget)field.GetValue(obj)!;
    }

    private static FieldInfo GetFieldOf<TTarget>(Type type)
    {
        var fields = type.GetFields(BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.DeclaredOnly);
        var targetType = typeof(TTarget);
        var field = fields.FirstOrDefault(x => x.FieldType == targetType);
        if (field is null)
            throw new ArgumentException($"There is no field with target type: {targetType} of {type.Name}");
        return field;
    }
}