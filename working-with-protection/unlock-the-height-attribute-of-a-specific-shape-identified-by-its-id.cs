using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Validate command‑line arguments: input file, shape ID, output file
        if (args.Length < 3)
        {
            Console.Error.WriteLine("Usage: <program> <inputDiagramPath> <shapeId> <outputDiagramPath>");
            return;
        }

        string inputPath = args[0];
        string shapeIdArg = args[1];
        string outputPath = args[2];

        // Guard: ensure the input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Parse the shape ID (expected to be a numeric value)
        if (!long.TryParse(shapeIdArg, out long shapeId))
        {
            Console.Error.WriteLine($"Invalid shape ID: {shapeIdArg}");
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
                // GetShape expects an int ID; cast safely
                int idAsInt = (int)shapeId;
                try
                {
                    Shape shape = page.Shapes.GetShape(idAsInt);
                    if (shape != null)
                    {
                        targetShape = shape;
                        break;
                    }
                }
                catch (ArgumentOutOfRangeException)
                {
                    // Shape not found on this page; continue searching
                }
            }

            if (targetShape == null)
            {
                Console.Error.WriteLine($"Shape with ID {shapeId} not found in the diagram.");
                return;
            }

            // Ensure the shape is not marked as deleted before modifying protection
            if (targetShape.Del == BOOL.True)
            {
                Console.Error.WriteLine($"Shape with ID {shapeId} is marked as deleted and cannot be modified.");
                return;
            }

            // Unlock the Height attribute by setting the corresponding protection flag to FALSE
            targetShape.Protection.LockHeight.Value = BOOL.False;

            // Save the modified diagram to the specified output path
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Shape height protection unlocked and diagram saved to: {outputPath}");
        }
        catch (Exception ex)
        {
            // Capture any Aspose.Diagram or I/O errors
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}