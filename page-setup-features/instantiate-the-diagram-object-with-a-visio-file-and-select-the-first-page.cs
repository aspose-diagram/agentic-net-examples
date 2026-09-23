using System.IO;
using System;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        try
        {

            // Load the Visio file into a Diagram object
            Diagram diagram = new Diagram("input.vsdx");

            // Select the first page (index 0) from the diagram
            Page firstPage = diagram.Pages[0];

            // Example: output the name of the first page
            Console.WriteLine("First page name: " + firstPage.Name);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
