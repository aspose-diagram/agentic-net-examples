using System.IO;
using System;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        try
        {

            // Load the Visio VSDX file
            Diagram diagram = new Diagram("input.vsdx");

            // Retrieve the total number of pages in the diagram
            int totalPages = diagram.Pages.Count;

            // Output the page count
            Console.WriteLine($"Total pages in the diagram: {totalPages}");

            // Clean up resources
            diagram.Dispose();

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
