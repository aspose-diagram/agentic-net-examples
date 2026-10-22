using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Expect input and output file paths as command‑line arguments.
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <inputVisioFile> <outputVisioFile>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

        // Guard against missing input file.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the Visio diagram.
            Diagram diagram = new Diagram(inputPath);

            // Assume we work with the first page.
            Page page = diagram.Pages[0];

            // Find the first rectangle shape on the page.
            Shape rectangleShape = null;
            foreach (Shape shape in page.Shapes)
            {
                // Ensure the shape has a master and its name is "Rectangle".
                if (shape.Master != null && shape.Master.Name == "Rectangle")
                {
                    rectangleShape = shape;
                    break;
                }
            }

            if (rectangleShape == null)
            {
                Console.Error.WriteLine("No rectangle shape found in the diagram.");
                return;
            }

            // Apply drop shadow effect:
            // 1. Set simple shadow type.
            rectangleShape.Fill.ShapeShdwType.Value = ShapeShdwTypeValue.Simple;
            // 2. Set shadow color to black.
            rectangleShape.Fill.ShdwForegnd.Value = "#000000";
            // 3. Set shadow opacity to 30% (0.3 = 30% transparent).
            rectangleShape.Fill.ShdwForegndTrans.Value = 0.3;
            // 4. Set horizontal and vertical offset to 5 (Visio units, typically inches; adjust as needed).
            rectangleShape.Fill.ShapeShdwOffsetX.Value = 5;
            rectangleShape.Fill.ShapeShdwOffsetY.Value = 5;

            // Save the modified diagram.
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Diagram saved successfully to: {outputPath}");
        }
        catch (Exception ex)
        {
            // Write any Aspose or IO errors to the error stream.
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}