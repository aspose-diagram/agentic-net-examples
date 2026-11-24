using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Validate arguments: input file, page name, comma‑separated shape IDs
        if (args.Length < 3)
        {
            Console.Error.WriteLine("Usage: <program> <inputDiagramPath> <pageName> <shapeId1,shapeId2,...>");
            return;
        }

        string inputPath = args[0];
        string pageName = args[1];
        string shapeIdsArg = args[2];

        // Guard: ensure the input Visio file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Error: Input file not found: {inputPath}");
            return;
        }

        // Parse shape IDs
        string[] idTokens = shapeIdsArg.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
        long[] shapeIds = new long[idTokens.Length];
        for (int i = 0; i < idTokens.Length; i++)
        {
            if (!long.TryParse(idTokens[i].Trim(), out shapeIds[i]))
            {
                Console.Error.WriteLine($"Error: Invalid shape ID '{idTokens[i]}'");
                return;
            }
        }

        try
        {
            // Load the diagram
            Diagram diagram = new Diagram(inputPath);

            // Retrieve the requested page
            Page page = diagram.Pages.GetPage(pageName);
            if (page == null)
            {
                Console.Error.WriteLine($"Error: Page '{pageName}' not found in the diagram.");
                return;
            }

            // Collect Shape objects for the provided IDs
            Shape[] shapesToGroup = new Shape[shapeIds.Length];
            for (int i = 0; i < shapeIds.Length; i++)
            {
                Shape shape = page.Shapes.GetShape(shapeIds[i]);
                if (shape == null)
                {
                    Console.Error.WriteLine($"Error: Shape with ID {shapeIds[i]} not found on page '{pageName}'.");
                    return;
                }
                shapesToGroup[i] = shape;
            }

            // Group the selected shapes; the method returns the new group shape
            Shape groupShape = page.Shapes.Group(shapesToGroup);
            Console.WriteLine($"Grouped {shapesToGroup.Length} shapes into a new group with ID {groupShape.ID}.");

            // Prepare output path (same directory, suffix "_grouped.vsdx")
            string dir = Path.GetDirectoryName(inputPath);
            string fileNameWithoutExt = Path.GetFileNameWithoutExtension(inputPath);
            string outputPath = Path.Combine(dir, $"{fileNameWithoutExt}_grouped.vsdx");

            // Save the modified diagram
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Diagram saved successfully to: {outputPath}");
        }
        catch (Exception ex)
        {
            // Write any Aspose or runtime errors to the error stream
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}