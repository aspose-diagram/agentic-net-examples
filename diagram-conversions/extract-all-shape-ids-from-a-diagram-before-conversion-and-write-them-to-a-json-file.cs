using System;
using System.IO;
using System.Collections.Generic;
using System.Text.Json;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Expect two arguments: input diagram path and output JSON path
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <inputDiagramPath> <outputJsonPath>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

        // Guard: verify input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file does not exist: {inputPath}");
            return;
        }

        try
        {
            // Load the Visio diagram
            Diagram diagram = new Diagram(inputPath);

            // Collect shape IDs from all pages
            List<long> shapeIds = new List<long>();

            // Iterate through each page in the diagram
            foreach (Page page in diagram.Pages)
            {
                // Iterate through each shape on the current page
                foreach (Shape shape in page.Shapes)
                {
                    // Skip deleted shapes
                    if (shape.Del == BOOL.True)
                        continue;

                    // Add the shape's ID to the list
                    shapeIds.Add(shape.ID);
                }
            }

            // Serialize the list of IDs to JSON
            string json = JsonSerializer.Serialize(shapeIds, new JsonSerializerOptions { WriteIndented = true });

            // Write JSON to the specified output file
            File.WriteAllText(outputPath, json);

            Console.WriteLine($"Extracted {shapeIds.Count} shape IDs to '{outputPath}'.");
            
            // Dispose the diagram to free resources
            diagram.Dispose();
        }
        catch (Exception ex)
        {
            // Log any errors that occur during processing
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}