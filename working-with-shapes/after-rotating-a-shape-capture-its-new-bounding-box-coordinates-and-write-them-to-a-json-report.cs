using System;
using System.IO;
using System.Text.Json;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Expect three arguments: input diagram path, output JSON path, shape ID, rotation angle (degrees)
        if (args.Length < 4)
        {
            Console.Error.WriteLine("Usage: <program> <inputDiagramPath> <outputJsonPath> <shapeId> <rotationDegrees>");
            return;
        }

        string inputPath = args[0];
        string outputJsonPath = args[1];
        string shapeIdArg = args[2];
        string rotationArg = args[3];

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
        if (!double.TryParse(rotationArg, out double rotationDegrees))
        {
            Console.Error.WriteLine($"Invalid rotation angle: {rotationArg}");
            return;
        }

        try
        {
            // Load the diagram inside a using block as required
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Assume operation on the first page
                if (diagram.Pages.Count == 0)
                {
                    Console.Error.WriteLine("The diagram contains no pages.");
                    return;
                }

                var page = diagram.Pages[0];

                // Retrieve the shape by its structural ID
                Shape shape = page.Shapes.GetShape(shapeId);
                if (shape == null)
                {
                    Console.Error.WriteLine($"Shape with ID {shapeId} not found on page 0.");
                    return;
                }

                // Rotate the shape to the specified angle (degrees)
                shape.XForm.Angle.Value = rotationDegrees;

                // Capture bounding box coordinates after rotation
                double pinX = shape.XForm.PinX.Value;
                double pinY = shape.XForm.PinY.Value;
                double width = shape.XForm.Width.Value;
                double height = shape.XForm.Height.Value;

                double left = pinX - width / 2.0;
                double right = pinX + width / 2.0;
                double top = pinY + height / 2.0;
                double bottom = pinY - height / 2.0;

                // Prepare report object
                var report = new
                {
                    ShapeId = shapeId,
                    RotationDegrees = rotationDegrees,
                    BoundingBox = new
                    {
                        Left = left,
                        Right = right,
                        Top = top,
                        Bottom = bottom
                    }
                };

                // Serialize report to JSON with indentation
                string json = JsonSerializer.Serialize(report, new JsonSerializerOptions { WriteIndented = true });

                // Write JSON to the specified output file
                File.WriteAllText(outputJsonPath, json);
                Console.WriteLine($"Bounding box report written to: {outputJsonPath}");
            }
        }
        catch (Exception ex)
        {
            // Capture any Aspose or I/O errors
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}