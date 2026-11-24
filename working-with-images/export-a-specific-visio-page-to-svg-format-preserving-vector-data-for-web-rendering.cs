using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Validate command‑line arguments: input Visio file, page index (zero‑based), output SVG path.
        if (args.Length < 3)
        {
            Console.Error.WriteLine("Usage: <program> <inputVisioPath> <pageIndex> <outputSvgPath>");
            return;
        }

        string inputPath = args[0];
        string pageIndexArg = args[1];
        string outputPath = args[2];

        // Guard: ensure the input Visio file exists.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Error: Input file not found: {inputPath}");
            return;
        }

        // Parse the page index (zero‑based).
        if (!int.TryParse(pageIndexArg, out int pageIndex) || pageIndex < 0)
        {
            Console.Error.WriteLine("Error: Page index must be a non‑negative integer.");
            return;
        }

        try
        {
            // Load the Visio diagram inside a using block (mandatory pattern).
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Verify the requested page index is within range.
                if (pageIndex >= diagram.Pages.Count)
                {
                    Console.Error.WriteLine($"Error: Page index {pageIndex} is out of range. Diagram contains {diagram.Pages.Count} pages.");
                    return;
                }

                // Configure SVG export options.
                SVGSaveOptions svgOptions = new SVGSaveOptions
                {
                    // Export only the specified page.
                    PageIndex = pageIndex,

                    // Do not export hidden pages or guide shapes.
                    ExportGuideShapes = false,

                    // Preserve the viewbox (fit SVG to viewport).
                    SVGFitToViewPort = true,

                    // Do not export comments.
                    IsExportComments = false
                };

                // Save the selected page as an SVG file.
                diagram.Save(outputPath, svgOptions);

                Console.WriteLine($"Page {pageIndex} successfully exported to SVG: {outputPath}");
            }
        }
        catch (Exception ex)
        {
            // Write any Aspose or I/O errors to the error stream.
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}
