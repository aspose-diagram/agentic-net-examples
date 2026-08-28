using System.IO;
using System;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        try
        {

            // Path to the existing VDX file
            string vdxPath = @"C:\Path\To\YourDiagram.vdx";

            // Load the diagram using the constructor that accepts a file name
            Diagram diagram = new Diagram(vdxPath);

            // Validate the number of pages in the diagram
            int pageCount = diagram.Pages.Count;

            // Example validation: ensure there is at least one page
            if (pageCount > 0)
            {
                Console.WriteLine($"Diagram loaded successfully. Page count: {pageCount}");
            }
            else
            {
                Console.WriteLine("Diagram loaded, but it contains no pages.");
            }

            // Dispose the diagram when done
            diagram.Dispose();

        }
        catch (System.IO.DirectoryNotFoundException ex)
        {
            Console.Error.WriteLine($"[DirectoryNotFoundException] {ex.Message}");
        }
    }
}
