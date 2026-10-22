using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Expect three arguments: input diagram path, shape ID, Y offset value
        if (args.Length < 3)
        {
            Console.Error.WriteLine("Usage: <program> <inputDiagramPath> <shapeId> <yOffset>");
            return;
        }

        string inputPath = args[0];
        string shapeIdArg = args[1];
        string offsetArg = args[2];

        // Guard: verify input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Error: File not found - {inputPath}");
            return;
        }

        // Parse shape ID
        if (!long.TryParse(shapeIdArg, out long shapeId))
        {
            Console.Error.WriteLine($"Error: Invalid shape ID '{shapeIdArg}'.");
            return;
        }

        // Parse Y offset
        if (!double.TryParse(offsetArg, out double yOffset))
        {
            Console.Error.WriteLine($"Error: Invalid Y offset '{offsetArg}'.");
            return;
        }

        try
        {
            // Load the diagram
            Diagram diagram = new Diagram(inputPath);

            Shape targetShape = null;

            // Search for the shape by ID across all pages
            foreach (Page page in diagram.Pages)
            {
                // GetShape expects an int ID; cast safely
                if (shapeId <= int.MaxValue && shapeId >= int.MinValue)
                {
                    Shape shape = page.Shapes.GetShape((int)shapeId);
                    if (shape != null && shape.Del == BOOL.False)
                    {
                        targetShape = shape;
                        break;
                    }
                }
            }

            if (targetShape == null)
            {
                Console.Error.WriteLine($"Error: Shape with ID {shapeId} not found.");
                return;
            }

            // Adjust the Y coordinate (PinY) by adding the offset
            targetShape.XForm.PinY.Value += yOffset;

            // Prepare output path
            string outputPath = Path.Combine(Path.GetDirectoryName(inputPath) ?? "", "output.vsdx");

            // Save the modified diagram
            diagram.Save(outputPath, SaveFileFormat.Vsdx);

            Console.WriteLine($"Shape ID {shapeId} Y coordinate adjusted by {yOffset}. Saved to {outputPath}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}