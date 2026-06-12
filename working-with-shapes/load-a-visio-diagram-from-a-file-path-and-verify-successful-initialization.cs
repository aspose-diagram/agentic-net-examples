using System.IO;
using System;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        try
        {

            // Path to the Visio file to be loaded
            string filePath = @"C:\Path\To\Your\Diagram.vsdx";

            // Load the diagram using the constructor that accepts a file name
            Diagram diagram = new Diagram(filePath);

            // Verify that the diagram was initialized correctly
            // Check that the diagram object is not null and contains at least one page
            if (diagram != null && diagram.Pages.Count > 0)
            {
                Console.WriteLine("Diagram loaded successfully. Page count: " + diagram.Pages.Count);
            }
            else
            {
                Console.WriteLine("Failed to load diagram or diagram contains no pages.");
            }

            // Clean up resources
            diagram.Dispose();

        }
        catch (System.IO.DirectoryNotFoundException ex)
        {
            Console.Error.WriteLine($"[DirectoryNotFoundException] {ex.Message}");
        }
    }
}
