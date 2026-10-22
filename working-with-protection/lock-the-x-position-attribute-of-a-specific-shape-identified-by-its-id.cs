using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Validate command‑line arguments.
        if (args.Length < 3)
        {
            Console.Error.WriteLine("Usage: <inputVisioPath> <shapeId> <outputVisioPath>");
            return;
        }

        string inputPath = args[0];
        string shapeIdArg = args[1];
        string outputPath = args[2];

        // Guard: ensure the input file exists.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Parse the shape ID (expected to be a long integer).
        if (!long.TryParse(shapeIdArg, out long shapeId))
        {
            Console.Error.WriteLine($"Invalid shape ID: {shapeIdArg}");
            return;
        }

        try
        {
            // Load the Visio diagram.
            Diagram diagram = new Diagram(inputPath);

            // Flag to indicate whether the shape was found.
            bool shapeFound = false;

            // Iterate through all pages to locate the shape by its ID.
            foreach (Page page in diagram.Pages)
            {
                // The GetShape method expects an int ID; cast safely.
                Shape shape = page.Shapes.GetShape((int)shapeId);
                if (shape != null)
                {
                    // Lock the X‑position (horizontal movement) of the shape.
                    shape.Protection.LockMoveX.Value = BOOL.True;

                    shapeFound = true;
                    break; // Shape IDs are unique; exit once found.
                }
            }

            if (!shapeFound)
            {
                Console.Error.WriteLine($"Shape with ID {shapeId} not found in the diagram.");
                return;
            }

            // Save the modified diagram to the specified output path.
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Shape ID {shapeId} X‑position locked and diagram saved to {outputPath}.");
        }
        catch (Exception ex)
        {
            // Write any Aspose or I/O errors to the error stream.
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}