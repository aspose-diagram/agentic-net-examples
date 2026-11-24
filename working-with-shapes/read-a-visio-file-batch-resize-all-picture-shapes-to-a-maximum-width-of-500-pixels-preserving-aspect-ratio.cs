using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Validate input arguments
        if (args.Length < 1)
        {
            Console.Error.WriteLine("Usage: Program <inputVisioPath> [outputVisioPath]");
            return;
        }

        string inputPath = args[0];
        string outputPath = args.Length >= 2 ? args[1] : Path.Combine(Path.GetDirectoryName(inputPath) ?? "", "Resized_" + Path.GetFileName(inputPath));

        // Guard: ensure the input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the Visio diagram inside a using block as required
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Define maximum width in inches (500 pixels at 96 DPI)
                const double dpi = 96.0;
                double maxWidthInches = 500.0 / dpi;

                // Iterate through all pages
                foreach (Page page in diagram.Pages)
                {
                    // Iterate through all shapes on the page
                    foreach (Shape shape in page.Shapes)
                    {
                        // Skip deleted shapes
                        if (shape.Del == BOOL.True)
                            continue;

                        // Identify picture shapes (foreign data shapes)
                        if (shape.Type == TypeValue.Foreign)
                        {
                            // Current dimensions (in inches)
                            double currentWidth = shape.XForm.Width.Value;
                            double currentHeight = shape.XForm.Height.Value;

                            // If the shape already meets the size constraint, skip
                            if (currentWidth <= maxWidthInches)
                                continue;

                            // Calculate scaling factor to fit within max width while preserving aspect ratio
                            double scale = maxWidthInches / currentWidth;
                            double newWidth = maxWidthInches;
                            double newHeight = currentHeight * scale;

                            // Apply new dimensions
                            shape.XForm.Width.Value = newWidth;
                            shape.XForm.Height.Value = newHeight;
                        }
                    }
                }

                // Save the modified diagram to the output path in VSDX format
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
                Console.WriteLine($"Diagram saved successfully to: {outputPath}");
            }
        }
        catch (Exception ex)
        {
            // Write any Aspose or runtime errors to the error stream
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}