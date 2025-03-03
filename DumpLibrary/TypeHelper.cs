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
            return $"{baseName}<{string.Join(",", genericArgs)}>";
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

    public static bool IsNumericType(Type type)
    {
        if (type == null) return false;

        // Gestione dei tipi nullable
        if (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(Nullable<>))
        {
            type = Nullable.GetUnderlyingType(type) ?? type;
        }

        // Controlla se il tipo è uno dei tipi numerici primitivi
        return Type.GetTypeCode(type) switch
        {
            TypeCode.Byte or 
            TypeCode.SByte or 
            TypeCode.UInt16 or 
            TypeCode.UInt32 or 
            TypeCode.UInt64 or 
            TypeCode.Int16 or 
            TypeCode.Int32 or 
            TypeCode.Int64 or       // <- Include long (int64)
            TypeCode.Decimal or 
            TypeCode.Double or 
            TypeCode.Single => true,
            _ => false,
        };
    }

    public static bool IsSimpleType(Type type)
    {
        // Ottieni il tipo sottostante se è un nullable
        var underlyingType = Nullable.GetUnderlyingType(type);

        // Se è un nullable, controlla il tipo sottostante
        if (underlyingType != null)
        {
            return IsSimpleType(underlyingType);
        }

        // The primitive types are: Boolean, Byte, SByte, Int16, UInt16, Int32, UInt32, Int64, UInt64, IntPtr, UIntPtr, Char, Double, and Single.
        // https://learn.microsoft.com/en-us/dotnet/api/system.type.isprimitive?view=net-9.0&devlangs=csharp&f1url=%3FappId%3DDev17IDEF1%26l%3DEN-US%26k%3Dk(System.Type.IsPrimitive)%3Bk(DevLang-csharp)%26rd%3Dtrue

        // Altrimenti controlla il tipo direttamente
        return type.IsPrimitive ||
               type.IsEnum ||
               type == typeof(string) ||
               type == typeof(decimal) ||
               type == typeof(DateTime) ||
               type == typeof(TimeSpan) ||
               type == typeof(DateTimeOffset) ||
               type == typeof(Guid);
    }

    public static string GetCleanTypeName2(Type type)
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
}
