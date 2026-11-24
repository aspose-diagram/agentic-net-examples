using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Validate command‑line arguments: first argument = diagram file path, second argument = shape ID
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

        // Parse the shape ID (Visio shape IDs are long integers)
        if (!long.TryParse(shapeIdArg, out long shapeId))
        {
            Console.Error.WriteLine($"Error: Invalid shape ID '{shapeIdArg}'. It must be a numeric value.");
            return;
        }

        try
        {
            // Load the Visio diagram inside a using block as required by the API guidelines
            using (Diagram diagram = new Diagram(diagramPath))
            {
                // Ensure the diagram contains at least one page
                if (diagram.Pages.Count == 0)
                {
                    Console.Error.WriteLine("Error: The diagram does not contain any pages.");
                    return;
                }

                // Access the first page (index 0)
                Page page = diagram.Pages[0];

                // Retrieve the shape by its ID using the supported GetShape method
                Shape shape = page.Shapes.GetShape(shapeId);

                // Output basic information about the retrieved shape
                Console.WriteLine($"Shape ID   : {shape.ID}");
                Console.WriteLine($"Shape Name : {shape.Name}");
                Console.WriteLine($"Master Name: {shape.Master?.Name ?? "(none)"}");

                // Example of checking the deletion flag using BOOL enumeration
                if (shape.Del == BOOL.True)
                {
                    Console.WriteLine("Note: This shape is marked as deleted in the diagram.");
                }
                else
                {
                    Console.WriteLine("The shape is active.");
                }
            }
        }
        catch (Exception ex)
        {
            // Capture any Aspose.Diagram or I/O errors and report them
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}