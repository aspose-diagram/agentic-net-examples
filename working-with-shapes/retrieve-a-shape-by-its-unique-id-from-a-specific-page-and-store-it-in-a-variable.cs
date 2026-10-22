using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Define the input Visio file path, target page index, and shape ID.
        string diagramPath = "input.vsdx";
        if (!System.IO.File.Exists(diagramPath))
        {
            Console.Error.WriteLine($"File not found: {diagramPath}");
            return;
        }
        int targetPageIndex = 0; // zero‑based page index
        long targetShapeId = 1;  // unique shape ID to retrieve

        // Guard: ensure the diagram file exists before proceeding.
        if (!File.Exists(diagramPath))
        {
            Console.Error.WriteLine($"Error: Diagram file not found at '{diagramPath}'.");
            return;
        }

        try
        {
            // Load the Visio diagram.
            Diagram diagram = new Diagram(diagramPath);

            // Validate the requested page index.
            if (targetPageIndex < 0 || targetPageIndex >= diagram.Pages.Count)
            {
                Console.Error.WriteLine($"Error: Page index {targetPageIndex} is out of range. Diagram contains {diagram.Pages.Count} pages.");
                return;
            }

            // Retrieve the specific page.
            Page page = diagram.Pages[targetPageIndex];

            // Retrieve the shape by its unique ID from the page.
            // The GetShape method expects an integer; cast the long ID accordingly.
            Shape shape = page.Shapes.GetShape((int)targetShapeId);

            // Verify that the shape was found.
            if (shape == null)
            {
                Console.Error.WriteLine($"Error: No shape with ID {targetShapeId} found on page {targetPageIndex}.");
                return;
            }

            // Shape successfully retrieved; you can now work with the 'shape' variable.
            Console.WriteLine($"Shape retrieved successfully:");
            Console.WriteLine($"  ID      : {shape.ID}");
            Console.WriteLine($"  NameU   : {shape.NameU}");
            Console.WriteLine($"  Master  : {shape.Master?.Name ?? "None"}");
        }
        catch (Exception ex)
        {
            // Output any Aspose.Diagram related errors.
            Console.Error.WriteLine($"An error occurred while processing the diagram: {ex.Message}");
        }
    }
}