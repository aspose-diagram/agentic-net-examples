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
            Console.Error.WriteLine("Usage: <program> <diagramPath> <shapeName> <expectedShapeId>");
            return;
        }

        string diagramPath = args[0];
        string targetShapeName = args[1];
        string expectedIdText = args[2];

        // Guard: ensure the diagram file exists.
        if (!File.Exists(diagramPath))
        {
            Console.Error.WriteLine($"Error: Diagram file not found at '{diagramPath}'.");
            return;
        }

        // Guard: parse the expected shape identifier.
        if (!long.TryParse(expectedIdText, out long expectedShapeId))
        {
            Console.Error.WriteLine($"Error: Expected shape ID '{expectedIdText}' is not a valid integer.");
            return;
        }

        try
        {
            // Load the Visio diagram inside a using block as required.
            using (Diagram diagram = new Diagram(diagramPath))
            {
                bool shapeFound = false;

                // Iterate through all pages and shapes to locate the shape by its NameU.
                foreach (Page page in diagram.Pages)
                {
                    foreach (Shape shape in page.Shapes)
                    {
                        // Compare the shape's universal name with the target name.
                        if (shape.NameU == targetShapeName)
                        {
                            shapeFound = true;
                            long actualShapeId = shape.ID;

                            // Compare the actual ID with the expected identifier.
                            if (actualShapeId == expectedShapeId)
                            {
                                Console.WriteLine($"Shape '{targetShapeName}' ID matches expected value: {expectedShapeId}.");
                            }
                            else
                            {
                                Console.WriteLine($"Shape '{targetShapeName}' ID mismatch. Expected: {expectedShapeId}, Actual: {actualShapeId}.");
                            }

                            // Shape found; exit loops.
                            break;
                        }
                    }

                    if (shapeFound)
                        break;
                }

                if (!shapeFound)
                {
                    Console.WriteLine($"Shape with name '{targetShapeName}' was not found in the diagram.");
                }
            }
        }
        catch (Exception ex)
        {
            // Report any errors that occur during processing.
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}