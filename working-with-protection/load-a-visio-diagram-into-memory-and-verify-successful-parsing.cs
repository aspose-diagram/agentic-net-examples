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
            string filePath = "sample.vsdx";

            // Load the diagram using the constructor that accepts a file name
            Diagram diagram = new Diagram(filePath);

            // Verify that the diagram was parsed by checking the pages collection
            if (diagram.Pages != null && diagram.Pages.Count > 0)
            {
                Console.WriteLine($"Diagram loaded successfully. Pages count: {diagram.Pages.Count}");
            }
            else
            {
                Console.WriteLine("Failed to load diagram or no pages were found.");
            }

            // Release resources
            diagram.Dispose();

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
