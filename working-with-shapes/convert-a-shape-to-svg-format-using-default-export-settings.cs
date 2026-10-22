using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Expect three arguments: input Visio file, shape ID, output SVG file path
        if (args.Length < 3)
        {
            Console.Error.WriteLine("Usage: <program> <inputVisioPath> <shapeId> <outputSvgPath>");
            return;
        }

        string visioPath = args[0];
        string shapeIdArg = args[1];
        string outputSvgPath = args[2];

        // Guard: check input Visio file exists
        if (!File.Exists(visioPath))
        {
            Console.Error.WriteLine($"Input file does not exist: {visioPath}");
            return;
        }

        // Guard: validate shape ID
        if (!long.TryParse(shapeIdArg, out long shapeId))
        {
            Console.Error.WriteLine($"Invalid shape ID: {shapeIdArg}");
            return;
        }

        try
        {
            // Load the Visio diagram
            Diagram diagram = new Diagram(visioPath);

            // Use the first page (index 0) for shape retrieval
            if (diagram.Pages.Count == 0)
            {
                Console.Error.WriteLine("The diagram contains no pages.");
                return;
            }

            Page page = diagram.Pages[0];

            // Retrieve the shape by its ID
            Shape shape = page.Shapes.GetShape(shapeId);
            if (shape == null)
            {
                Console.Error.WriteLine($"Shape with ID {shapeId} not found on page 0.");
                return;
            }

            // Export the shape to SVG using default settings
            SVGSaveOptions svgOptions = new SVGSaveOptions();
            shape.ToSvg(outputSvgPath, svgOptions);

            Console.WriteLine($"Shape {shapeId} successfully exported to SVG: {outputSvgPath}");
        }
        catch (Exception ex)
        {
            // Write any errors to the error console
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}