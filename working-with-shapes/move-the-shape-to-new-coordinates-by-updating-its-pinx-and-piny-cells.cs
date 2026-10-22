using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Validate arguments count
        if (args.Length < 5)
        {
            Console.Error.WriteLine("Usage: <inputVisioPath> <shapeId> <newPinX> <newPinY> <outputVisioPath>");
            return;
        }

        string inputPath = args[0];
        string shapeIdArg = args[1];
        string newPinXArg = args[2];
        string newPinYArg = args[3];
        string outputPath = args[4];

        // Guard: check input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file does not exist: {inputPath}");
            return;
        }

        // Parse numeric arguments
        if (!int.TryParse(shapeIdArg, out int shapeId))
        {
            Console.Error.WriteLine($"Invalid shape ID: {shapeIdArg}");
            return;
        }

        if (!double.TryParse(newPinXArg, out double newPinX))
        {
            Console.Error.WriteLine($"Invalid PinX value: {newPinXArg}");
            return;
        }

        if (!double.TryParse(newPinYArg, out double newPinY))
        {
            Console.Error.WriteLine($"Invalid PinY value: {newPinYArg}");
            return;
        }

        try
        {
            // Load the Visio diagram
            Diagram diagram = new Diagram(inputPath);

            // Ensure there is at least one page
            if (diagram.Pages.Count == 0)
            {
                Console.Error.WriteLine("The diagram contains no pages.");
                return;
            }

            // Access the first page (adjust if needed)
            Page page = diagram.Pages[0];

            // Retrieve the shape by its ID
            Shape shape = page.Shapes.GetShape(shapeId);
            if (shape == null)
            {
                Console.Error.WriteLine($"Shape with ID {shapeId} not found on the first page.");
                return;
            }

            // Update the shape's position
            shape.XForm.PinX.Value = newPinX;
            shape.XForm.PinY.Value = newPinY;

            // Save the modified diagram to the specified output path
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Shape {shapeId} moved to ({newPinX}, {newPinY}) and saved to {outputPath}");
        }
        catch (Exception ex)
        {
            // Write any Aspose or runtime errors to the error stream
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}