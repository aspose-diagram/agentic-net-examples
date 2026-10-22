using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Validate arguments: expect diagram file path and shape ID
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <diagramPath> <shapeId>");
            return;
        }

        string diagramPath = args[0];
        string shapeIdArg = args[1];

        // Guard: check if the diagram file exists
        if (!File.Exists(diagramPath))
        {
            Console.Error.WriteLine($"Error: File not found - {diagramPath}");
            return;
        }

        // Parse shape ID (expected as integer/long)
        if (!long.TryParse(shapeIdArg, out long shapeId))
        {
            Console.Error.WriteLine($"Error: Invalid shape ID '{shapeIdArg}'. Must be a numeric value.");
            return;
        }

        try
        {
            // Load the Visio diagram
            Diagram diagram = new Diagram(diagramPath);

            // Ensure the diagram has at least one page
            if (diagram.Pages.Count == 0)
            {
                Console.Error.WriteLine("Error: Diagram contains no pages.");
                return;
            }

            // Get the first page (index 0)
            Page page = diagram.Pages[0];

            // Retrieve the shape by its ID
            Shape shape = page.Shapes.GetShape(shapeId);
            if (shape == null)
            {
                Console.Error.WriteLine($"Error: Shape with ID {shapeId} not found on page '{page.Name}'.");
                return;
            }

            // Extract the line color value (hex string, e.g., "#FF00AA")
            string lineColorHex = shape.Line.LineColor.Value;

            if (string.IsNullOrEmpty(lineColorHex))
            {
                Console.Error.WriteLine("Error: Line color is not set for the specified shape.");
                return;
            }

            // Remove leading '#' if present
            if (lineColorHex.StartsWith("#"))
                lineColorHex = lineColorHex.Substring(1);

            // Ensure the hex string has exactly 6 characters
            if (lineColorHex.Length != 6)
            {
                Console.Error.WriteLine($"Error: Unexpected line color format '{shape.Line.LineColor.Value}'.");
                return;
            }

            // Parse RGB components from hex
            int r = Convert.ToInt32(lineColorHex.Substring(0, 2), 16);
            int g = Convert.ToInt32(lineColorHex.Substring(2, 2), 16);
            int b = Convert.ToInt32(lineColorHex.Substring(4, 2), 16);

            // Output the RGB components
            Console.WriteLine($"Line Color RGB: R={r}, G={g}, B={b}");
        }
        catch (Exception ex)
        {
            // Write any Aspose or runtime errors to the error stream
            Console.Error.WriteLine($"Exception: {ex.Message}");
        }
    }
}