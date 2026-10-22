using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Validate input arguments
        if (args.Length == 0)
        {
            Console.Error.WriteLine("Usage: Program <diagramPath> [shapeId]");
            return;
        }

        string diagramPath = args[0];
        if (!File.Exists(diagramPath))
        {
            Console.Error.WriteLine($"File not found: {diagramPath}");
            return;
        }

        // Optional shape ID (default to 1)
        long shapeId = 1;
        if (args.Length > 1 && long.TryParse(args[1], out long parsedId))
        {
            shapeId = parsedId;
        }

        try
        {
            // Load the Visio diagram inside a using block (mandatory pattern)
            using (Diagram diagram = new Diagram(diagramPath))
            {
                // Ensure the diagram contains at least one page
                if (diagram.Pages.Count == 0)
                {
                    Console.Error.WriteLine("The diagram contains no pages.");
                    return;
                }

                // Use the first page for analysis
                Page page = diagram.Pages[0];

                // Retrieve the shape by its ID
                Shape shape = page.Shapes.GetShape(shapeId);
                if (shape == null)
                {
                    Console.Error.WriteLine($"Shape with ID {shapeId} not found on page 0.");
                    return;
                }

                // Skip logically deleted shapes
                if (shape.Del == BOOL.True)
                {
                    Console.Error.WriteLine($"Shape with ID {shapeId} is marked as deleted.");
                    return;
                }

                // Compute bounding box using the shape's XForm dimensions
                double width = shape.XForm.Width.Value;
                double height = shape.XForm.Height.Value;

                // Log the results
                Console.WriteLine($"Bounding box for shape ID {shapeId}:");
                Console.WriteLine($"Width : {width} inches");
                Console.WriteLine($"Height: {height} inches");
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}
