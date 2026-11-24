using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Validate command‑line arguments
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

        // Parse the shape identifier (must be a long)
        if (!long.TryParse(shapeIdArg, out long shapeId))
        {
            Console.Error.WriteLine($"Error: Invalid shape ID '{shapeIdArg}'. Must be a numeric value.");
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
                    // GetShape expects a long identifier
                    Shape shape = page.Shapes.GetShape(shapeId);
                    if (shape != null && shape.ID == shapeId)
                    {
                        targetShape = shape;
                        break;
                    }
                }

                if (targetShape == null)
                {
                    Console.Error.WriteLine($"Error: Shape with ID {shapeId} not found in the diagram.");
                    return;
                }

                // Retrieve the inherited fill data (shape.InheritFill)
                Fill inheritFill = targetShape.InheritFill;

                // Output some representative inherited fill properties
                Console.WriteLine($"Shape ID: {shapeId}");
                Console.WriteLine($"Inherited Fill Foreground Color: {inheritFill.FillForegnd.Value}");
                Console.WriteLine($"Inherited Fill Background Color: {inheritFill.FillBkgnd.Value}");
                Console.WriteLine($"Inherited Fill Pattern: {inheritFill.FillPattern.Value}");
            }
        }
        catch (Exception ex)
        {
            // Report any errors that occur during processing
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}