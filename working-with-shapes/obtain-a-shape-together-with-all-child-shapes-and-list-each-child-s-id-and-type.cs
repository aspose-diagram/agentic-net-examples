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
            Console.Error.WriteLine("Usage: Program <diagramPath> <shapeName>");
            return;
        }

        string diagramPath = args[0];
        string shapeName = args[1];

        // Guard: ensure the input Visio file exists.
        if (!File.Exists(diagramPath))
        {
            Console.Error.WriteLine($"File not found: {diagramPath}");
            return;
        }

        try
        {
            // Load the diagram inside a using block as required by the API.
            using (Diagram diagram = new Diagram(diagramPath))
            {
                Shape targetShape = null;

                // Search each page for the shape (including nested group children) by name.
                foreach (Page page in diagram.Pages)
                {
                    // GetShapeIncludingChild searches within the page's shape collection,
                    // returning the shape itself or a group child that matches the name.
                    Shape found = page.Shapes.GetShapeIncludingChild(shapeName);
                    if (found != null)
                    {
                        targetShape = found;
                        break;
                    }
                }

                if (targetShape == null)
                {
                    Console.Error.WriteLine($"Shape with name '{shapeName}' not found.");
                    return;
                }

                // Output basic information about the found shape.
                Console.WriteLine($"Target Shape ID: {targetShape.ID}, NameU: {targetShape.NameU}");

                // -----------------------------------------------------------------
                // TODO: Enumerate child shapes of the target shape.
                // The Aspose.Diagram API provides methods to access child shapes
                // of a group shape (e.g., via a ChildShapes collection or similar).
                // Insert the appropriate code here to iterate over each child,
                // writing its ID and type to the console, e.g.:
                //   foreach (Shape child in targetShape.ChildShapes) { ... }
                // -----------------------------------------------------------------
                Console.WriteLine("Child shape enumeration not implemented – add appropriate API calls here.");
            }
        }
        catch (Exception ex)
        {
            // Write any exception details to the error stream.
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}