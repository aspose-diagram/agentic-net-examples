using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Validate arguments
        if (args.Length < 3)
        {
            Console.Error.WriteLine("Usage: <inputVisioPath> <shapeId> <outputVisioPath>");
            return;
        }

        string inputPath = args[0];
        string shapeIdArg = args[1];
        string outputPath = args[2];

        // Guard: check input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file does not exist: {inputPath}");
            return;
        }

        // Parse shape ID
        if (!long.TryParse(shapeIdArg, out long shapeId))
        {
            Console.Error.WriteLine($"Invalid shape ID: {shapeIdArg}");
            return;
        }

        try
        {
            // Load the Visio diagram
            Diagram diagram = new Diagram(inputPath);

            Shape targetShape = null;

            // Search for the shape with the specified ID across all pages
            foreach (Page page in diagram.Pages)
            {
                // Shapes.GetShape expects an int ID; cast safely
                int intId = (int)shapeId;
                try
                {
                    Shape shape = page.Shapes.GetShape(intId);
                    if (shape != null && shape.ID == shapeId)
                    {
                        targetShape = shape;
                        break;
                    }
                }
                catch
                {
                    // GetShape throws if not found; ignore and continue
                }
            }

            if (targetShape == null)
            {
                Console.Error.WriteLine($"Shape with ID {shapeId} not found.");
                return;
            }

            // Ensure the shape is not logically deleted
            if (targetShape.Del == BOOL.True)
            {
                Console.Error.WriteLine($"Shape with ID {shapeId} is marked as deleted.");
                return;
            }

            // Unlock the Height protection attribute
            // Access the protection cell and set it to FALSE
            targetShape.Protection.LockHeight.Value = BOOL.False;

            // Save the modified diagram
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Shape height protection unlocked and diagram saved to: {outputPath}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}