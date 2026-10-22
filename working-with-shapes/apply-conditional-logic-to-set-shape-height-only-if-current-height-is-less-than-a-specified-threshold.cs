using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Validate command‑line arguments: input file, output file, height threshold
        if (args.Length < 3)
        {
            Console.Error.WriteLine("Usage: Program <input.vsdx> <output.vsdx> <heightThreshold>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];
        string thresholdArg = args[2];

        // Guard: ensure the input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Parse the height threshold
        if (!double.TryParse(thresholdArg, out double heightThreshold))
        {
            Console.Error.WriteLine($"Invalid height threshold: {thresholdArg}");
            return;
        }

        try
        {
            // Load the Visio diagram
            Diagram diagram = new Diagram(inputPath);

            // Iterate through all pages
            foreach (Page page in diagram.Pages)
            {
                // Iterate through all shapes on the page
                foreach (Shape shape in page.Shapes)
                {
                    // Skip logically deleted shapes
                    if (shape.Del == BOOL.True)
                        continue;

                    // Retrieve the current height (in inches)
                    double currentHeight = shape.XForm.Height.Value;

                    // Apply conditional logic: set new height only if current height is less than the threshold
                    if (currentHeight < heightThreshold)
                    {
                        // Example: increase height to the threshold value
                        shape.SetHeight(heightThreshold);
                    }
                }
            }

            // Save the modified diagram using a valid overload (requires SaveFileFormat)
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
        }
        catch (Exception ex)
        {
            // Write any Aspose or I/O errors to the error stream
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}