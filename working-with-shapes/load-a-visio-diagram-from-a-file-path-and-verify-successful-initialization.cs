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

            // Load and verify the diagram
            Diagram diagram = LoadDiagram(filePath);

            // Diagram is ready for further processing
            // ...

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }

    static Diagram LoadDiagram(string filePath)
    {
        // Load the diagram using the constructor that accepts a file path
        Diagram diagram = new Diagram(filePath);

        // Verify successful initialization:
        // Ensure the diagram object is not null and contains at least one page
        if (diagram == null || diagram.Pages == null || diagram.Pages.Count == 0)
        {
            throw new InvalidOperationException("Failed to load the Visio diagram or it contains no pages.");
        }

        Console.WriteLine($"Diagram loaded successfully. Page count: {diagram.Pages.Count}");
        return diagram;
    }
}
