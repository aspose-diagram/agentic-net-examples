using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Expected arguments:
        // 0 - input Visio file path
        // 1 - shape ID (integer)
        // 2 - new width (double, inches)
        // 3 - new height (double, inches)
        // 4 - output Visio file path
        if (args.Length < 5)
        {
            Console.Error.WriteLine("Usage: <inputPath> <shapeId> <newWidth> <newHeight> <outputPath>");
            return;
        }

        string inputPath = args[0];
        string shapeIdArg = args[1];
        string newWidthArg = args[2];
        string newHeightArg = args[3];
        string outputPath = args[4];

        // Guard: ensure input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file does not exist: {inputPath}");
            return;
        }

        // Parse numeric arguments with validation
        if (!int.TryParse(shapeIdArg, out int shapeId))
        {
            Console.Error.WriteLine($"Invalid shape ID: {shapeIdArg}");
            return;
        }

        if (!double.TryParse(newWidthArg, out double newWidth))
        {
            Console.Error.WriteLine($"Invalid width value: {newWidthArg}");
            return;
        }

        if (!double.TryParse(newHeightArg, out double newHeight))
        {
            Console.Error.WriteLine($"Invalid height value: {newHeightArg}");
            return;
        }

        try
        {
            // Load the diagram from the specified file
            Diagram diagram = new Diagram(inputPath);

            // Assume the shape is on the first page; adjust as needed
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
                Console.Error.WriteLine($"Shape with ID {shapeId} not found on the first page.");
                return;
            }

            // Set the new width and height (values are in inches)
            shape.XForm.Width.Value = newWidth;
            shape.XForm.Height.Value = newHeight;

            // Save the modified diagram to the output path in VSDX format
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Shape resized and diagram saved to: {outputPath}");
        }
        catch (Exception ex)
        {
            // Write any Aspose or runtime errors to the error stream
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}