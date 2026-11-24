using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Expect three arguments: input diagram path, output diagram path, shape ID to lock X‑position.
        if (args.Length < 3)
        {
            Console.Error.WriteLine("Usage: <program> <inputDiagramPath> <outputDiagramPath> <shapeId>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];
        string shapeIdArg = args[2];

        // Guard: verify input file exists.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file does not exist: {inputPath}");
            return;
        }

        // Parse shape ID.
        if (!long.TryParse(shapeIdArg, out long shapeId))
        {
            Console.Error.WriteLine($"Invalid shape ID: {shapeIdArg}");
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
                try
                {
                    // GetShape expects an int ID; cast safely.
                    targetShape = page.Shapes.GetShape((int)shapeId);
                    if (targetShape != null)
                    {
                        break;
                    }
                }
                catch
                {
                    // Shape not found on this page; continue searching.
                }
            }

            if (targetShape == null)
            {
                Console.Error.WriteLine($"Shape with ID {shapeId} not found in the diagram.");
                return;
            }

            // Lock the X‑position (horizontal movement) of the shape.
            targetShape.Protection.LockMoveX.Value = BOOL.True;

            // Save the modified diagram to the specified output path.
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Shape ID {shapeId} X‑position locked and diagram saved to {outputPath}.");
        }
        catch (Exception ex)
        {
            // Report any errors that occur during processing.
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}