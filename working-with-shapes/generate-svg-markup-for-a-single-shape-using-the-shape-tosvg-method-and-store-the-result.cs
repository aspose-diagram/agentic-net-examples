using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Expect three arguments: input Visio file, shape ID, output SVG file
        if (args.Length < 3)
        {
            Console.Error.WriteLine("Usage: Program <inputVisioFile> <shapeId> <outputSvgFile>");
            return;
        }

        string inputPath = args[0];
        string shapeIdStr = args[1];
        string outputPath = args[2];

        // Guard: ensure the input Visio file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file does not exist: {inputPath}");
            return;
        }

        // Guard: parse the shape ID as an integer
        if (!int.TryParse(shapeIdStr, out int shapeId))
        {
            Console.Error.WriteLine($"Invalid shape ID: {shapeIdStr}");
            return;
        }

        try
        {
            // Load the diagram inside a using block as required by the API
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Retrieve the first page (index 0). Adjust if a different page is needed.
                Page page = diagram.Pages[0];

                // Get the shape by its ID from the page's shape collection
                Shape shape = page.Shapes.GetShape(shapeId);
                if (shape == null)
                {
                    Console.Error.WriteLine($"Shape with ID {shapeId} not found on page 0.");
                    return;
                }

                // Create default SVG save options
                SVGSaveOptions svgOptions = new SVGSaveOptions();

                // Export the selected shape to an SVG file
                shape.ToSvg(outputPath, svgOptions);

                Console.WriteLine($"Shape {shapeId} exported to SVG successfully: {outputPath}");
            }
        }
        catch (Exception ex)
        {
            // Write any errors that occur during processing to the error stream
            Console.Error.WriteLine($"Error during SVG export: {ex.Message}");
        }
    }
}