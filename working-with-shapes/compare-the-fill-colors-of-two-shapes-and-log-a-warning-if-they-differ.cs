using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Validate command line arguments: diagram path, first shape ID, second shape ID
        if (args.Length < 3)
        {
            Console.Error.WriteLine("Usage: Program <diagramPath> <shapeId1> <shapeId2>");
            return;
        }

        string diagramPath = args[0];
        string shapeId1Str = args[1];
        string shapeId2Str = args[2];

        // Guard: check if the diagram file exists
        if (!File.Exists(diagramPath))
        {
            Console.Error.WriteLine($"Error: Diagram file not found at '{diagramPath}'.");
            return;
        }

        // Guard: parse shape IDs
        if (!long.TryParse(shapeId1Str, out long shapeId1) || !long.TryParse(shapeId2Str, out long shapeId2))
        {
            Console.Error.WriteLine("Error: Shape IDs must be valid integers.");
            return;
        }

        try
        {
            // Load the Visio diagram
            Diagram diagram = new Diagram(diagramPath);

            // Retrieve the first page (assumes at least one page exists)
            if (diagram.Pages.Count == 0)
            {
                Console.Error.WriteLine("Error: The diagram contains no pages.");
                return;
            }

            Page page = diagram.Pages[0];

            // Retrieve the two shapes by their IDs
            Shape shape1 = page.Shapes.GetShape(shapeId1);
            Shape shape2 = page.Shapes.GetShape(shapeId2);

            // Guard: ensure shapes were found
            if (shape1 == null)
            {
                Console.Error.WriteLine($"Error: Shape with ID {shapeId1} not found on page '{page.Name}'.");
                return;
            }

            if (shape2 == null)
            {
                Console.Error.WriteLine($"Error: Shape with ID {shapeId2} not found on page '{page.Name}'.");
                return;
            }

            // Extract fill foreground colors (hex string values)
            string fillColor1 = shape1.Fill?.FillForegnd?.Value;
            string fillColor2 = shape2.Fill?.FillForegnd?.Value;

            // Guard: handle missing fill information
            if (string.IsNullOrEmpty(fillColor1))
            {
                Console.Error.WriteLine($"Warning: Shape ID {shapeId1} does not have a FillForegnd value.");
                fillColor1 = string.Empty;
            }

            if (string.IsNullOrEmpty(fillColor2))
            {
                Console.Error.WriteLine($"Warning: Shape ID {shapeId2} does not have a FillForegnd value.");
                fillColor2 = string.Empty;
            }

            // Compare the fill colors
            if (!string.Equals(fillColor1, fillColor2, StringComparison.OrdinalIgnoreCase))
            {
                Console.WriteLine($"Warning: Fill colors differ between shapes {shapeId1} and {shapeId2}.");
                Console.WriteLine($"  Shape {shapeId1} FillForegnd: {fillColor1}");
                Console.WriteLine($"  Shape {shapeId2} FillForegnd: {fillColor2}");
            }
            else
            {
                Console.WriteLine($"Info: Fill colors are identical for shapes {shapeId1} and {shapeId2} ({fillColor1}).");
            }
        }
        catch (Exception ex)
        {
            // Log any unexpected errors from Aspose.Diagram operations
            Console.Error.WriteLine($"Error: An exception occurred - {ex.Message}");
        }
    }
}