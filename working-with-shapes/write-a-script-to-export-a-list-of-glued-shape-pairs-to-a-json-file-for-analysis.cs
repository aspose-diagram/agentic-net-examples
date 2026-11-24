using System;
using System.IO;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text.Json;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Expect two arguments: input Visio file path and output JSON file path
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <inputVisioPath> <outputJsonPath>");
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

            // Collection to hold unique glued shape pairs
            List<(long, long)> gluedPairs = new List<(long, long)>();
            HashSet<string> seenPairs = new HashSet<string>();

            // Iterate through all pages and shapes
            foreach (Aspose.Diagram.Page page in diagram.Pages)
            {
                foreach (Aspose.Diagram.Shape shape in page.Shapes)
                {
                    // Retrieve IDs of shapes glued to the current shape (1D connectors)
                    long[] gluedIds = shape.GluedShapes(GluedShapesFlags.GluedShapesAll1D, null, null);
                    if (gluedIds == null) continue;

                    foreach (long gluedId in gluedIds)
                    {
                        // Create a deterministic key to avoid duplicate pairs
                        long first = Math.Min(shape.ID, gluedId);
                        long second = Math.Max(shape.ID, gluedId);
                        string key = $"{first}-{second}";

                        if (!seenPairs.Contains(key))
                        {
                            seenPairs.Add(key);
                            gluedPairs.Add((first, second));
                        }
                    }
                }
            }

            // Prepare a serializable list of pairs
            var serializableList = gluedPairs
                .Select(p => new GluedPair { Shape1 = p.Item1, Shape2 = p.Item2 })
                .ToList();

            // Serialize to JSON with indentation
            string json = JsonSerializer.Serialize(serializableList, new JsonSerializerOptions { WriteIndented = true });

            // Write JSON to the output file
            File.WriteAllText(outputPath, json);

            Console.WriteLine($"Exported {serializableList.Count} glued shape pairs to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            // Capture any errors from Aspose.Diagram operations or file I/O
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }

    // Helper class for JSON serialization
    public class GluedPair
    {
        public long Shape1 { get; set; }
        public long Shape2 { get; set; }
    }
}