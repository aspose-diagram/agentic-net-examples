using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Expected arguments: inputDiagramPath outputDiagramPath pageName shapeId1,shapeId2,shapeId3,...
        if (args.Length < 4)
        {
            Console.Error.WriteLine("Usage: <inputDiagramPath> <outputDiagramPath> <pageName> <commaSeparatedShapeIds>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];
        string pageName = args[2];
        string shapeIdsArg = args[3];

        // Guard: verify input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file does not exist: {inputPath}");
            return;
        }

        // Parse shape IDs
        List<long> shapeIds = new List<long>();
        try
        {
            foreach (var part in shapeIdsArg.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                if (long.TryParse(part.Trim(), out long id))
                {
                    shapeIds.Add(id);
                }
                else
                {
                    Console.Error.WriteLine($"Invalid shape ID: '{part}'");
                    return;
                }
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error parsing shape IDs: {ex.Message}");
            return;
        }

        if (shapeIds.Count == 0)
        {
            Console.Error.WriteLine("No valid shape IDs provided.");
            return;
        }

        try
        {
            // Load the diagram
            Diagram diagram = new Diagram(inputPath);

            // Retrieve the specified page
            Page page = diagram.Pages.GetPage(pageName);
            if (page == null)
            {
                Console.Error.WriteLine($"Page not found: {pageName}");
                return;
            }

            // Collect Shape objects for the given IDs
            List<Shape> shapesToGroup = new List<Shape>();
            foreach (long id in shapeIds)
            {
                // Shape IDs are long, but GetShape expects int; cast safely
                Shape shape = page.Shapes.GetShape((int)id);
                if (shape == null)
                {
                    Console.Error.WriteLine($"Shape with ID {id} not found on page {pageName}.");
                    return;
                }
                shapesToGroup.Add(shape);
            }

            // Perform grouping
            Shape groupShape = page.Shapes.Group(shapesToGroup.ToArray());

            // Optional: assign a name to the new group
            groupShape.NameU = "GroupedShape";

            // Save the modified diagram
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Diagram saved successfully to {outputPath}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"An error occurred while processing the diagram: {ex.Message}");
        }
    }
}