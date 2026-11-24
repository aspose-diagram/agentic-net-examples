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
            Console.Error.WriteLine("Usage: Program <input.vsdx> <output.vsdx> [areaThreshold]");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];
        double areaThreshold = 100.0; // Default threshold.

        // Optional threshold argument.
        if (args.Length >= 3 && !double.TryParse(args[2], out areaThreshold))
        {
            Console.Error.WriteLine("Invalid area threshold value.");
            return;
        }

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

            // Iterate through all pages.
            foreach (Page page in diagram.Pages)
            {
                // Iterate through all shapes on the current page.
                foreach (Shape shape in page.Shapes)
                {
                    // Skip deleted shapes.
                    if (shape.Del == BOOL.True)
                        continue;

                    // Retrieve width and height (in inches). Use .Value to get the numeric value.
                    double width = shape.XForm.Width.Value;
                    double height = shape.XForm.Height.Value;

                    // Calculate shape area.
                    double area = width * height;

                    // Enable fill inheritance only if the area exceeds the threshold.
                    if (area > areaThreshold)
                    {
                        // Set the fill foreground to inherit from parent style/master.
                        // According to Aspose.Diagram rules, this enables inheritance.
                        shape.Fill.FillForegnd.Ufe.F = "INHERITED()";
                    }
                }
            }

            // Save the modified diagram to the specified output path.
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Diagram saved successfully to: {outputPath}");
        }
        catch (Exception ex)
        {
            // Write any Aspose or runtime errors to the error stream.
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}