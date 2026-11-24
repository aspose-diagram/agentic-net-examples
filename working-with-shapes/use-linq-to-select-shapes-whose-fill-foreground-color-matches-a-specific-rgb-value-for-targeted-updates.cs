using System;
using System.IO;
using System.Linq;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Validate arguments: inputPath, outputPath, target RGB (e.g., "255,0,0"), new background hex (e.g., "#00FF00")
        if (args.Length < 4)
        {
            Console.Error.WriteLine("Usage: Program <inputVisioPath> <outputVisioPath> <targetRGB> <newBackgroundHex>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];
        string targetRgb = args[2];
        string newBackgroundHex = args[3];

        // Guard: ensure input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Parse target RGB and convert to hex string (e.g., "#FF0000")
        string targetHex;
        try
        {
            var rgbParts = targetRgb.Split(',').Select(p => int.Parse(p.Trim())).ToArray();
            if (rgbParts.Length != 3 ||
                rgbParts.Any(v => v < 0 || v > 255))
                throw new FormatException();

            targetHex = $"#{rgbParts[0]:X2}{rgbParts[1]:X2}{rgbParts[2]:X2}";
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Invalid target RGB value '{targetRgb}'. Expected format: R,G,B (0-255). Error: {ex.Message}");
            return;
        }

        // Guard: ensure new background hex is a valid hex string (basic check)
        if (!newBackgroundHex.StartsWith("#") || newBackgroundHex.Length != 7)
        {
            Console.Error.WriteLine($"Invalid new background hex color '{newBackgroundHex}'. Expected format: #RRGGBB.");
            return;
        }

        try
        {
            // Load the Visio diagram
            Diagram diagram = new Diagram(inputPath);

            // Iterate through all pages
            foreach (Page page in diagram.Pages)
            {
                // Use LINQ to select shapes whose FillForegnd matches the target hex color
                var matchingShapes = page.Shapes
                                         .Cast<Shape>()
                                         .Where(s => string.Equals(s.Fill.FillForegnd.Value, targetHex, StringComparison.OrdinalIgnoreCase))
                                         .ToList();

                // Apply targeted update: change the background fill color
                foreach (Shape shape in matchingShapes)
                {
                    shape.Fill.FillBkgnd.Value = newBackgroundHex;
                }
            }

            // Save the updated diagram to the specified output path in VSDX format
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Diagram processed and saved to: {outputPath}");
        }
        catch (Exception ex)
        {
            // Write any Aspose or runtime errors to the error console
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}