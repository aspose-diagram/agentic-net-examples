using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Validate arguments: input file path, shape name, expected shape ID
        if (args.Length < 3)
        {
            Console.Error.WriteLine("Usage: Program <inputVisioFile> <shapeName> <expectedShapeId>");
            return;
        }

        string inputPath = args[0];
        string targetShapeName = args[1];
        string expectedIdStr = args[2];

        // Guard: check if the input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Error: File not found - {inputPath}");
            return;
        }

        // Parse expected shape ID
        if (!long.TryParse(expectedIdStr, out long expectedShapeId))
        {
            Console.Error.WriteLine($"Error: Invalid expected shape ID '{expectedIdStr}'.");
            return;
        }

        try
        {
            // Load the Visio diagram using a using block as required
            using (Diagram diagram = new Diagram(inputPath))
            {
                Shape foundShape = null;

                // Iterate through all pages and shapes to locate the shape by its NameU
                foreach (Page page in diagram.Pages)
                {
                    foreach (Shape shape in page.Shapes)
                    {
                        // Compare the shape's universal name with the target name
                        if (shape.NameU == targetShapeName)
                        {
                            foundShape = shape;
                            break;
                        }
                    }

                    if (foundShape != null)
                        break;
                }

                // If the shape was not found, report an error
                if (foundShape == null)
                {
                    Console.Error.WriteLine($"Error: Shape with name '{targetShapeName}' not found.");
                    return;
                }

                // Compare the actual shape ID with the expected ID
                if (foundShape.ID == expectedShapeId)
                {
                    Console.WriteLine($"Success: Shape '{targetShapeName}' has the expected ID {expectedShapeId}.");
                }
                else
                {
                    Console.WriteLine($"Mismatch: Shape '{targetShapeName}' has ID {foundShape.ID}, expected {expectedShapeId}.");
                }
            }
        }
        catch (Exception ex)
        {
            // Write any Aspose or runtime errors to the error stream
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}