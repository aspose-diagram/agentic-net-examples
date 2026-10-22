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
        string shapeNameU = args[1];
        string eventFormula = args[2];
        string outputPath = args[3];

        // Guard against missing input file.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file does not exist: {inputPath}");
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
                    if (shape.NameU == shapeNameU)
                    {
                        targetShape = shape;
                        break;
                    }
                }
                if (targetShape != null)
                    break;
            }

            if (targetShape == null)
            {
                Console.Error.WriteLine($"Shape with NameU \"{shapeNameU}\" not found.");
                return;
            }

            // Set the double‑click event formula using the correct API pattern.
            // The formula should be a valid Visio expression, e.g., "CALLTHIS(\"ShowAlert\")".
            targetShape.Event.EventDblClick.Ufe.F = eventFormula;

            // Save the modified diagram to the specified output path.
            diagram.Save(outputPath, SaveFileFormat.Vsdx);

            Console.WriteLine("EventDblClick cell updated and diagram saved successfully.");
        }
        catch (Exception ex)
        {
            // Write any Aspose or runtime errors to the error stream.
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}