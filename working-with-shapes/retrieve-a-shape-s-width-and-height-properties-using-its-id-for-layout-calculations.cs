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
            Console.Error.WriteLine($"Error: Diagram file not found at '{diagramPath}'.");
            return;
        }

        // Parse the shape ID (Visio shape IDs are long integers).
        if (!long.TryParse(shapeIdArg, out long shapeId))
        {
            Console.Error.WriteLine($"Error: Invalid shape ID '{shapeIdArg}'.");
            return;
        }

        // Load the diagram inside a using block as required.
        using (Diagram diagram = new Diagram(diagramPath))
        {
            try
            {
                // Retrieve the first page (index 0). Adjust if needed for multi‑page diagrams.
                Page page = diagram.Pages[0];

                // Retrieve the shape by its ID.
                Shape shape = page.Shapes.GetShape(shapeId);
                if (shape == null)
                {
                    Console.Error.WriteLine($"Error: No shape with ID {shapeId} found on page 0.");
                    return;
                }

                // Access width and height via the XForm object (values are in inches).
                double width = shape.XForm.Width.Value;
                double height = shape.XForm.Height.Value;

                // Output the dimensions.
                Console.WriteLine($"Shape ID {shapeId}: Width = {width} inches, Height = {height} inches");

                // Example layout calculation: compute the area.
                double area = width * height;
                Console.WriteLine($"Calculated area = {area} square inches");
            }
            catch (Exception ex)
            {
                // Report any Aspose.Diagram related errors.
                Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
            }
        }
    }
}