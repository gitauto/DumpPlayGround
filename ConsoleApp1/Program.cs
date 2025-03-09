using System.Text;
using TestDump;

// Ottieni l'encoding corrente della console
Encoding inputEncoding = Console.InputEncoding;
Encoding outputEncoding = Console.OutputEncoding;

Console.WriteLine($"Input Encoding: {inputEncoding.EncodingName}");
Console.WriteLine($"Output Encoding: {outputEncoding.EncodingName}");

// Esempio di modifica dell'encoding (ad esempio, UTF-8)
Console.OutputEncoding = Encoding.UTF8;
Console.WriteLine($"Nuovo Output Encoding: {Console.OutputEncoding.EncodingName}");

// Test con un carattere Unicode
Console.WriteLine("Caffè ☕"); // Il simbolo ☕ è Unicode

DumpTestStuff.ConsoleDumpTestBaseTypes();