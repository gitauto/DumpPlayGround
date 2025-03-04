using DumpLibrary;
using System.Data;
using System.Diagnostics;

namespace TestDump;

public class DumpTestStuff
{
    public delegate void Notification(string message);

    public static void DumpTest()
    {
        string? buffer = null;
        buffer.Dump("null string");

        new Guid().Dump("Guid");

        (-10.76).Dump("Number");

        new { Name = "Avatar", Description = "This is a film" }.Dump("Anonymous types");

        TimeZoneInfo.Local.Dump();

        string[] stringArray = ["Luigi", "Mario", "Arturo"];
        stringArray.Dump("stringArray");

        int[] intArray = [7, 4, 32, 81];
        intArray.Dump("intArray");

        var persona = new { Nome = "Mario", Età = 30, Amici = new[] { "Luigi", "Peach" } };
        persona.Dump("Persona");

        var listStrings = new List<string> { "ciao", "come", "stai" };
        listStrings.Dump();

        var numeri = new List<int> { 1, 2, 3, 4, 5 };
        numeri.Dump("Numeri");

        var filtrati = numeri.Where(n => n > 2);
        filtrati.Dump("Filtrati");

        var somma = filtrati.Sum();
        somma.Dump("Somma");

        var alice = new Person2 { Name = "Alice" };
        var bob = new Person2 { Name = "Bob" };
        alice.Friend = bob;
        bob.Friend = alice;
        alice.Dump("Circular Reference Example");

        // Riferimento circolare
        var moaid1 = new Person
        {
            FirstName = "Moaid",
            LastName = "Hathot",
            Profession = Profession.Software,
            _fooField = "Hello"

        };
        var haneeni1 = new Person
        {
            FirstName = "Haneeni",
            LastName = "Shibli",
            Profession = Profession.Health,
            _fooField = "Bye"
        };
        moaid1.Spouse = haneeni1;
        haneeni1.Spouse = moaid1;

        moaid1.Dump("moaid1");
        haneeni1.Dump("haneeni1");

        new[] { moaid1, haneeni1 }.Dump();

        var obj = new MyClass();
        obj.Dump();

        var table = GetTable();
        table.Dump("DataTable");
        table.Rows.Dump("DataRowCollection");

        // Dataset
        // Create 2 DataTable instances.
        var table1 = new DataTable("patients");
        table1.Columns.Add("name");
        table1.Columns.Add("id");
        table1.Rows.Add("sam", 1);
        table1.Rows.Add("mark", 2);

        var table2 = new DataTable("medications");
        table2.Columns.Add("id");
        table2.Columns.Add("medication");
        table2.Rows.Add(1, "atenolol");
        table2.Rows.Add(2, "amoxicillin");

        // Create a DataSet and put both tables in it.
        var dataSet = new DataSet("office");
        dataSet.Tables.Add(table1);
        dataSet.Tables.Add(table2);

        // Visualize DataSet.
        dataSet.Dump();
    }

    private static DataTable GetTable()
    {
        // Here we create a DataTable with four columns.
        var table = new DataTable();
        table.Columns.Add("Dosage", typeof(int));
        table.Columns.Add("Drug", typeof(string));
        table.Columns.Add("Patient", typeof(string));
        table.Columns.Add("Date", typeof(DateTime));

        // Here we add five DataRows.
        table.Rows.Add(25, "Indocin", "David", DateTime.Now);
        table.Rows.Add(50, "Enebrel", "Sam", DateTime.Now);
        table.Rows.Add(10, "Hydralazine", "Christoff", DateTime.Now);
        table.Rows.Add(21, "Combivent", "Janet", DateTime.Now);
        table.Rows.Add(100, "Dilantin", "Melanie", DateTime.Now);
        return table;
    }

    public static void DumpTestBaseTypes()
    {
        // 1. Tipi primitivi
        int integer = 42;
        double floatingPoint = 3.14;
        bool boolean = true;
        char character = 'A';
        string text = "Hello, World!";
        DateTime date = DateTime.Now;
        decimal number = 123.45m;
        Guid guid = Guid.NewGuid();

        Debug.WriteLine("Dumping primitive types:");

        integer.Dump("Integer");
        floatingPoint.Dump("Floating Point");
        boolean.Dump("Boolean");
        character.Dump("Character");
        text.Dump("String");
        date.Dump("DateTime");
        number.Dump("Decimal");
        guid.Dump("GUID");

        // 2. Tipo nullo
        string? nullString = null;
        nullString.Dump("Null String");

        // 3. Classi personalizzate
        var person = new Person2 { Name = "John Doe", Age = 30 };
        person.Dump("Person Object");

        // 4. Record
        var recordExample = new RecordExample("Record", 123);
        recordExample.Dump("Record Example");

        // 5. Tuple
        var tupleExample = (Name: "Tuple", Value: 456);
        tupleExample.Dump("Tuple Example");

        // 6. Oggetto anonimo
        var anonymousObject = new { Key = "Anonymous", Value = 789 };
        anonymousObject.Dump("Anonymous Object");

        // 7. Liste
        var listExample = new List<int> { 1, 2, 3, 4, 5 };
        listExample.Dump("List of Integers");

        // 8. Dizionari
        var dictionaryExample = new Dictionary<string, string>
        {
            { "Key1", "Value1" },
            { "Key2", "Value2" }
        };
        dictionaryExample.Dump("Dictionary Example");

        // 9. Code
        var queueExample = new Queue<string>();
        queueExample.Enqueue("First");
        queueExample.Enqueue("Second");
        queueExample.Enqueue("Third");
        queueExample.Dump("Queue Example");

        // 10. Set
        var setExample = new HashSet<int> { 10, 20, 30, 40 };
        setExample.Dump("Set Example");

        // 11. Enumerazione
        var dayOfWeek = DayOfWeek.Monday;
        dayOfWeek.Dump("Enum Example");

        // 12. Struct
        Point point = new() { X = 10, Y = 20 };
        point.Dump("Struct");

        // Interfaccia
        IShape shape = new Circle(3);
        shape.Dump("IShape");

        ((Circle)shape).Dump("Circle");

        // Delegato
        Notification notify = Console.WriteLine;
        // TODO: Il delegato non termina mai
        //notify.Dump("Notification");

        // Array
        int[] numbers = { 1, 2, 3 };
        numbers.Dump("Array");

        // Oggetto
        object obj = new();
        obj.Dump("object");

        // Dynamic
        //dynamic dyn = "Dynamic";

        // Tipo nullable
        int? nullableInt = null;
        nullableInt.Dump("nullableInt");

        // Span
        Span<int> span = stackalloc int[5] { 1, 2, 3, 4, 5 };
        span.Dump("Span");

        // Task
        Task<int> task = Task.FromResult(42);
        task.Dump();

        Debug.WriteLine("All tests completed.");
    }

    public static void ConsoleDumpTestBaseTypes()
    {
        // 1. Tipi primitivi
        int integer = 42;
        double floatingPoint = 3.14;
        bool boolean = true;
        char character = 'A';
        string text = "Hello, World!";
        DateTime date = DateTime.Now;
        decimal number = 123.45m;
        Guid guid = Guid.NewGuid();

        Console.WriteLine("Dumping primitive types:");
        integer.DumpToConsole("Integer");
        floatingPoint.DumpToConsole("Floating Point");
        boolean.DumpToConsole("Boolean");
        character.DumpToConsole("Character");
        text.DumpToConsole("String");
        date.DumpToConsole("DateTime");
        number.DumpToConsole("Decimal");
        guid.DumpToConsole("GUID");

        // 2. Tipo nullo
        string? nullString = null;
        nullString.DumpToConsole("Null String");

        // 3. Classi personalizzate
        var person = new Person2 { Name = "John Doe", Age = 30 };
        person.DumpToConsole("Person Object");

        // 4. Record
        var recordExample = new RecordExample("Record", 123);
        recordExample.DumpToConsole("Record Example");

        // 5. Tuple
        var tupleExample = (Name: "Tuple", Value: 456);
        tupleExample.DumpToConsole("Tuple Example");

        // 6. Oggetto anonimo
        var anonymousObject = new { Key = "Anonymous", Value = 789 };
        anonymousObject.DumpToConsole("Anonymous Object");

        // 7. Liste
        var listExample = new List<int> { 1, 2, 3, 4, 5 };
        listExample.DumpToConsole("List of Integers");

        // 8. Dizionari
        var dictionaryExample = new Dictionary<string, string>
        {
            { "Key1", "Value1" },
            { "Key2", "Value2" }
        };
        dictionaryExample.DumpToConsole("Dictionary Example");

        // 9. Code
        var queueExample = new Queue<string>();
        queueExample.Enqueue("First");
        queueExample.Enqueue("Second");
        queueExample.Enqueue("Third");
        queueExample.DumpToConsole("Queue Example");

        // 10. Set
        var setExample = new HashSet<int> { 10, 20, 30, 40 };
        setExample.DumpToConsole("Set Example");

        // 11. Enumerazione
        var dayOfWeek = DayOfWeek.Monday;
        dayOfWeek.DumpToConsole("Enum Example");

        // 12. Struct
        Point point = new() { X = 10, Y = 20 };
        point.DumpToConsole("Struct");

        // Interfaccia
        IShape shape = new Circle(3);
        shape.Dump("IShape");

        ((Circle)shape).Dump("Circle");

        // Delegato
        Notification notify = Console.WriteLine;
        notify.Dump("Notification");

        // Array
        int[] numbers = { 1, 2, 3 };
        numbers.DumpToConsole("Array");

        // Oggetto
        object obj = new();
        obj.DumpToConsole("object");

        // Dynamic
        //dynamic dyn = "Dynamic";

        // Tipo nullable
        int? nullableInt = null;
        nullableInt.DumpToConsole("nullableInt");

        // Span
        Span<int> span = stackalloc int[5] { 1, 2, 3, 4, 5 };
        span.DumpToConsole("Span");

        // Task
        Task<int> task = Task.FromResult(42);
        task.DumpToConsole();

        Console.WriteLine("All tests completed.");
    }

    // Record
    public record RecordExample(string Name, int Id);

    // Enumerazione
    public enum DayOfWeek
    {
        Sunday,
        Monday,
        Tuesday,
        Wednesday,
        Thursday,
        Friday,
        Saturday
    }

    // Definizione dell'interfaccia IShape
    public interface IShape
    {
        // Metodo per disegnare la forma
        void Draw();
    }

    // Implementazione della classe Circle
    public class Circle(double radius) : IShape
    {
        // Proprietà per il raggio del cerchio
        public double Radius { get; set; } = radius;

        // Implementazione del metodo Draw() definito dall'interfaccia IShape
        public void Draw() => Console.WriteLine($"Drawing a circle with radius {Radius}");
    }

    // Definizione della struttura Point
    public struct Point(int x, int y)
    {
        // Proprietà pubbliche per le coordinate X e Y
        public int X { get; set; } = x;
        public int Y { get; set; } = y;

        // Override del metodo ToString() per una rappresentazione testuale
        public override readonly string ToString() => $"({X}, {Y})";
    }

    public record class Person
    {
        public string? _fooField = "hello";

        public required string FirstName { get; set; }
        public required string LastName { get; set; }

        public Person? Spouse { get; set; }

        public Profession Profession { get; set; }

        public static string? FooMethod(int a) => a == 1 ? "a=1" : "a!=1";
    }

    public enum Profession
    {
        Software,
        Health
    };

    public class Person2
    {
        public required string Name { get; set; }
        public Person2? Friend { get; set; }
        public int Age { get; set; }
    }

    public class MyClass
    {
        public int Field1;
        public string Field2 = "";

        public int Property1 { get; set; }
        public string Property2 { get; set; } = "";

        private readonly int PrivateField = 3;

        private string PrivateProperty { get; set; } = "";

        private void UseField() => PrivateField.ToString();
    }
}
