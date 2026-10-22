using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Expect three arguments: input diagram path, shape ID, new line weight (in inches)
        if (args.Length < 3)
        {
            Console.Error.WriteLine("Usage: Program <inputDiagramPath> <shapeId> <newLineWeight>");
            return;
        }

        string inputPath = args[0];
        string shapeIdArg = args[1];
        string newWeightArg = args[2];

        // Guard: verify input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Error: File not found - {inputPath}");
            return;
        }

        // Parse shape ID and new line weight
        if (!int.TryParse(shapeIdArg, out int shapeId))
        {
            Console.Error.WriteLine($"Error: Invalid shape ID '{shapeIdArg}'.");
            return;
        }

        if (!double.TryParse(newWeightArg, out double newLineWeight))
        {
            Console.Error.WriteLine($"Error: Invalid line weight '{newWeightArg}'.");
            return;
        }

        try
        {
            // Load the Visio diagram
            Diagram diagram = new Diagram(inputPath);

            // Get the first page (index 0)
            Page page = diagram.Pages[0];

            // Retrieve the shape by its ID
            Shape shape = page.Shapes.GetShape(shapeId);
            if (shape == null)
            {
                Console.Error.WriteLine($"Error: Shape with ID {shapeId} not found on page.");
                return;
            }

            // Update the line weight (in inches)
            shape.Line.LineWeight.Value = newLineWeight;

            // Verify the update
            double actualWeight = shape.Line.LineWeight.Value;
            if (Math.Abs(actualWeight - newLineWeight) > 0.0001)
            {
                throw new Exception($"Verification failed: Expected line weight {newLineWeight}, but got {actualWeight}.");
            }
            else
            {
                Console.WriteLine($"Success: Shape ID {shapeId} line weight set to {actualWeight} inches.");
            }

            // Save the modified diagram to a new file
            string outputPath = Path.Combine(
                Path.GetDirectoryName(inputPath) ?? "",
                Path.GetFileNameWithoutExtension(inputPath) + "_modified.vsdx");

            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Diagram saved to: {outputPath}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}