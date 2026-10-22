using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Validate command‑line arguments.
        if (args.Length < 1)
        {
            Console.Error.WriteLine("Usage: Program <inputVisioPath> [outputVisioPath]");
            return;
        }

        string inputPath = args[0];
        // Guard: ensure the input file exists.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Determine output path. If not supplied, create a new file name with suffix "_flagged".
        string outputPath = args.Length >= 2
            ? args[1]
            : Path.Combine(
                Path.GetDirectoryName(inputPath) ?? string.Empty,
                Path.GetFileNameWithoutExtension(inputPath) + "_flagged" + Path.GetExtension(inputPath));

        try
        {
            // Load the Visio diagram inside a using block as required.
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Define size thresholds (in inches). Shapes larger than these will be flagged.
                const double maxWidth = 2.0;
                const double maxHeight = 2.0;

                // Iterate over all pages in the diagram.
                foreach (Page page in diagram.Pages)
                {
                    // Iterate over all shapes on the current page.
                    foreach (Shape shape in page.Shapes)
                    {
                        // Skip deleted shapes.
                        if (shape.Del == BOOL.True)
                            continue;

                        // Retrieve shape dimensions.
                        double width = shape.XForm.Width.Value;
                        double height = shape.XForm.Height.Value;

                        // Check if the shape exceeds the defined size thresholds.
                        if (width > maxWidth || height > maxHeight)
                        {
                            // Flag the shape by applying a red border.
                            // Set line color to red.
                            shape.Line.LineColor.Value = "#FF0000";

                            // Set a visible line weight (e.g., 0.03 inches).
                            shape.Line.LineWeight.Value = 0.03;

                            // Ensure the line pattern is solid. Cast integer to LinePatternValue.
                            shape.Line.LinePattern.Value = (LinePatternValue)1;
                        }
                    }
                }

                // Save the modified diagram to the output path using VSDX format.
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
            }

            Console.WriteLine($"Processing completed. Output saved to: {outputPath}");
        }
        catch (Exception ex)
        {
            // Write any Aspose or I/O errors to the error stream.
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}