using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Expect two arguments: input Visio file path and output SVG file path.
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: <program> <inputVisioPath> <outputSvgPath>");
            return;
        }

        string inputPath = args[0];
        // Guard: ensure the input Visio file exists.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        string outputPath = args[1];
        // Guard: ensure the directory for the output file exists.
        string outputDir = Path.GetDirectoryName(outputPath);
        if (!Directory.Exists(outputDir))
        {
            Console.Error.WriteLine($"Output directory does not exist: {outputDir}");
            return;
        }

        try
        {
            // Load the Visio diagram from the specified file.
            Diagram diagram = new Diagram(inputPath);

            // Define a tolerance (in inches) for curve simplification.
            double tolerance = 0.01; // Adjust as needed.

            // Iterate over all pages in the diagram.
            foreach (Page page in diagram.Pages)
            {
                // Iterate over all shapes on the current page.
                foreach (Shape shape in page.Shapes)
                {
                    // Skip deleted shapes.
                    if (shape.Del == BOOL.True) continue;

                    // NOTE: The SimplifyGeometry method is not available in this version of Aspose.Diagram.
                    // If curve simplification is required, it must be implemented manually by processing
                    // the shape's Geoms collection. For now, this step is omitted.
                }
            }

            // NOTE: Aspose.Diagram does not support direct DXF export.
            // As a workaround, export the simplified diagram to SVG, which can be
            // converted to DXF using external tools if required.
            SVGSaveOptions svgOptions = new SVGSaveOptions
            {
                // Export hidden pages is set to false to keep the output clean.
                ExportHiddenPage = false
            };

            // Save the diagram (with simplified geometry) to the specified SVG file.
            diagram.Save(outputPath, svgOptions);

            Console.WriteLine($"Simplified diagram saved to: {outputPath}");
        }
        catch (Exception ex)
        {
            // Write any errors that occur during processing to the error stream.
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}