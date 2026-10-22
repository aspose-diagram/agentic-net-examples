using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Validate command‑line arguments: input file, output file, shape ID, rotation angle.
        if (args.Length < 4)
        {
            Console.Error.WriteLine("Usage: <program> <inputDiagramPath> <outputDiagramPath> <shapeId> <angleInDegrees>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];
        string shapeIdArg = args[2];
        string angleArg = args[3];

        // Guard: ensure the input file exists.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file does not exist: {inputPath}");
            return;
        }

        // Parse shape ID.
        if (!int.TryParse(shapeIdArg, out int shapeId))
        {
            Console.Error.WriteLine($"Invalid shape ID: {shapeIdArg}");
            return;
        }

        // Parse rotation angle (degrees).
        if (!double.TryParse(angleArg, out double angleDeg))
        {
            Console.Error.WriteLine($"Invalid angle value: {angleArg}");
            return;
        }

        try
        {
            // Load the Visio diagram.
            Diagram diagram = new Diagram(inputPath);

            // Locate the shape with the specified ID across all pages.
            Shape targetShape = null;
            foreach (Page page in diagram.Pages)
            {
                // GetShape expects an integer ID.
                Shape shape = page.Shapes.GetShape(shapeId);
                if (shape != null)
                {
                    targetShape = shape;
                    break;
                }
            }

            if (targetShape == null)
            {
                Console.Error.WriteLine($"Shape with ID {shapeId} not found in the diagram.");
                return;
            }

            // Apply the rotation angle (degrees) to the shape's XForm.
            targetShape.XForm.Angle.Value = angleDeg;

            // Save the modified diagram. Preserve the original format (VSDX assumed here).
            diagram.Save(outputPath, SaveFileFormat.Vsdx);

            Console.WriteLine($"Shape ID {shapeId} rotated to {angleDeg} degrees and saved to {outputPath}.");
        }
        catch (Exception ex)
        {
            // Write any unexpected errors to the error stream.
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}