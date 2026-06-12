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
            string visioFilePath = @"C:\Path\To\Your\Diagram.vsdx";

            // Load the Visio file using the Diagram(string) constructor
            Diagram diagram = new Diagram(visioFilePath);

            // Verify that the diagram was initialized successfully
            if (diagram == null)
            {
                Console.WriteLine("Failed to create Diagram object.");
                return;
            }

            // Check that at least one page was loaded
            if (diagram.Pages == null || diagram.Pages.Count == 0)
            {
                Console.WriteLine("Diagram loaded, but no pages were found.");
            }
            else
            {
                Console.WriteLine($"Diagram loaded successfully. Pages count: {diagram.Pages.Count}");
            }

            // Optional: clean up resources
            diagram.Dispose();

        }
        catch (System.IO.DirectoryNotFoundException ex)
        {
            Console.Error.WriteLine($"[DirectoryNotFoundException] {ex.Message}");
        }
    }
}
