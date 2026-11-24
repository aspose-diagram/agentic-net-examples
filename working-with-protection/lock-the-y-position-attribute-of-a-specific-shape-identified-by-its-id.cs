using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Validate command‑line arguments: input file, shape ID, output file
        if (args.Length != 3)
        {
            Console.Error.WriteLine("Usage: Program <inputDiagramPath> <shapeId> <outputDiagramPath>");
            return;
        }

        string inputPath = args[0];
        string shapeIdArg = args[1];
        string outputPath = args[2];

        // Guard: ensure the input Visio file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Error: Input file \"{inputPath}\" does not exist.");
            return;
        }

        // Parse the shape ID (expected to be a numeric value)
        if (!long.TryParse(shapeIdArg, out long shapeId))
        {
            Console.Error.WriteLine($"Error: Shape ID \"{shapeIdArg}\" is not a valid integer.");
            return;
        }

        try
        {
            // Load the diagram
            Diagram diagram = new Diagram(inputPath);

            // Locate the shape with the specified ID across all pages
            Shape targetShape = null;
            foreach (Page page in diagram.Pages)
            {
                try
                {
                    // GetShape expects an int; cast the long ID safely
                    Shape shape = page.Shapes.GetShape((int)shapeId);
                    if (shape != null)
                    {
                        targetShape = shape;
                        break;
                    }
                }
                catch
                {
                    // GetShape throws if the ID is not present on this page – ignore and continue
                }
            }

            if (targetShape == null)
            {
                Console.Error.WriteLine($"Error: No shape with ID {shapeId} was found in the diagram.");
                return;
            }

            // Lock the Y‑position (vertical movement) of the shape
            // The corresponding protection property is LockMoveY
            targetShape.Protection.LockMoveY.Value = BOOL.True;

            // Save the modified diagram to the specified output path
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Shape ID {shapeId} Y‑position locked successfully. Saved to \"{outputPath}\".");
        }
        catch (Exception ex)
        {
            // Report any Aspose or I/O errors
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}