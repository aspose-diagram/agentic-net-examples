using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Validate arguments: expected <diagramPath> <shapeId1> <shapeId2>
        if (args.Length < 3)
        {
            Console.Error.WriteLine("Usage: Program <diagramPath> <shapeId1> <shapeId2>");
            return;
        }

        string diagramPath = args[0];
        // Guard: ensure the diagram file exists
        if (!File.Exists(diagramPath))
        {
            Console.Error.WriteLine($"Error: File not found - {diagramPath}");
            return;
        }

        // Parse shape IDs
        if (!long.TryParse(args[1], out long shapeId1) || !long.TryParse(args[2], out long shapeId2))
        {
            Console.Error.WriteLine("Error: Shape IDs must be valid integers.");
            return;
        }

        try
        {
            // Load the Visio diagram
            Diagram diagram = new Diagram(diagramPath);

            // Use the first page (or you could select by name if needed)
            if (diagram.Pages.Count == 0)
            {
                Console.Error.WriteLine("Error: Diagram contains no pages.");
                return;
            }

            Page page = diagram.Pages[0];

            // Retrieve the first shape
            Shape shape1 = page.Shapes.GetShape(shapeId1);
            if (shape1 == null)
            {
                Console.Error.WriteLine($"Error: Shape with ID {shapeId1} not found.");
                return;
            }

            // Retrieve the second shape
            Shape shape2 = page.Shapes.GetShape(shapeId2);
            if (shape2 == null)
            {
                Console.Error.WriteLine($"Error: Shape with ID {shapeId2} not found.");
                return;
            }

            // Ensure shapes are not deleted
            if (shape1.Del == BOOL.True)
            {
                Console.Error.WriteLine($"Warning: Shape {shapeId1} is marked as deleted and will be ignored.");
                return;
            }
            if (shape2.Del == BOOL.True)
            {
                Console.Error.WriteLine($"Warning: Shape {shapeId2} is marked as deleted and will be ignored.");
                return;
            }

            // Access fill foreground colors (hex string like "#FF0000")
            string fillColor1 = shape1.Fill?.FillForegnd?.Value;
            string fillColor2 = shape2.Fill?.FillForegnd?.Value;

            // If either fill color is null, treat it as undefined
            if (string.IsNullOrEmpty(fillColor1) || string.IsNullOrEmpty(fillColor2))
            {
                Console.WriteLine("Info: One or both shapes do not have a defined fill foreground color.");
                return;
            }

            // Compare the fill colors (case-insensitive)
            if (!string.Equals(fillColor1, fillColor2, StringComparison.OrdinalIgnoreCase))
            {
                Console.WriteLine($"Warning: Fill colors differ. Shape {shapeId1} has {fillColor1}, Shape {shapeId2} has {fillColor2}.");
            }
            else
            {
                Console.WriteLine($"Info: Fill colors are identical ({fillColor1}).");
            }
        }
        catch (Exception ex)
        {
            // Log any unexpected errors
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}