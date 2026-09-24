using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Input Visio file path (first argument or default)
        string inputPath = args.Length > 0 ? args[0] : "input.vsdx";
        // Guard: ensure the input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Optional output folder (second argument or same folder as input)
        string outputFolder = args.Length > 1 ? args[1] : Path.GetDirectoryName(inputPath) ?? Directory.GetCurrentDirectory();
        // Guard: ensure the output folder exists (create if missing)
        if (!Directory.Exists(outputFolder))
        {
            try
            {
                Directory.CreateDirectory(outputFolder);
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Failed to create output folder: {ex.Message}");
                return;
            }
        }

        try
        {
            // Load the Visio diagram
            Diagram diagram = new Diagram(inputPath);

            // Iterate through all pages in the diagram
            foreach (Page page in diagram.Pages)
            {
                // Iterate through all shapes on the current page
                foreach (Shape shape in page.Shapes)
                {
                    // Skip deleted shapes
                    if (shape.Del == BOOL.True) continue;

                    // NOTE: Aspose.Diagram does not expose a 'Selected' property on Shape.
                    // Therefore, we export all non‑deleted shapes. Adjust the filter as needed.

                    // Build the SVG file name using the shape's ID
                    string svgFileName = $"{shape.ID}.svg";
                    string svgPath = Path.Combine(outputFolder, svgFileName);

                    // Configure SVG save options (default options are sufficient)
                    SVGSaveOptions svgOptions = new SVGSaveOptions();

                    // Export the shape to an SVG file
                    shape.ToSvg(svgPath, svgOptions);

                    // Log successful export
                    Console.WriteLine($"Exported shape ID {shape.ID} to {svgPath}");
                }
            }
        }
        catch (Exception ex)
        {
            // Write any Aspose or I/O errors to the error stream
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}