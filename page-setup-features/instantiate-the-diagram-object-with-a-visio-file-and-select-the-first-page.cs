using System.IO;
using System;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        try
        {

            // Load the Visio diagram from a file
            Diagram diagram = new Diagram("sample.vsdx");

            // Select the first page (index 0)
            Page firstPage = diagram.Pages[0];

            // Example usage: output the page name
            Console.WriteLine("First page name: " + firstPage.Name);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
