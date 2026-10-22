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
            Console.Error.WriteLine("Usage: <program> <inputVisioFile> <shapeId> <outputVisioFile>");
            return;
        }

        string inputPath = args[0];
        string shapeIdText = args[1];
        string outputPath = args[2];

        // Guard: ensure the input Visio file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Error: Input file \"{inputPath}\" does not exist.");
            return;
        }

        // Parse the shape ID (expected to be a numeric value)
        if (!long.TryParse(shapeIdText, out long shapeId))
        {
            Console.Error.WriteLine($"Error: Unable to parse shape ID \"{shapeIdText}\" as a number.");
            return;
        }

        try
        {
            // Load the Visio diagram
            Diagram diagram = new Diagram(inputPath);

            // Flag to indicate whether the shape was found
            bool shapeFound = false;

            // Iterate through all pages to locate the shape by its ID
            foreach (Page page in diagram.Pages)
            {
                // The GetShape method expects an int identifier; cast safely
                Shape shape = page.Shapes.GetShape((int)shapeId);
                if (shape != null)
                {
                    // Ensure the shape is not marked as deleted
                    if (shape.Del == BOOL.False)
                    {
                        // Lock the Y‑position (vertical movement) of the shape
                        shape.Protection.LockMoveY.Value = BOOL.True;
                        shapeFound = true;
                        Console.WriteLine($"Shape ID {shapeId} found on page \"{page.Name}\" and Y‑position locked.");
                    }
                    else
                    {
                        Console.Error.WriteLine($"Shape ID {shapeId} is marked as deleted and cannot be modified.");
                    }
                    break; // Shape IDs are unique across the document; exit loop once found
                }
            }

            if (!shapeFound)
            {
                Console.Error.WriteLine($"Error: Shape with ID {shapeId} was not found in the diagram.");
                return;
            }

            // Save the modified diagram to the specified output path in VSDX format
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Diagram saved successfully to \"{outputPath}\".");
        }
        catch (Exception ex)
        {
            // Capture any Aspose or I/O exceptions and report them
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}