using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Validate command‑line arguments: input file, output file, shape ID
        if (args.Length < 3)
        {
            Console.Error.WriteLine("Usage: <program> <inputDiagramPath> <outputDiagramPath> <shapeId>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];
        string shapeIdText = args[2];

        // Guard: input file must exist
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Error: Input file \"{inputPath}\" does not exist.");
            return;
        }

        // Parse shape ID as long
        if (!long.TryParse(shapeIdText, out long shapeId))
        {
            Console.Error.WriteLine($"Error: Unable to parse shape ID \"{shapeIdText}\" as a number.");
            return;
        }

        try
        {
            // Load the Visio diagram
            Diagram diagram = new Diagram(inputPath);

            // Locate the shape with the specified ID across all pages
            Shape targetShape = null;
            foreach (Page page in diagram.Pages)
            {
                // GetShape expects an int; cast the long ID safely
                Shape shape = page.Shapes.GetShape((int)shapeId);
                if (shape != null)
                {
                    targetShape = shape;
                    break;
                }
            }

            if (targetShape == null)
            {
                Console.Error.WriteLine($"Error: Shape with ID {shapeId} was not found in the diagram.");
                return;
            }

            // Ensure the shape is not already deleted
            if (targetShape.Del == BOOL.True)
            {
                Console.Error.WriteLine($"Error: Shape with ID {shapeId} is marked as deleted.");
                return;
            }

            // Lock the rotation attribute of the shape
            targetShape.Protection.LockRotate.Value = BOOL.True;

            // Save the modified diagram to the specified output path
            diagram.Save(outputPath, SaveFileFormat.Vsdx);

            Console.WriteLine($"Successfully locked rotation for shape ID {shapeId} and saved to \"{outputPath}\".");
        }
        catch (Exception ex)
        {
            // Capture any Aspose.Diagram related errors
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}