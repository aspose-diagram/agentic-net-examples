using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Expect arguments: inputPath shapeId newWidth newHeight outputPath
        if (args.Length < 5)
        {
            Console.Error.WriteLine("Usage: <program> <inputDiagramPath> <shapeId> <newWidth> <newHeight> <outputDiagramPath>");
            return;
        }

        string inputPath = args[0];
        string shapeIdArg = args[1];
        string widthArg = args[2];
        string heightArg = args[3];
        string outputPath = args[4];

        // Guard: input file must exist
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file does not exist: {inputPath}");
            return;
        }

        // Parse shape ID, width and height
        if (!long.TryParse(shapeIdArg, out long shapeId))
        {
            Console.Error.WriteLine($"Invalid shape ID: {shapeIdArg}");
            return;
        }

        if (!double.TryParse(widthArg, out double newWidth))
        {
            Console.Error.WriteLine($"Invalid width value: {widthArg}");
            return;
        }

        if (!double.TryParse(heightArg, out double newHeight))
        {
            Console.Error.WriteLine($"Invalid height value: {heightArg}");
            return;
        }

        try
        {
            // Load the diagram from the specified file
            Diagram diagram = new Diagram(inputPath);

            // Assume the shape is on the first page
            Page page = diagram.Pages[0];

            // Retrieve the shape by its ID (cast to int as required by GetShape)
            Shape shape = page.Shapes.GetShape((int)shapeId);
            if (shape == null)
            {
                Console.Error.WriteLine($"Shape with ID {shapeId} not found on the first page.");
                return;
            }

            // Resize the shape using the provided dimensions
            shape.SetWidth(newWidth);
            shape.SetHeight(newHeight);

            // Save the modified diagram to the output path in VSDX format
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Shape resized and diagram saved to: {outputPath}");
        }
        catch (Exception ex)
        {
            // Write any Aspose or runtime errors to the error stream
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}