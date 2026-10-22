using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Validate command‑line arguments.
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <diagramPath> <shapeId>");
            return;
        }

        string diagramPath = args[0];
        string shapeIdArg = args[1];

        // Guard: ensure the diagram file exists.
        if (!File.Exists(diagramPath))
        {
            Console.Error.WriteLine($"Error: File not found -> {diagramPath}");
            return;
        }

        // Parse the shape ID (expected to be a long).
        if (!long.TryParse(shapeIdArg, out long shapeId))
        {
            Console.Error.WriteLine($"Error: Invalid shape ID '{shapeIdArg}'.");
            return;
        }

        try
        {
            // Load the Visio diagram inside a using block (mandatory pattern).
            using (Diagram diagram = new Diagram(diagramPath))
            {
                bool shapeFound = false;
                int pageIndex = 0; // Manual page counter because Page.Index does not exist.

                // Iterate through all pages to locate the shape by its ID.
                foreach (Page page in diagram.Pages)
                {
                    // Attempt to retrieve the shape using ShapeCollection.GetShape(long id).
                    Shape shape = null;
                    try
                    {
                        shape = page.Shapes.GetShape(shapeId);
                    }
                    catch
                    {
                        // GetShape may throw if the ID is not present on this page; ignore and continue.
                    }

                    if (shape != null)
                    {
                        shapeFound = true;
                        Console.WriteLine($"Shape found on page #{pageIndex}");
                        Console.WriteLine($"Shape ID   : {shape.ID}");
                        Console.WriteLine($"Shape NameU: {shape.NameU}");
                        // Additional shape details can be printed here if needed.
                        break;
                    }

                    pageIndex++;
                }

                if (!shapeFound)
                {
                    Console.WriteLine($"Shape with ID {shapeId} was not found in any page of the diagram.");
                }
            }
        }
        catch (Exception ex)
        {
            // Report any errors that occur during loading or processing.
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}