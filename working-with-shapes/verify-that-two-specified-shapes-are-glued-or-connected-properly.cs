using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Expect three arguments: diagram file path, first shape ID, second shape ID
        if (args.Length != 3)
        {
            Console.Error.WriteLine("Usage: <program> <diagramPath> <shapeId1> <shapeId2>");
            return;
        }

        // Assign arguments to variables
        string diagramPath = args[0];
        string shapeId1Str = args[1];
        string shapeId2Str = args[2];

        // Guard: ensure the diagram file exists
        if (!File.Exists(diagramPath))
        {
            Console.Error.WriteLine($"File not found: {diagramPath}");
            return;
        }

        // Guard: parse shape IDs to long
        if (!long.TryParse(shapeId1Str, out long shapeId1))
        {
            Console.Error.WriteLine($"Invalid shape ID: {shapeId1Str}");
            return;
        }
        if (!long.TryParse(shapeId2Str, out long shapeId2))
        {
            Console.Error.WriteLine($"Invalid shape ID: {shapeId2Str}");
            return;
        }

        try
        {
            // Load the Visio diagram from the specified file
            Diagram diagram = new Diagram(diagramPath);

            // Use the first page (index 0) for shape lookup
            Page page = diagram.Pages[0];

            // Retrieve the two shapes by their IDs
            Shape shape1 = page.Shapes.GetShape(shapeId1);
            Shape shape2 = page.Shapes.GetShape(shapeId2);

            // Guard: ensure both shapes were found
            if (shape1 == null)
            {
                Console.Error.WriteLine($"Shape with ID {shapeId1} not found on page '{page.Name}'.");
                return;
            }
            if (shape2 == null)
            {
                Console.Error.WriteLine($"Shape with ID {shapeId2} not found on page '{page.Name}'.");
                return;
            }

            // Verify connection status using the built‑in API
            bool areConnected = shape1.IsConnected(shape2);
            // Verify glue status using the built‑in API
            bool areGlued = shape1.IsGlued(shape2);

            // Output the verification results
            Console.WriteLine($"Shape {shapeId1} (NameU: {shape1.NameU}) and Shape {shapeId2} (NameU: {shape2.NameU}) connection status:");
            Console.WriteLine($"  Connected: {(areConnected ? "Yes" : "No")}");
            Console.WriteLine($"  Glued    : {(areGlued ? "Yes" : "No")}");
        }
        catch (Exception ex)
        {
            // Write any Aspose or runtime errors to the error stream
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}