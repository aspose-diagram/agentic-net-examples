using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Validate arguments
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <VisioFilePath> <ShapeId>");
            return;
        }

        string visioPath = args[0];
        string shapeIdArg = args[1];

        // Guard: check file existence
        if (!File.Exists(visioPath))
        {
            Console.Error.WriteLine($"Error: File not found - {visioPath}");
            return;
        }

        // Parse shape ID
        if (!int.TryParse(shapeIdArg, out int shapeId))
        {
            Console.Error.WriteLine($"Error: Invalid shape ID '{shapeIdArg}'.");
            return;
        }

        try
        {
            // Load the Visio diagram
            Diagram diagram = new Diagram(visioPath);

            // Get the first page (index 0)
            if (diagram.Pages.Count == 0)
            {
                Console.Error.WriteLine("Error: The diagram contains no pages.");
                return;
            }
            Page page = diagram.Pages[0];

            // Retrieve the shape by ID
            Shape shape = page.Shapes.GetShape(shapeId);
            if (shape == null)
            {
                Console.Error.WriteLine($"Error: Shape with ID {shapeId} not found on page.");
                return;
            }

            // Skip deleted shapes
            if (shape.Del == BOOL.True)
            {
                Console.Error.WriteLine($"Error: Shape with ID {shapeId} is marked as deleted.");
                return;
            }

            // Extract the line color value (expected as a hex string like "#RRGGBB")
            string colorValue = shape.Line.LineColor.Value;
            if (string.IsNullOrEmpty(colorValue))
            {
                Console.Error.WriteLine("Error: Line color is not set or could not be retrieved.");
                return;
            }

            // Normalize the color string
            if (colorValue.StartsWith("#"))
                colorValue = colorValue.Substring(1);

            // Ensure the string has exactly 6 hex digits
            if (colorValue.Length != 6)
            {
                Console.Error.WriteLine($"Error: Unexpected line color format '{shape.Line.LineColor.Value}'.");
                return;
            }

            // Parse RGB components
            int r = Convert.ToInt32(colorValue.Substring(0, 2), 16);
            int g = Convert.ToInt32(colorValue.Substring(2, 2), 16);
            int b = Convert.ToInt32(colorValue.Substring(4, 2), 16);

            // Output the RGB components
            Console.WriteLine($"Line color RGB: {r}, {g}, {b}");
        }
        catch (Exception ex)
        {
            // Write any Aspose or runtime errors to the error stream
            Console.Error.WriteLine($"Exception: {ex.Message}");
        }
    }
}