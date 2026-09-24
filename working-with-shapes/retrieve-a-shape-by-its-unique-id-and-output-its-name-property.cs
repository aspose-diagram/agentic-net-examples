using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Validate command‑line arguments: expect file path and shape ID.
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: <program> <VisioFilePath> <ShapeID>");
            return;
        }

        // Assign input variables.
        string visioPath = args[0];
        string shapeIdArg = args[1];

        // Guard: ensure the Visio file exists.
        if (!File.Exists(visioPath))
        {
            Console.Error.WriteLine($"File not found: {visioPath}");
            return;
        }

        // Guard: parse shape ID to integer.
        if (!int.TryParse(shapeIdArg, out int shapeId))
        {
            Console.Error.WriteLine($"Invalid shape ID: {shapeIdArg}");
            return;
        }

        try
        {
            // Load the Visio diagram using Aspose.Diagram.
            Diagram diagram = new Diagram(visioPath);

            // Search for the shape with the specified unique ID across all pages.
            Shape targetShape = null;
            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    // Compare the shape's ID with the requested ID.
                    if (shape.ID == shapeId)
                    {
                        targetShape = shape;
                        break; // Shape found; exit inner loop.
                    }
                }
                if (targetShape != null)
                    break; // Shape found; exit outer loop.
            }

            // If shape not found, report and exit.
            if (targetShape == null)
            {
                Console.Error.WriteLine($"Shape with ID {shapeId} not found in the diagram.");
                return;
            }

            // Output the Name property of the located shape.
            Console.WriteLine($"Shape ID: {shapeId}, Name: {targetShape.Name}");
        }
        catch (Exception ex)
        {
            // Write any Aspose or I/O errors to the error stream.
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}