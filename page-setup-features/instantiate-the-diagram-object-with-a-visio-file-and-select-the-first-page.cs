using System.IO;
using System;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        try
        {

            // Load the Visio file into a Diagram object using the file‑path constructor
            var diagram = new Diagram("sample.vsdx");

            // Select the first page (index 0) from the Pages collection
            var firstPage = diagram.Pages[0];

            // Example usage: output the name of the first page
            Console.WriteLine($"First page name: {firstPage.Name}");

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
