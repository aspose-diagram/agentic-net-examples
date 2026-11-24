using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Validate command‑line arguments: diagram path, page index, shape ID
        if (args.Length < 3)
        {
            Console.Error.WriteLine("Usage: Program <diagramPath> <pageIndex> <shapeId>");
            return;
        }

        string diagramPath = args[0];
        if (!File.Exists(diagramPath))
        {
            Console.Error.WriteLine($"File not found: {diagramPath}");
            return;
        }

        if (!int.TryParse(args[1], out int pageIndex))
        {
            Console.Error.WriteLine("Invalid page index.");
            return;
        }

        if (!long.TryParse(args[2], out long shapeId))
        {
            Console.Error.WriteLine("Invalid shape ID.");
            return;
        }

        try
        {
            // Load the Visio diagram
            Diagram diagram = new Diagram(diagramPath);

            // Ensure the requested page exists
            if (pageIndex < 0 || pageIndex >= diagram.Pages.Count)
            {
                Console.Error.WriteLine($"Page index {pageIndex} is out of range. Diagram contains {diagram.Pages.Count} pages.");
                return;
            }

            // Retrieve the specific page
            Page page = diagram.Pages[pageIndex];

            // Retrieve the shape by its unique ID from the page
            // Shapes.GetShape expects an integer ID; cast the long ID safely
            Shape targetShape = page.Shapes.GetShape((int)shapeId);

            if (targetShape == null)
            {
                Console.Error.WriteLine($"Shape with ID {shapeId} not found on page {pageIndex}.");
                return;
            }

            // Shape successfully retrieved; you can now work with 'targetShape'
            Console.WriteLine($"Shape retrieved: ID={targetShape.ID}, NameU='{targetShape.NameU}', Master='{targetShape.Master?.Name}'");
        }
        catch (Exception ex)
        {
            // Output any Aspose or runtime errors
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}