using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Validate command‑line arguments.
        if (args.Length < 4)
        {
            Console.Error.WriteLine("Usage: <inputVisioPath> <shapeNameU> <eventFormula> <outputVisioPath>");
            return;
        }

        string inputPath = args[0];
        string targetShapeNameU = args[1];
        string eventFormula = args[2];
        string outputPath = args[3];

        // Guard: ensure the input file exists.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the Visio diagram.
            Diagram diagram = new Diagram(inputPath);

            // Locate the shape with the specified NameU across all pages.
            Shape targetShape = null;
            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    // Shape.NameU holds the universal name of the shape.
                    if (shape.NameU != null && shape.NameU.Equals(targetShapeNameU, StringComparison.OrdinalIgnoreCase))
                    {
                        targetShape = shape;
                        break;
                    }
                }
                if (targetShape != null) break;
            }

            if (targetShape == null)
            {
                Console.Error.WriteLine($"Shape with NameU \"{targetShapeNameU}\" not found in the diagram.");
                return;
            }

            // Set the EventDblClick cell formula using the Ufe.F property.
            // This defines the action that will be executed when the shape is double‑clicked.
            targetShape.Event.EventDblClick.Ufe.F = eventFormula;

            // Save the modified diagram to the specified output path.
            diagram.Save(outputPath, SaveFileFormat.Vsdx);

            Console.WriteLine($"Successfully updated EventDblClick for shape \"{targetShapeNameU}\" and saved to \"{outputPath}\".");
        }
        catch (Exception ex)
        {
            // Write any Aspose or I/O errors to the error stream.
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}