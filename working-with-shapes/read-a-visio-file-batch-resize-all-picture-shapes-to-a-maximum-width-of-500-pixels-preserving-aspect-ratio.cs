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
            Console.Error.WriteLine("Usage: Program <input-visio-file>");
            return;
        }

        string inputPath = args[0];
        // Guard: ensure the input file exists.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Error: File not found – {inputPath}");
            return;
        }

        // Define output path (original name with suffix).
        string outputPath = Path.Combine(
            Path.GetDirectoryName(inputPath) ?? "",
            Path.GetFileNameWithoutExtension(inputPath) + "_resized.vsdx");

        try
        {
            // Load the Visio diagram inside a using block as required.
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Maximum width in pixels and conversion to inches (assuming 96 DPI).
                const double maxWidthPixels = 500.0;
                const double dpi = 96.0;
                double maxWidthInches = maxWidthPixels / dpi;

                // Iterate through all pages.
                foreach (Page page in diagram.Pages)
                {
                    // Iterate through all shapes on the page.
                    foreach (Shape shape in page.Shapes)
                    {
                        // Skip deleted shapes.
                        if (shape.Del == BOOL.True)
                            continue;

                        // Identify picture (foreign) shapes.
                        if (shape.Type == TypeValue.Foreign)
                        {
                            // Current dimensions (in inches).
                            double currentWidth = shape.XForm.Width.Value;
                            double currentHeight = shape.XForm.Height.Value;

                            // If the shape already fits within the limit, skip.
                            if (currentWidth <= maxWidthInches)
                                continue;

                            // Compute scaling factor to preserve aspect ratio.
                            double scale = maxWidthInches / currentWidth;
                            double newWidth = maxWidthInches;
                            double newHeight = currentHeight * scale;

                            // Apply new dimensions.
                            shape.SetWidth(newWidth);
                            shape.SetHeight(newHeight);
                        }
                    }
                }

                // Save the modified diagram to the output file.
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
                Console.WriteLine($"Resized diagram saved to: {outputPath}");
            }
        }
        catch (Exception ex)
        {
            // Report any errors that occur during processing.
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}