using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Determine input and output file paths.
        string inputPath = args.Length > 0 ? args[0] : "input.vsdx";
        string outputPath = args.Length > 1 ? args[1] : "output_glow.vsdx";

        // Guard: ensure the input file exists.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the Visio diagram.
            Diagram diagram = new Diagram(inputPath);

            // Iterate through all pages.
            foreach (Page page in diagram.Pages)
            {
                // Iterate through all shapes on the page.
                foreach (Shape shape in page.Shapes)
                {
                    // Process only connector shapes (1‑D shapes).
                    if (shape.OneD)
                    {
                        // Set the connector line pattern to dashed.
                        shape.Line.LinePattern.Value = LinePatternValue.Dash;

                        // Apply a glow-like effect using bright color, increased weight, and shadow.
                        shape.Line.LineColor.Value = "#FFFF00";          // Bright yellow line color.
                        shape.Line.LineWeight.Value = 0.05;             // Thicker line for visibility.
                        shape.Fill.ShdwForegnd.Value = "#FFFF00";       // Shadow color matching the line.
                        shape.Fill.ShapeShdwType.Value = ShapeShdwTypeValue.Simple; // Simple drop shadow.
                    }
                }
            }

            // Save the modified diagram.
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Diagram saved with glow effect to: {outputPath}");
        }
        catch (Exception ex)
        {
            // Write any errors to the error console.
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}