using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Expect three arguments: input diagram path, shape ID, output diagram path.
        if (args.Length < 3)
        {
            Console.Error.WriteLine("Usage: <program> <inputVisioPath> <shapeId> <outputVisioPath>");
            return;
        }

        // Assign input parameters to variables.
        string inputPath = args[0];
        // Guard: ensure the input file exists.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        string shapeIdArg = args[1];
        // Guard: parse shape ID to long.
        if (!long.TryParse(shapeIdArg, out long shapeId))
        {
            Console.Error.WriteLine($"Invalid shape ID: {shapeIdArg}");
            return;
        }

        string outputPath = args[2];

        try
        {
            // Load the Visio diagram from the specified file.
            Diagram diagram = new Diagram(inputPath);

            // Locate the shape with the given ID across all pages.
            Shape targetShape = null;
            foreach (Page page in diagram.Pages)
            {
                // Iterate each shape on the current page.
                foreach (Shape shape in page.Shapes)
                {
                    // Compare the shape's ID with the requested ID.
                    if (shape.ID == shapeId)
                    {
                        targetShape = shape;
                        break;
                    }
                }
                if (targetShape != null) break;
            }

            // Guard: ensure the shape was found.
            if (targetShape == null)
            {
                Console.Error.WriteLine($"Shape with ID {shapeId} not found.");
                return;
            }

            // Unlock the rotation lock on the shape.
            // The rotation lock is accessed via the Protection collection.
            targetShape.Protection.LockRotate.Value = BOOL.False;

            // Save the modified diagram to the output path using VSDX format.
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Rotation attribute unlocked and diagram saved to: {outputPath}");
        }
        catch (Exception ex)
        {
            // Write any unexpected errors to the error stream.
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}