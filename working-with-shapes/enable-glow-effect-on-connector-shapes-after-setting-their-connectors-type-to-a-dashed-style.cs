using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Validate command‑line arguments.
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <input.vsdx> <output.vsdx>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

        // Guard against missing input file.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the Visio diagram.
            Diagram diagram = new Diagram(inputPath);

            // Iterate through all pages and their shapes.
            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    // Process only connector (1‑D) shapes.
                    if (shape.OneD)
                    {
                        // Set the line pattern to dashed.
                        shape.Line.LinePattern.Value = LinePatternValue.Dash;

                        // Apply a "glow" effect workaround:
                        //   - Bright yellow line color.
                        //   - Slightly thicker line weight.
                        //   - Yellow shadow with simple drop shadow type.
                        shape.Line.LineColor.Value = "#FFFF00";
                        shape.Line.LineWeight.Value = 0.05;
                        shape.Fill.ShdwForegnd.Value = "#FFFF00";
                        shape.Fill.ShapeShdwType.Value = ShapeShdwTypeValue.Simple;
                    }
                }
            }

            // Save the modified diagram.
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Diagram saved successfully to: {outputPath}");
        }
        catch (Exception ex)
        {
            // Report any errors that occur during processing.
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}