using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Define input Visio file and output SVG file paths
        string inputPath = "input.vsdx";
        if (!System.IO.File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }
        string outputPath = "shape.svg";

        // Guard: ensure the input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the diagram inside a using block as required
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Verify the diagram contains at least one page
                if (diagram.Pages.Count == 0)
                {
                    Console.Error.WriteLine("The diagram contains no pages.");
                    return;
                }

                // Get the first page
                Page page = diagram.Pages[0];

                // Verify the page contains at least one shape
                if (page.Shapes.Count == 0)
                {
                    Console.Error.WriteLine("The page contains no shapes.");
                    return;
                }

                // Retrieve the first shape on the page
                Shape shape = page.Shapes[0];

                // Create SVG save options (default settings)
                SVGSaveOptions svgOptions = new SVGSaveOptions();

                // Export the shape to an SVG file
                shape.ToSvg(outputPath, svgOptions);

                Console.WriteLine($"Shape successfully exported to SVG: {outputPath}");
            }
        }
        catch (Exception ex)
        {
            // Write any errors to the error console
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}