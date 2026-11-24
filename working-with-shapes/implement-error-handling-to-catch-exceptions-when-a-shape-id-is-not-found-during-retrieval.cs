using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Determine the diagram file path (first argument or default)
        string diagramPath = args.Length > 0 ? args[0] : "input.vsdx";

        // Guard: ensure the diagram file exists
        if (!File.Exists(diagramPath))
        {
            Console.Error.WriteLine($"Error: Diagram file not found at '{diagramPath}'.");
            return;
        }

        // Load the diagram inside a try/catch to handle loading errors
        Diagram diagram;
        try
        {
            diagram = new Diagram(diagramPath);
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error loading diagram: {ex.Message}");
            return;
        }

        // Determine the shape ID to retrieve (second argument or example ID)
        long shapeId = args.Length > 1 && long.TryParse(args[1], out long parsedId) ? parsedId : 15L;

        // Attempt to retrieve the shape by ID with error handling
        try
        {
            // Use the first page for simplicity; adjust index as needed
            Page page = diagram.Pages[0];

            // Retrieve the shape; this may throw if the ID does not exist
            Shape shape = page.Shapes.GetShape(shapeId);

            // If retrieval succeeds, output basic information
            Console.WriteLine($"Shape found. ID: {shape.ID}, NameU: {shape.NameU}");
        }
        catch (Exception ex)
        {
            // Handle the case where the shape ID is not found or other errors
            Console.Error.WriteLine($"Error: Shape with ID {shapeId} could not be retrieved. Details: {ex.Message}");
        }

        // Optional: save the diagram unchanged to demonstrate a successful operation
        try
        {
            string outputPath = Path.Combine(Path.GetDirectoryName(diagramPath) ?? "", "output.vsdx");
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Diagram saved successfully to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error saving diagram: {ex.Message}");
        }
    }
}