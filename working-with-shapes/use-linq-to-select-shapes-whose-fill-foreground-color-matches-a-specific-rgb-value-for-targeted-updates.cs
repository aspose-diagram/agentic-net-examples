using System;
using System.IO;
using System.Linq;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Validate command‑line arguments.
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <inputVisioFile> <outputVisioFile>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

        // Guard: ensure the input file exists.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Error: Input file \"{inputPath}\" does not exist.");
            return;
        }

        // Define the target fill foreground color (hex string) and the new color to apply.
        const string targetColor = "#FF0000"; // Red – change as needed.
        const string newColor = "#00FF00";    // Green – change as needed.

        try
        {
            // Load the Visio diagram.
            Diagram diagram = new Diagram(inputPath);

            // Use LINQ to find all shapes whose FillForegnd matches the target color.
            var matchingShapes = diagram.Pages
                .SelectMany(page => page.Shapes.Cast<Shape>())
                .Where(shape => string.Equals(shape.Fill.FillForegnd.Value, targetColor, StringComparison.OrdinalIgnoreCase))
                .ToList();

            Console.WriteLine($"Found {matchingShapes.Count} shape(s) with fill foreground color {targetColor}.");

            // Update each matching shape's fill foreground color to the new color.
            foreach (var shape in matchingShapes)
            {
                shape.Fill.FillForegnd.Value = newColor;
                Console.WriteLine($"Shape ID {shape.ID} fill color changed to {newColor}.");
            }

            // Save the modified diagram to the specified output path.
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Diagram saved successfully to \"{outputPath}\".");
        }
        catch (Exception ex)
        {
            // Write any errors to the error stream.
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}