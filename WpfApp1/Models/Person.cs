namespace WpfApp1.Models;

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