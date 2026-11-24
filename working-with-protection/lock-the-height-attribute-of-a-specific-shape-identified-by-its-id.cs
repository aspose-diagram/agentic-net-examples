using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Validate arguments: input file, shape ID, output file
        if (args.Length < 3)
        {
            Console.Error.WriteLine("Usage: <program> <inputDiagramPath> <shapeId> <outputDiagramPath>");
            return;
        }

        string inputPath = args[0];
        string shapeIdArg = args[1];
        string outputPath = args[2];

        // Guard: ensure input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Error: Input file not found: {inputPath}");
            return;
        }

        // Parse shape ID (expected to be a numeric value)
        if (!long.TryParse(shapeIdArg, out long shapeId))
        {
            Console.Error.WriteLine($"Error: Invalid shape ID '{shapeIdArg}'. Must be a numeric value.");
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
                // The GetShape method expects an int identifier
                Shape shape = page.Shapes.GetShape((int)shapeId);
                if (shape != null && shape.Del == BOOL.False)
                {
                    // Lock the Height attribute of the shape
                    shape.Protection.LockHeight.Value = BOOL.True;

                    shapeFound = true;
                    // No need to continue searching once the shape is found
                    break;
                }
            }

            if (!shapeFound)
            {
                Console.Error.WriteLine($"Error: Shape with ID {shapeId} not found or is deleted.");
                return;
            }

            // Save the modified diagram to the specified output path in VSDX format
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Shape height locked successfully. Diagram saved to: {outputPath}");
        }
        catch (Exception ex)
        {
            // Capture any Aspose.Diagram or I/O exceptions and report them
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}