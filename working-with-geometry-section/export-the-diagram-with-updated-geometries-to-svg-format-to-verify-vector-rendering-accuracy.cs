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
            Console.Error.WriteLine("Usage: Program <inputVisioFile> <outputSvgFile>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

        // Guard against missing input file.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Error: Input file \"{inputPath}\" does not exist.");
            return;
        }

        try
        {
            // Load the Visio diagram.
            Diagram diagram = new Diagram(inputPath);

            // OPTIONAL: modify geometry here if needed.
            // For this example we directly export to SVG.

            // Configure SVG export options.
            SVGSaveOptions svgOptions = new SVGSaveOptions();
            svgOptions.ExportHiddenPage = false;      // Do not export hidden pages.
            svgOptions.ExportGuideShapes = false;    // Do not export guide shapes.
            svgOptions.IsExportComments = false;     // Do not export comments.
            svgOptions.SVGFitToViewPort = true;      // Fit SVG to viewport.
            svgOptions.PageIndex = 0;                 // Export first page (zero‑based index).
            // svgOptions.PageCount = 1; // Not supported in this version; omitted.

            // Save the diagram as SVG using the configured options.
            diagram.Save(outputPath, svgOptions);
            Console.WriteLine($"Diagram successfully exported to SVG: {outputPath}");
        }
        catch (Exception ex)
        {
            // Report any errors that occur during loading or saving.
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}
