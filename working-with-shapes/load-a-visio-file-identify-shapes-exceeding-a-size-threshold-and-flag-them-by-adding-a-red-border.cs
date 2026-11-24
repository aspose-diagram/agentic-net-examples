using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Validate command‑line arguments.
        if (args.Length < 1)
        {
            Console.Error.WriteLine("Usage: Program <inputVisioPath> [sizeThresholdInInches] [outputVisioPath]");
            return;
        }

        // Input Visio file path.
        string inputPath = args[0];
        // Guard: ensure the input file exists.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Parse optional size threshold (default = 2.0 inches).
        double sizeThreshold = 2.0;
        if (args.Length >= 2 && !double.TryParse(args[1], out sizeThreshold))
        {
            Console.Error.WriteLine($"Invalid size threshold value: {args[1]}");
            return;
        }

        // Determine output path (optional argument or auto‑generated).
        string outputPath;
        if (args.Length >= 3)
        {
            outputPath = args[2];
        }
        else
        {
            string dir = Path.GetDirectoryName(inputPath) ?? string.Empty;
            string nameWithoutExt = Path.GetFileNameWithoutExtension(inputPath);
            outputPath = Path.Combine(dir, $"{nameWithoutExt}_flagged.vsdx");
        }

        // Load the Visio diagram inside a using block (mandatory pattern).
        try
        {
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Iterate through all pages in the diagram.
                foreach (Page page in diagram.Pages)
                {
                    // Iterate through all shapes on the current page.
                    foreach (Shape shape in page.Shapes)
                    {
                        // Skip logically deleted shapes.
                        if (shape.Del == BOOL.True)
                            continue;

                        // Retrieve shape dimensions (in inches).
                        double width = shape.XForm.Width.Value;
                        double height = shape.XForm.Height.Value;

                        // Flag shapes that exceed the size threshold.
                        if (width > sizeThreshold || height > sizeThreshold)
                        {
                            // Add a red border by setting the line color.
                            shape.Line.LineColor.Value = "#FF0000";

                            // Increase line weight for better visibility.
                            shape.Line.LineWeight.Value = 0.02;
                        }
                    }
                }

                // Save the modified diagram to the output file.
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
                Console.WriteLine($"Processing complete. Output saved to: {outputPath}");
            }
        }
        catch (Exception ex)
        {
            // Report any errors that occur during processing.
            Console.Error.WriteLine($"Error processing Visio file: {ex.Message}");
        }
    }
}