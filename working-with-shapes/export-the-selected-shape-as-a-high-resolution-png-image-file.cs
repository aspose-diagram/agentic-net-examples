using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Expecting three arguments: input diagram path, shape ID, output PNG path.
        if (args.Length < 3)
        {
            Console.Error.WriteLine("Usage: <program> <inputDiagram.vsdx> <shapeId> <outputImage.png> [resolutionDPI]");
            return;
        }

        string diagramPath = args[0];
        string shapeIdArg = args[1];
        string outputPath = args[2];
        float resolution = 300f; // default high resolution DPI

        // Optional resolution argument
        if (args.Length >= 4 && float.TryParse(args[3], out float parsedRes) && parsedRes > 0)
        {
            resolution = parsedRes;
        }

        // Guard against missing input file
        if (!File.Exists(diagramPath))
        {
            Console.Error.WriteLine($"Input file does not exist: {diagramPath}");
            return;
        }

        // Guard against invalid shape ID
        if (!long.TryParse(shapeIdArg, out long shapeId))
        {
            Console.Error.WriteLine($"Invalid shape ID: {shapeIdArg}");
            return;
        }

        try
        {
            // Load the Visio diagram
            Diagram diagram = new Diagram(diagramPath);

            // Use the first page (index 0) for simplicity
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

            // Configure image save options for high‑resolution PNG
            ImageSaveOptions saveOptions = new ImageSaveOptions(SaveFileFormat.Png);
            saveOptions.Resolution = resolution; // DPI

            // Export the selected shape to PNG
            shape.ToImage(outputPath, saveOptions);

            Console.WriteLine($"Shape {shapeId} exported successfully to {outputPath} at {resolution} DPI.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}