using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Validate command‑line arguments
        if (args.Length < 1)
        {
            Console.Error.WriteLine("Usage: Program <inputVisioFile> [outputSvgFile]");
            return;
        }

        string inputPath = args[0];
        string outputPath = args.Length > 1 ? args[1] : "FirstPageTriangle.svg";

        // Guard against missing input file
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file does not exist: {inputPath}");
            return;
        }

        try
        {
            // Load the Visio diagram inside a using block (required pattern)
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Ensure the diagram contains at least one page
                if (diagram.Pages.Count == 0)
                {
                    Console.Error.WriteLine("The diagram contains no pages.");
                    return;
                }

                // Configure SVG save options to export only the first page
                SVGSaveOptions svgOptions = new SVGSaveOptions
                {
                    PageIndex = 0,          // zero‑based index of the first page
                    // PageCount property is not available in this API version; omitting it exports a single page
                    SVGFitToViewPort = true,
                    ExportGuideShapes = false,
                    IsExportComments = false
                };

                // Save the first page as an SVG file
                diagram.Save(outputPath, svgOptions);
                Console.WriteLine($"First page exported successfully to: {outputPath}");
            }
        }
        catch (Exception ex)
        {
            // Write any Aspose or IO errors to the error stream
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}
