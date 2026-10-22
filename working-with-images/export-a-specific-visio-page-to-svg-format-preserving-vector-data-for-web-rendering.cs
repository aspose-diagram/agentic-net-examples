using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Validate command‑line arguments.
        if (args.Length < 3)
        {
            Console.Error.WriteLine("Usage: <program> <inputVisioPath> <outputSvgPath> <pageIndex>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];
        string pageIndexArg = args[2];

        // Guard: ensure the input Visio file exists.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Error: Input file \"{inputPath}\" does not exist.");
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
            // Load the Visio diagram inside a using block as required.
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Validate the requested page index against the diagram's page count.
                if (pageIndex >= diagram.Pages.Count)
                {
                    Console.Error.WriteLine($"Error: Page index {pageIndex} is out of range. Diagram contains {diagram.Pages.Count} pages.");
                    return;
                }

                // Configure SVG export options for a single page.
                SVGSaveOptions svgOptions = new SVGSaveOptions();
                svgOptions.PageIndex = pageIndex;          // Export only the specified page.
                // svgOptions.PageCount = 1;                // Not required; omitted to avoid missing member error.
                svgOptions.SVGFitToViewPort = true;        // Preserve viewbox.
                svgOptions.ExportGuideShapes = false;      // Do not export guide shapes.
                svgOptions.IsExportComments = false;       // Do not export comments.
                svgOptions.SaveFormat = SaveFileFormat.Svg; // Explicitly set the format.

                // Save the selected page as SVG.
                diagram.Save(outputPath, svgOptions);
            }

            Console.WriteLine($"Page {pageIndex} exported successfully to \"{outputPath}\".");
        }
        catch (Exception ex)
        {
            // Write any Aspose or runtime errors to the error stream.
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}
