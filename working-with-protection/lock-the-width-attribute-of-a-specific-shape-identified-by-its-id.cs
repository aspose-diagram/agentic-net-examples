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

        // Guard: check input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file does not exist: {inputPath}");
            return;
        }

        // Parse shape ID as long
        if (!long.TryParse(shapeIdArg, out long shapeId))
        {
            Console.Error.WriteLine($"Invalid shape ID: {shapeIdArg}");
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
                // Attempt direct retrieval if ID fits into int range
                if (shapeId >= int.MinValue && shapeId <= int.MaxValue)
                {
                    Shape shape = page.Shapes.GetShape((int)shapeId);
                    if (shape != null)
                    {
                        targetShape = shape;
                        break;
                    }
                }

                // Fallback: iterate all shapes on the page
                foreach (Shape shape in page.Shapes)
                {
                    if (shape.ID == shapeId)
                    {
                        targetShape = shape;
                        break;
                    }
                }

                if (targetShape != null)
                    break;
            }

            if (targetShape == null)
            {
                Console.Error.WriteLine($"Shape with ID {shapeId} not found in the diagram.");
                return;
            }

            // Lock the width attribute of the shape
            targetShape.Protection.LockWidth.Value = BOOL.True;

            // Save the modified diagram
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Shape width locked and diagram saved to: {outputPath}");
        }
        catch (Exception ex)
        {
            // Write any Aspose or runtime errors to the error stream
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}