using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Validate arguments: expect diagram file path and shape ID
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <diagramPath> <shapeId>");
            return;
        }

        string diagramPath = args[0];
        string shapeIdArg = args[1];

        // Guard: check if the diagram file exists
        if (!File.Exists(diagramPath))
        {
            Console.Error.WriteLine($"Error: Diagram file not found at '{diagramPath}'.");
            return;
        }

        // Parse shape ID (expected to be a numeric identifier)
        if (!long.TryParse(shapeIdArg, out long shapeId))
        {
            Console.Error.WriteLine($"Error: Invalid shape ID '{shapeIdArg}'. It must be a numeric value.");
            return;
        }

        try
        {
            // Load the Visio diagram
            Diagram diagram = new Diagram(diagramPath);

            // Retrieve the first page (or you can modify to select a specific page)
            if (diagram.Pages.Count == 0)
            {
                Console.Error.WriteLine("Error: The diagram contains no pages.");
                return;
            }

            Page page = diagram.Pages[0];

            // Retrieve the shape by its ID
            Shape shape = page.Shapes.GetShape(shapeId);
            if (shape == null)
            {
                Console.Error.WriteLine($"Error: No shape found with ID {shapeId} on the first page.");
                return;
            }

            // -----------------------------------------------------------------
            // NOTE: Aspose.Diagram does not provide a direct method to export a
            // single Shape to SWF format. The SWF export is only available at the
            // Diagram level via Diagram.Save(..., SaveFileFormat.Swf).
            // -----------------------------------------------------------------
            // TODO: Implement shape-to-SWF conversion if the API adds support in the future.
            // As a fallback, the whole diagram can be saved to SWF as shown below.

            string outputSwfPath = Path.Combine(Path.GetDirectoryName(diagramPath) ?? "", "output.swf");

            // Save the entire diagram to SWF format
            diagram.Save(outputSwfPath, SaveFileFormat.Swf);
            Console.WriteLine($"Diagram saved to SWF at '{outputSwfPath}'.");
        }
        catch (Exception ex)
        {
            // Handle any errors that occur during loading or saving
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}