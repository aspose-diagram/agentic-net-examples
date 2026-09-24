using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Validate argument count
        if (args.Length < 4)
        {
            Console.Error.WriteLine("Usage: <inputVisio> <outputVisio> <pageIndex> <shapeId1,shapeId2,...>");
            return;
        }

        // Input Visio file path
        string inputPath = args[0];
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Output Visio file path
        string outputPath = args[1];
        // No existence check needed for output file

        // Page index (zero‑based)
        if (!int.TryParse(args[2], out int pageIndex))
        {
            Console.Error.WriteLine($"Invalid page index: {args[2]}");
            return;
        }

        // Parse comma‑separated shape IDs into a long array
        string[] idTokens = args[3].Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
        long[] shapeIds = new long[idTokens.Length];
        for (int i = 0; i < idTokens.Length; i++)
        {
            if (!long.TryParse(idTokens[i], out shapeIds[i]))
            {
                Console.Error.WriteLine($"Invalid shape ID: {idTokens[i]}");
                return;
            }
        }

        try
        {
            // Load the Visio diagram from the input file
            Diagram diagram = new Diagram(inputPath);

            // Ensure the requested page index exists
            if (pageIndex < 0 || pageIndex >= diagram.Pages.Count)
            {
                Console.Error.WriteLine($"Page index out of range: {pageIndex}");
                return;
            }

            // Retrieve the target page
            Page page = diagram.Pages[pageIndex];

            // Collect Shape objects for the provided IDs
            Shape[] shapesToGroup = new Shape[shapeIds.Length];
            for (int i = 0; i < shapeIds.Length; i++)
            {
                // GetShape returns null if the ID is not present on the page
                Shape shp = page.Shapes.GetShape(shapeIds[i]);
                if (shp == null)
                {
                    Console.Error.WriteLine($"Shape ID not found on page {pageIndex}: {shapeIds[i]}");
                    return;
                }
                shapesToGroup[i] = shp;
            }

            // Group the selected shapes; the method returns the new group shape
            Shape groupShape = page.Shapes.Group(shapesToGroup);

            // Output the ID of the created group for verification
            Console.WriteLine($"Created group shape with ID: {groupShape.ID}");

            // Save the modified diagram to the output file in VSDX format
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
        }
        catch (Exception ex)
        {
            // Write any Aspose or runtime errors to the error stream
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}