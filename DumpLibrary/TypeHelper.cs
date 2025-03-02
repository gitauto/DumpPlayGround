namespace DumpLibrary;

public static class TypeHelper
{
    public static string GetCleanTypeName<T>(T obj)
    {
        if (obj is null) { return "<null>"; }

        return GetCleanTypeName(obj.GetType());
    }

    private static string GetCleanTypeName(Type type)
    {
        // Se il tipo implementa IEnumerable<T> e è un iteratore LINQ, trattalo come IEnumerable<T>
        Type? enumerableType = GetImplementedGenericInterface(type, typeof(IEnumerable<>));

        Console.WriteLine($"Type Name: {type.Name} | Namespace: {type.Namespace} | IsGenericType: {type.IsGenericType} | IsLinqIterator(type): {IsLinqIterator(type)}");

        if (enumerableType != null && IsLinqIterator(type))
        {
            Type elementType = enumerableType.GetGenericArguments()[0];
            return $"IEnumerable<{GetCleanTypeName(elementType)}>";
        }

        // Gestione dei tipi generici definiti
        if (type.IsGenericType)
        {
            string baseName = type.Name[..type.Name.IndexOf('`')];
            string[] genericArgs = [.. type.GetGenericArguments().Select(t => GetCleanTypeName(t))];
            return $"{baseName}<{string.Join(", ", genericArgs)}>";
        }

        // Restituisce il nome del tipo per i tipi non generici
        return type.Name;
    }

    // Controlla se il tipo appartiene allo spazio dei nomi System.Linq.Enumerable
    // e se il nome del tipo inizia con "Where" o altri prefissi noti per gli iteratori LINQ
    private static bool IsLinqIterator(Type type) => type.Namespace == "System.Linq" && (type.Name.Contains("Where") || type.Name.Contains("Enumerable"));

    private static Type? GetImplementedGenericInterface(Type type, Type genericInterface)
    {
        foreach (Type interfaceType in type.GetInterfaces())
        {
            if (interfaceType.IsGenericType && interfaceType.GetGenericTypeDefinition() == genericInterface)
            {
                return interfaceType;
            }
        }
        return null;
    }
}
