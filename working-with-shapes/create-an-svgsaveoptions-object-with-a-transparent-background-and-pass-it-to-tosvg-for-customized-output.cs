using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Validate input arguments.
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <inputVisioFile> <outputSvgFile>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

        // Guard: ensure the input Visio file exists.
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
                // Guard: ensure the diagram contains at least one page.
                if (diagram.Pages.Count == 0)
                {
                    Console.Error.WriteLine("The diagram does not contain any pages.");
                    return;
                }

                // Get the first page.
                Page page = diagram.Pages[0];

                // Guard: ensure the page contains at least one shape.
                if (page.Shapes.Count == 0)
                {
                    Console.Error.WriteLine("The first page does not contain any shapes.");
                    return;
                }

                // Get the first shape on the page.
                Shape shape = page.Shapes[0];

                // Create SVGSaveOptions with default settings.
                // Transparent background is the default behavior for SVG export.
                SVGSaveOptions svgOptions = new SVGSaveOptions();

                // Optional: fit the SVG to the viewport.
                svgOptions.SVGFitToViewPort = true;

                // Export the selected shape to SVG using the customized options.
                shape.ToSvg(outputPath, svgOptions);

                Console.WriteLine($"Shape exported successfully to SVG: {outputPath}");
            }
        }
        catch (Exception ex)
        {
            // Write any errors to the error console.
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}