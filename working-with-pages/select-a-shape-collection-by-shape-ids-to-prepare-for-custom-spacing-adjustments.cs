using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Validate arguments: input file, output file, and at least one shape ID
        if (args.Length < 3)
        {
            Console.Error.WriteLine("Usage: <program> <inputDiagramPath> <outputDiagramPath> <shapeId1> [<shapeId2> ...]");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

        // Guard: ensure the input diagram file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Parse shape IDs from remaining arguments
        List<long> shapeIds = new List<long>();
        for (int i = 2; i < args.Length; i++)
        {
            if (long.TryParse(args[i], out long id))
            {
                shapeIds.Add(id);
            }
            else
            {
                Console.Error.WriteLine($"Invalid shape ID: {args[i]}");
                return;
            }
        }

        try
        {
            // Load the Visio diagram
            Diagram diagram = new Diagram(inputPath);

            // Use the first page for demonstration (could be changed to a specific page)
            if (diagram.Pages.Count == 0)
            {
                Console.Error.WriteLine("The diagram contains no pages.");
                return;
            }

            Page page = diagram.Pages[0];

            // Collect the shapes matching the provided IDs, ignoring deleted shapes
            List<Shape> selectedShapes = new List<Shape>();
            foreach (long shapeId in shapeIds)
            {
                // Retrieve shape by ID
                Shape shape = page.Shapes.GetShape(shapeId);
                if (shape == null)
                {
                    Console.Error.WriteLine($"Shape with ID {shapeId} not found on page '{page.Name}'.");
                    continue;
                }

                // Skip logically deleted shapes
                if (shape.Del == BOOL.True)
                {
                    Console.Error.WriteLine($"Shape with ID {shapeId} is marked as deleted; skipping.");
                    continue;
                }

                selectedShapes.Add(shape);
            }

            if (selectedShapes.Count == 0)
            {
                Console.Error.WriteLine("No valid shapes were selected for spacing adjustments.");
                return;
            }

            // Example custom spacing adjustment: set line spacing for each paragraph in each selected shape
            foreach (Shape shape in selectedShapes)
            {
                // Iterate through paragraphs using indexer (no LINQ)
                for (int i = 0; i < shape.Paras.Count; i++)
                {
                    // Set line spacing to 1.5 lines (example value)
                    shape.Paras[i].SpLine.Value = 1.5;
                }
            }

            // Save the modified diagram to the specified output path
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Diagram saved successfully to: {outputPath}");
        }
        catch (Exception ex)
        {
            // Capture any Aspose.Diagram related errors
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}