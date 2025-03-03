using System.Collections;
using System.Reflection;

namespace DumpLibrary;

public static class ConsoleDumpExtensions
{
    public static void DumpToConsole<T>(this T obj, string? title = null, int maxDepth = 5)
    {
        if (obj == null)
        {
            Console.WriteLine("Object is null");
            return;
        }

        if (!string.IsNullOrEmpty(title))
        {
            Console.WriteLine($"\n{title}\n{new string('=', title.Length)}");
        }

        Type type = obj.GetType();

        // Se è una collezione, gestiscila in modo diverso
        if (obj is IEnumerable and not string)
        {
            DumpCollection((IEnumerable)obj);
            return;
        }

        // Per oggetti singoli
        var properties = type.GetProperties(BindingFlags.Public | BindingFlags.Instance)
                             .Where(p => p.CanRead)
                             .ToList();

        if (properties.Count == 0)
        {
            Console.WriteLine($"[{type.Name}]: {obj}");
            return;
        }

        // Trova la larghezza massima per le colonne
        int nameColumnWidth = Math.Max(properties.Max(p => p.Name.Length), 10);
        int typeColumnWidth = Math.Max(properties.Max(p => GetTypeName(p.PropertyType).Length), 10);
        int valueColumnWidth = 50; // larghezza massima per i valori

        // Intestazione tabella
        string headerFormat = $"| {{0,-{nameColumnWidth}}} | {{1,-{typeColumnWidth}}} | {{2,-{valueColumnWidth}}} |";
        string separator = $"+{new string('-', nameColumnWidth + 2)}+{new string('-', typeColumnWidth + 2)}+{new string('-', valueColumnWidth + 2)}+";

        Console.WriteLine(separator);
        Console.WriteLine(string.Format(headerFormat, "Property", "Type", "Value"));
        Console.WriteLine(separator);

        // Righe della tabella
        foreach (var property in properties)
        {
            object? value;
            try
            {
                value = property.GetValue(obj);
            }
            catch (Exception ex)
            {
                value = $"<Error: {ex.Message}>";
            }

            string valueStr = FormatValue(value ?? "", valueColumnWidth);
            string typeStr = GetTypeName(property.PropertyType);

            Console.WriteLine(string.Format(headerFormat, property.Name, typeStr, valueStr));
        }

        Console.WriteLine(separator);
    }

    // Implementazione specifica per Span<T>
    public static void DumpToConsole<T>(this Span<T> span, string? title = null, int maxDepth = 5) => span.ToArray().Dump(title, maxDepth);

    private static void DumpCollection(IEnumerable collection)
    {
        var items = collection.Cast<object>().ToList();
        if (items.Count == 0)
        {
            Console.WriteLine("Empty collection");
            return;
        }

        // Determina se tutti gli elementi sono dello stesso tipo e non sono oggetti complessi
        Type firstType = items[0]?.GetType();
        bool isSimpleCollection = items.All(i => i == null ||
                                         (i.GetType() == firstType && TypeHelper.IsSimpleType(firstType)));

        if (isSimpleCollection)
        {
            DumpSimpleCollection(items);
        }
        else
        {
            // Per collezioni di oggetti complessi
            int index = 0;
            foreach (var item in items)
            {
                if (item == null)
                {
                    Console.WriteLine($"[{index}] = null");
                }
                else
                {
                    item.DumpToConsole($"Item[{index}]");
                }
                index++;
            }
        }
    }

    private static void DumpSimpleCollection(List<object> items)
    {
        int indexWidth = items.Count.ToString().Length + 2;
        int valueWidth = 60;

        string headerFormat = $"| {{0,{indexWidth}}} | {{1,-{valueWidth}}} |";
        string separator = $"+{new string('-', indexWidth + 2)}+{new string('-', valueWidth + 2)}+";

        Console.WriteLine(separator);
        Console.WriteLine(string.Format(headerFormat, "Idx", "Value"));
        Console.WriteLine(separator);

        for (int i = 0; i < items.Count; i++)
        {
            string valueStr = FormatValue(items[i], valueWidth);
            Console.WriteLine(string.Format(headerFormat, i, valueStr));
        }

        Console.WriteLine(separator);
    }

    private static string FormatValue(object value, int maxWidth)
    {
        if (value == null) { return "<null>"; }

        if (value is string str) { return TruncateString($@"""{str}""", maxWidth); }            

        if (TypeHelper.IsSimpleType(value.GetType())) { return TruncateString(value.ToString() ?? "", maxWidth); }
            
        // Per oggetti complessi, mostra solo il tipo
        return $"[{GetTypeName(value.GetType())}]";
    }

    private static string GetTypeName(Type type)
    {
        if (type.IsGenericType)
        {
            var genericArgs = string.Join(", ", type.GetGenericArguments().Select(GetTypeName));
            return $"{type.Name.Split('`')[0]}<{genericArgs}>";
        }

        return type.Name;
    }

    private static string TruncateString(string value, int maxWidth)
    {
        if (string.IsNullOrEmpty(value)) { return value; }

        if (value.Length <= maxWidth) { return value; }

        return string.Concat(value.AsSpan(0, maxWidth - 3), "...");
    }
}
