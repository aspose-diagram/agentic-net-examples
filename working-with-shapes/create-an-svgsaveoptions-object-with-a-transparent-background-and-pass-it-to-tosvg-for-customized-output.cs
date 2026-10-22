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
        // Output SVG file path (second argument or default)
        string outputPath = args.Length > 1 ? args[1] : "shape.svg";

        // Guard: ensure the input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the diagram inside a using block as required
        using (Diagram diagram = new Diagram(inputPath))
        {
            try
            {
                // Ensure the document contains at least one page
                if (diagram.Pages.Count == 0)
                {
                    Console.Error.WriteLine("The diagram does not contain any pages.");
                    return;
                }

                // Get the first page
                Page page = diagram.Pages[0];

                // Ensure the page contains at least one shape
                if (page.Shapes.Count == 0)
                {
                    Console.Error.WriteLine("The first page does not contain any shapes.");
                    return;
                }

                // Get the first shape on the page
                Shape shape = page.Shapes[0];

                // Create SVGSaveOptions with default (transparent) background
                SVGSaveOptions svgOptions = new SVGSaveOptions();

                // Optional: fit the SVG to the viewport (preserves viewbox)
                svgOptions.SVGFitToViewPort = true;

                // Export the shape to SVG using the customized options
                shape.ToSvg(outputPath, svgOptions);

                Console.WriteLine($"Shape exported successfully to: {outputPath}");
            }
            catch (Exception ex)
            {
                // Write any Aspose or runtime errors to the error console
                Console.Error.WriteLine($"An error occurred: {ex.Message}");
            }
        }
    }
}