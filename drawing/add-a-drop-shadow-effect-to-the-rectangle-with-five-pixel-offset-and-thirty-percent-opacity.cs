using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Validate command‑line arguments.
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <inputVisioPath> <outputVisioPath>");
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
            // Load the Visio diagram inside a using block as required.
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Access the first page (index 0).
                Page page = diagram.Pages[0];

                // Find the first rectangle shape on the page.
                Shape rectangleShape = null;
                foreach (Shape shape in page.Shapes)
                {
                    // Ensure the shape has a master and compare its name.
                    if (shape.Master != null && shape.Master.Name == "Rectangle")
                    {
                        rectangleShape = shape;
                        break;
                    }
                }

                if (rectangleShape == null)
                {
                    Console.Error.WriteLine("No rectangle shape found on the first page.");
                    return;
                }

                // Apply drop shadow effect.
                // Set simple shadow type.
                rectangleShape.Fill.ShapeShdwType.Value = ShapeShdwTypeValue.Simple;
                // Set shadow color to black.
                rectangleShape.Fill.ShdwForegnd.Value = "#000000";
                // Set shadow transparency to 70% (i.e., 30% opacity).
                rectangleShape.Fill.ShdwForegndTrans.Value = 0.7;
                // Set shadow offset to 5 units (interpreted as points/inches depending on diagram settings).
                rectangleShape.Fill.ShapeShdwOffsetX.Value = 5;
                rectangleShape.Fill.ShapeShdwOffsetY.Value = 5;

                // Save the modified diagram.
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
                Console.WriteLine($"Diagram saved with shadow effect to: {outputPath}");
            }
        }
        catch (Exception ex)
        {
            // Write any Aspose or IO errors to the error stream.
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}