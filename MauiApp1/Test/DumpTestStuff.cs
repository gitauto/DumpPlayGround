using DumpLibrary;
using System.Data;
using MauiApp1.Models;

namespace MauiApp1.Test;

internal class DumpTestStuff
{
    public static void DumpTest()
    {
        string? buffer = null;
        buffer.Dump("null string");

        TimeZoneInfo.Local.Dump();

        (-10.76).Dump("Number");

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

    static DataTable GetTable()
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
}
