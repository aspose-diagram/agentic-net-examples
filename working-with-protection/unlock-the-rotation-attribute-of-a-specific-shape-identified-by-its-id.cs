using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Validate arguments: input file, shape ID, optional output file
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <inputDiagramPath> <shapeId> [outputDiagramPath]");
            return;
        }

        string inputPath = args[0];
        string outputPath = args.Length > 2 ? args[2] : "output.vsdx";

        // Guard: ensure the input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Error: Input file not found: {inputPath}");
            return;
        }

        // Parse the shape ID (expected to be a long integer)
        if (!long.TryParse(args[1], out long shapeId))
        {
            Console.Error.WriteLine("Error: Shape ID must be a valid integer.");
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
                try
                {
                    // GetShape returns the shape with the given structural ID
                    Shape shape = page.Shapes.GetShape(shapeId);
                    if (shape != null)
                    {
                        targetShape = shape;
                        break;
                    }
                }
                catch
                {
                    // If GetShape throws (e.g., shape not on this page), continue searching
                }
            }

            if (targetShape == null)
            {
                Console.Error.WriteLine($"Error: Shape with ID {shapeId} not found in the diagram.");
                return;
            }

            // Unlock the rotation protection attribute
            // The rotation lock is controlled by the LockRotate cell in the Protection section
            targetShape.Protection.LockRotate.Value = BOOL.False;

            // Save the modified diagram
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Shape rotation unlocked and diagram saved to: {outputPath}");
        }
        catch (Exception ex)
        {
            // Report any Aspose.Diagram or I/O errors
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}