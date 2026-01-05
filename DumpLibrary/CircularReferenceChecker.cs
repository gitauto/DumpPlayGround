using System.Diagnostics;
using System.Reflection;

namespace DumpLibrary;

internal class CircularReferenceChecker
{
    private readonly HashSet<object> _visited = [];
    private readonly Stack<object> _currentPath = new();

    public bool HasCircularReference(object root)
    {
        if (root == null) { return false; }
        _visited.Clear();
        _currentPath.Clear();
        return DetectCycle(root);
    }

    private bool DetectCycle(object obj)
    {
        // Ignora tipi che non possono contenere riferimenti circolari
        if (IsNonReferenceType(obj)) { return false; }

        // Controlla se l'oggetto è già stato visitato nel percorso corrente
        if (_currentPath.Contains(obj)) { return true; }

        // Controlla se l'oggetto è già stato completamente esplorato
        if (_visited.Contains(obj)) { return false; }

        // Marca l'oggetto come parte del percorso corrente
        _currentPath.Push(obj);

        var members = DumpExtensions.GetPublicMembers(obj.GetType());

        foreach (var member in members)
        {
            try
            {
                object? value = member is FieldInfo info ? info.GetValue(obj) : ((PropertyInfo)member).GetValue(obj, null);
                if (value != null && DetectCycle(value)) { return true; }
            }
            catch (Exception ex)
            {
                // Ignora eventuali eccezioni legate a proprietà non accessibili
                Debug.WriteLine(ex.Message);
            }
        }

        // Rimuovi l'oggetto dal percorso corrente
        _currentPath.Pop();

        // Marca l'oggetto come visitato
        _visited.Add(obj);

        return false;
    }

    private static bool IsNonReferenceType(object obj)
    {
        var type = obj.GetType();

        // Tipi valore (esclusi i nullable)
        if (type.IsValueType && !IsNullableType(type)) { return true; }

        // Stringhe (immutabili)
        if (type == typeof(string)) { return true; }

        // Enumerazioni
        if (type.IsEnum) { return true; }

        // Delegati
        if (typeof(Delegate).IsAssignableFrom(type)) { return true; }

        // Tipi Nullable<T>
        if (IsNullableType(type))
        {
            // Controlla il tipo sottostante del nullable
            var underlyingType = Nullable.GetUnderlyingType(type);
            if (underlyingType != null && underlyingType.IsValueType) { return true; }
        }

        return false;
    }

    public static bool IsNullableType(Type type) => type.IsGenericType && type.GetGenericTypeDefinition() == typeof(Nullable<>);
}
