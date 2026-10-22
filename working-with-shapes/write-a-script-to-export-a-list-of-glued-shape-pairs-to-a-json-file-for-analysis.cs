using System;
using System.IO;
using System.Collections.Generic;
using System.Text.Json;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Validate command‑line arguments.
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <inputVisioFile> <outputJsonFile>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

        // Guard against missing input file.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the Visio diagram.
            Diagram diagram = new Diagram(inputPath);

            // Collect all shapes from all pages.
            var allShapes = new List<(Shape shape, long id, string name)>();
            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    // Store shape reference together with its ID and Name for later use.
                    allShapes.Add((shape, shape.ID, shape.Name));
                }
            }

            // Find all pairs of shapes that are glued together.
            var gluedPairs = new List<GluedPair>();
            for (int i = 0; i < allShapes.Count; i++)
            {
                for (int j = i + 1; j < allShapes.Count; j++)
                {
                    Shape shapeA = allShapes[i].shape;
                    Shape shapeB = allShapes[j].shape;

                    // Use the IsGlued method to determine if the two shapes are glued.
                    bool areGlued = shapeA.IsGlued(shapeB);
                    if (areGlued)
                    {
                        gluedPairs.Add(new GluedPair
                        {
                            Shape1Id = allShapes[i].id,
                            Shape1Name = allShapes[i].name,
                            Shape2Id = allShapes[j].id,
                            Shape2Name = allShapes[j].name
                        });
                    }
                }
            }

            // Serialize the list of glued pairs to JSON.
            var jsonOptions = new JsonSerializerOptions { WriteIndented = true };
            string json = JsonSerializer.Serialize(gluedPairs, jsonOptions);

            // Write JSON to the specified output file.
            File.WriteAllText(outputPath, json);
            Console.WriteLine($"Exported {gluedPairs.Count} glued shape pairs to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            // Report any errors that occur during processing.
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }

    // Simple DTO for JSON serialization of a glued shape pair.
    private class GluedPair
    {
        public long Shape1Id { get; set; }
        public string Shape1Name { get; set; }
        public long Shape2Id { get; set; }
        public string Shape2Name { get; set; }
    }
}