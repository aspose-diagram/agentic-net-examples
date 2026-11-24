using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Validate command‑line arguments.
        // Expected: <inputDiagramPath> <shapeIdOrName> <newXCoordinate> <outputDiagramPath>
        if (args.Length < 4)
        {
            Console.Error.WriteLine("Usage: Program <inputDiagramPath> <shapeIdOrName> <newXCoordinate> <outputDiagramPath>");
            return;
        }

        string inputPath = args[0];
        string shapeIdentifier = args[1];
        string newXStr = args[2];
        string outputPath = args[3];

        // Guard: ensure the input file exists.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Error: Input file \"{inputPath}\" does not exist.");
            return;
        }

        // Parse the new X coordinate.
        if (!double.TryParse(newXStr, out double newX))
        {
            Console.Error.WriteLine($"Error: Unable to parse \"{newXStr}\" as a double value for the X coordinate.");
            return;
        }

        try
        {
            // Load the Visio diagram.
            Diagram diagram = new Diagram(inputPath);

            // Locate the target shape by ID (numeric) or by NameU (string).
            Shape targetShape = null;
            bool identifierIsId = long.TryParse(shapeIdentifier, out long targetId);

            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    if (identifierIsId && shape.ID == targetId)
                    {
                        targetShape = shape;
                        break;
                    }
                    else if (!identifierIsId && shape.NameU == shapeIdentifier)
                    {
                        targetShape = shape;
                        break;
                    }
                }
                if (targetShape != null)
                    break;
            }

            // If the shape was not found, report an error.
            if (targetShape == null)
            {
                Console.Error.WriteLine($"Error: Shape \"{shapeIdentifier}\" not found in the diagram.");
                return;
            }

            // Preserve the existing Y coordinate and set the new X coordinate.
            // XForm.PinX controls the horizontal position; PinY remains unchanged.
            targetShape.XForm.PinX.Value = newX;

            // Save the modified diagram to the specified output path.
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Shape \"{shapeIdentifier}\" X coordinate updated to {newX} and diagram saved to \"{outputPath}\".");
        }
        catch (Exception ex)
        {
            // Write any Aspose or runtime errors to the error stream.
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}