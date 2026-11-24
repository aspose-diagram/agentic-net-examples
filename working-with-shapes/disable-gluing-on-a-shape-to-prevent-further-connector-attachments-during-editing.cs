using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Validate arguments: at least input and output file paths are required
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <inputVisioFile> <outputVisioFile> [shapeId]");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];
        long targetShapeId = 0; // 0 means "use the first shape on the first page"

        // Optional shape ID argument
        if (args.Length >= 3 && !long.TryParse(args[2], out targetShapeId))
        {
            Console.Error.WriteLine("Invalid shape ID provided.");
            return;
        }

        // Guard: ensure the input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file does not exist: {inputPath}");
            return;
        }

        try
        {
            // Load the Visio diagram inside a using block (required pattern)
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Ensure the diagram contains at least one page
                if (diagram.Pages.Count == 0)
                {
                    Console.Error.WriteLine("The diagram contains no pages.");
                    return;
                }

                // Work with the first page (index 0)
                Page page = diagram.Pages[0];

                // Retrieve the shape to modify
                Shape shape;
                if (targetShapeId != 0)
                {
                    // Get shape by the provided ID
                    shape = page.Shapes.GetShape(targetShapeId);
                    if (shape == null)
                    {
                        Console.Error.WriteLine($"Shape with ID {targetShapeId} not found on the first page.");
                        return;
                    }
                }
                else
                {
                    // Use the first shape in the collection as a fallback
                    if (page.Shapes.Count == 0)
                    {
                        Console.Error.WriteLine("The first page contains no shapes.");
                        return;
                    }
                    shape = page.Shapes[0];
                    targetShapeId = shape.ID;
                }

                // Disable dynamic gluing on the selected shape.
                // This prevents connectors from being glued to the shape during editing.
                shape.Misc.GlueType.Value = GlueTypeValue.NoAllowDynamicGlue;

                // Save the modified diagram to the specified output path
                diagram.Save(outputPath, SaveFileFormat.Vsdx);

                Console.WriteLine($"Successfully disabled gluing on shape ID {targetShapeId} and saved to {outputPath}.");
            }
        }
        catch (Exception ex)
        {
            // Write any errors to the error console
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}