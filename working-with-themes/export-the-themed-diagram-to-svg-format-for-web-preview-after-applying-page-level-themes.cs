using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Validate command‑line arguments.
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <inputVisioPath> <outputSvgPath>");
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
            // Load the Visio diagram inside a using block as required.
            using (Diagram diagram = new Diagram(inputPath))
            {
                // NOTE: Page‑level themes are automatically applied when the diagram is saved.
                // Prepare SVG save options.
                SVGSaveOptions svgOptions = new SVGSaveOptions();
                svgOptions.ExportHiddenPage = false;          // Do not export hidden pages.
                svgOptions.ExportGuideShapes = false;         // Do not export guide shapes.
                svgOptions.SVGFitToViewPort = true;           // Fit SVG to viewport (preserve viewbox).

                // Explicitly set the format for clarity (optional).
                svgOptions.SaveFormat = SaveFileFormat.Svg;

                // Save the diagram as SVG.
                diagram.Save(outputPath, svgOptions);
                Console.WriteLine($"Diagram successfully exported to SVG: {outputPath}");
            }
        }
        catch (Exception ex)
        {
            // Write any errors to the error stream.
            Console.Error.WriteLine($"Error during SVG export: {ex.Message}");
        }
    }
}