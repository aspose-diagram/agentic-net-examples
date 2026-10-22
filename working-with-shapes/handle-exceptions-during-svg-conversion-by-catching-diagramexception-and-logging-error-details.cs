using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Input Visio file path (first argument or default)
        string inputPath = args.Length > 0 ? args[0] : "sample.vsdx";
        // Output SVG file path (second argument or default)
        string outputPath = args.Length > 1 ? args[1] : "shape.svg";

        // Guard: ensure the input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Error: Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the Visio diagram inside a using block as required
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Ensure the diagram contains at least one page
                if (diagram.Pages.Count == 0)
                {
                    Console.Error.WriteLine("Error: Diagram contains no pages.");
                    return;
                }

                // Get the first page
                Page page = diagram.Pages[0];

                // Ensure the page contains at least one shape
                if (page.Shapes.Count == 0)
                {
                    Console.Error.WriteLine("Error: Page contains no shapes.");
                    return;
                }

                // Get the first shape on the page
                Shape shape = page.Shapes[0];

                // Prepare SVG save options
                SVGSaveOptions svgOptions = new SVGSaveOptions
                {
                    // Fit the SVG to the viewport (preserve viewbox)
                    SVGFitToViewPort = true,               // changed from BOOL.True to bool
                    // Do not export guide shapes
                    ExportGuideShapes = false,              // changed from BOOL.False to bool
                    // Do not export comments
                    IsExportComments = false,               // changed from BOOL.False to bool
                    // Export only the first page (current page)
                    PageIndex = 0
                    // PageCount property omitted because it is not required for shape export
                };

                // Convert the shape to SVG using the specified options
                shape.ToSvg(outputPath, svgOptions);

                Console.WriteLine($"Shape successfully exported to SVG: {outputPath}");
            }
        }
        catch (DiagramException dex)
        {
            // Handle Aspose.Diagram specific exceptions
            Console.Error.WriteLine("DiagramException occurred during SVG conversion:");
            Console.Error.WriteLine(dex.Message);
            if (dex.InnerException != null)
            {
                Console.Error.WriteLine("Inner exception:");
                Console.Error.WriteLine(dex.InnerException.Message);
            }
        }
        catch (Exception ex)
        {
            // Handle any other unexpected exceptions
            Console.Error.WriteLine("An unexpected error occurred:");
            Console.Error.WriteLine(ex.Message);
        }
    }
}
