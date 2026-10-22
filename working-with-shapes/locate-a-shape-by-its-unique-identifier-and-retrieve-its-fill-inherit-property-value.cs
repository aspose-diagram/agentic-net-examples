using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Expect two arguments: diagram file path and shape ID
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <diagramPath> <shapeId>");
            return;
        }

        string diagramPath = args[0];
        string shapeIdArg = args[1];

        // Guard: ensure the diagram file exists
        if (!File.Exists(diagramPath))
        {
            Console.Error.WriteLine($"Error: File not found - {diagramPath}");
            return;
        }

        // Parse the shape ID (Visio shape IDs are long)
        if (!long.TryParse(shapeIdArg, out long shapeId))
        {
            Console.Error.WriteLine($"Error: Invalid shape ID '{shapeIdArg}'.");
            return;
        }

        try
        {
            // Load the Visio diagram inside a using block (required pattern)
            using (Diagram diagram = new Diagram(diagramPath))
            {
                Shape targetShape = null;

                // Search each page for the shape with the specified ID
                foreach (Page page in diagram.Pages)
                {
                    // GetShape returns null if the shape is not on this page
                    Shape shape = page.Shapes.GetShape(shapeId);
                    if (shape != null)
                    {
                        targetShape = shape;
                        break;
                    }
                }

                if (targetShape == null)
                {
                    Console.Error.WriteLine($"Shape with ID {shapeId} not found in the diagram.");
                    return;
                }

                // Retrieve inherited fill data.
                // The API provides an InheritFill property (not Fill.Inherit).
                // We'll output the inherited foreground fill color as an example.
                string inheritedForeColor = targetShape.InheritFill?.FillForegnd?.Value ?? "(none)";

                Console.WriteLine($"Shape ID: {shapeId}");
                Console.WriteLine($"Inherited Fill Foreground Color: {inheritedForeColor}");
            }
        }
        catch (Exception ex)
        {
            // Report any errors from Aspose or runtime
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}