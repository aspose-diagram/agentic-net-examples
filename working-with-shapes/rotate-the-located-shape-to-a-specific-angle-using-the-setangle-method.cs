using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Expecting four arguments: input diagram path, shape ID, rotation angle (degrees), output diagram path
        if (args.Length < 4)
        {
            Console.Error.WriteLine("Usage: <program> <inputDiagramPath> <shapeId> <angleDegrees> <outputDiagramPath>");
            return;
        }

        string inputPath = args[0];
        string shapeIdArg = args[1];
        string angleArg = args[2];
        string outputPath = args[3];

        // Guard: verify input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file does not exist: {inputPath}");
            return;
        }

        // Parse shape ID
        if (!long.TryParse(shapeIdArg, out long shapeId))
        {
            Console.Error.WriteLine($"Invalid shape ID: {shapeIdArg}");
            return;
        }

        // Parse rotation angle
        if (!double.TryParse(angleArg, out double angleDegrees))
        {
            Console.Error.WriteLine($"Invalid angle value: {angleArg}");
            return;
        }

        try
        {
            // Load the diagram inside a using block as required
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Retrieve the first page (or active page) to locate the shape
                Page page = diagram.Pages[0];

                // Locate the shape by its ID
                Shape shape = page.Shapes.GetShape(shapeId);
                if (shape == null)
                {
                    Console.Error.WriteLine($"Shape with ID {shapeId} not found on page 0.");
                    return;
                }

                // Rotate the shape to the specified angle using SetAngle (degrees)
                shape.SetAngle(angleDegrees);

                // Save the modified diagram to the output path in VSDX format
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
                Console.WriteLine($"Shape {shapeId} rotated to {angleDegrees} degrees and saved to {outputPath}");
            }
        }
        catch (Exception ex)
        {
            // Write any Aspose or runtime errors to the error stream
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}