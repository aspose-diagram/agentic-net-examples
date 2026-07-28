using System.IO;
using System;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        try
        {

            // Path to the VSDX file on disk
            string filePath = "sample.vsdx";

            // Load the diagram using the constructor that accepts a file name
            Diagram diagram = new Diagram(filePath);

            // Verify that the diagram was loaded successfully
            if (diagram != null && diagram.Pages.Count > 0)
            {
                Console.WriteLine("Diagram loaded successfully. Page count: " + diagram.Pages.Count);
            }
            else
            {
                Console.WriteLine("Failed to load the diagram.");
            }

            // Clean up resources
            diagram.Dispose();

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
