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
            Console.Error.WriteLine("Usage: Program <inputVisioPath> <outputSvgPath>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

        // Guard: ensure the input file exists.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the Visio diagram inside a using block (mandatory pattern).
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Configure SVG save options.
                SVGSaveOptions svgOptions = new SVGSaveOptions();

                // Preserve the original viewbox by fitting the SVG to the viewport.
                svgOptions.SVGFitToViewPort = true;

                // Export guide shapes (no direct CSS support, this is the closest alternative).
                svgOptions.ExportGuideShapes = true;

                // Export comments if needed (optional).
                svgOptions.IsExportComments = false;

                // Export only the first page (default PageIndex is 0).
                svgOptions.PageIndex = 0;

                // Explicitly set the save format (optional, as SVGSaveOptions implies SVG).
                svgOptions.SaveFormat = SaveFileFormat.Svg;

                // Note: Aspose.Diagram does not provide a direct option to enable CSS styling for shapes in SVG.
                // The ExportGuideShapes flag is the closest available feature for styling control.

                // Save the diagram as SVG using the configured options.
                diagram.Save(outputPath, svgOptions);
            }
        }
        catch (Exception ex)
        {
            // Write any errors to the error console.
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}
