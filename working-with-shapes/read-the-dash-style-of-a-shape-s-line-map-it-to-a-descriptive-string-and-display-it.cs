using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Validate input arguments
        if (args.Length < 1)
        {
            Console.Error.WriteLine("Usage: Program <VisioFilePath> [ShapeId]");
            return;
        }

        string filePath = args[0];
        // Guard: ensure the Visio file exists
        if (!File.Exists(filePath))
        {
            Console.Error.WriteLine($"Error: File not found - {filePath}");
            return;
        }

        // Optional shape ID argument (default to 1)
        long shapeId = 1;
        if (args.Length >= 2 && long.TryParse(args[1], out long parsedId))
        {
            shapeId = parsedId;
        }

        try
        {
            // Load the Visio diagram inside a using block (mandatory)
            using (Diagram diagram = new Diagram(filePath))
            {
                // Get the first page (index 0)
                Page page = diagram.Pages[0];

                // Retrieve the shape by its ID
                Shape shape = page.Shapes.GetShape(shapeId);
                if (shape == null)
                {
                    Console.Error.WriteLine($"Error: Shape with ID {shapeId} not found on the first page.");
                    return;
                }

                // Read the line dash pattern enum value
                // LinePattern.Value returns a LinePatternValue enum; cast to int for mapping
                int patternCode = (int)shape.Line.LinePattern.Value;

                // Map the numeric code to a descriptive string
                string description = MapLinePatternToDescription(patternCode);

                // Display the result
                Console.WriteLine($"Shape ID: {shapeId}");
                Console.WriteLine($"Line dash style code: {patternCode}");
                Console.WriteLine($"Line dash style description: {description}");
            }
        }
        catch (Exception ex)
        {
            // Write any Aspose or runtime errors to the error console
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }

    /// <summary>
    /// Maps Visio line pattern numeric codes to human‑readable descriptions.
    /// </summary>
    /// <param name="code">The numeric line pattern code.</param>
    /// <returns>A descriptive string for the dash style.</returns>
    private static string MapLinePatternToDescription(int code)
    {
        // Visio uses integer codes for line patterns; map common values
        switch (code)
        {
            case 0:
                return "Solid";
            case 1:
                return "Dash";
            case 2:
                return "Dot";
            case 3:
                return "DashDot";
            case 4:
                return "DashDotDot";
            default:
                return "Unknown";
        }
    }
}