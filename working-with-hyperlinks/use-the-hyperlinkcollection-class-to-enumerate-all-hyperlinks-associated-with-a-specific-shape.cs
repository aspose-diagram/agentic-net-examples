using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Validate command‑line arguments: expect diagram file path and shape ID.
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
            Console.Error.WriteLine($"Error: File not found - {diagramPath}");
            return;
        }

        // Parse the shape ID (Visio shape IDs are long values).
        if (!long.TryParse(shapeIdArg, out long shapeId))
        {
            Console.Error.WriteLine($"Error: Invalid shape ID '{shapeIdArg}'.");
            return;
        }

        Diagram diagram;
        try
        {
            // Load the Visio diagram.
            diagram = new Diagram(diagramPath);
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error loading diagram: {ex.Message}");
            return;
        }

        // Locate the shape with the specified ID across all pages.
        Shape targetShape = null;
        foreach (Page page in diagram.Pages)
        {
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

        // Enumerate hyperlinks attached to the found shape.
        if (targetShape.Hyperlinks != null && targetShape.Hyperlinks.Count > 0)
        {
            Console.WriteLine($"Hyperlinks for shape ID {shapeId}:");
            foreach (Hyperlink link in targetShape.Hyperlinks)
            {
                // Output hyperlink details. Use null‑conditional operator for optional values.
                Console.WriteLine($"Name        : {link.Name}");
                Console.WriteLine($"Address     : {link.Address?.Value}");
                Console.WriteLine($"SubAddress  : {link.SubAddress?.Value}");
                Console.WriteLine($"Description : {link.Description?.Value}");
                Console.WriteLine($"NewWindow   : {link.NewWindow}");
                Console.WriteLine(new string('-', 40));
            }
        }
        else
        {
            Console.WriteLine($"No hyperlinks found for shape ID {shapeId}.");
        }
    }
}