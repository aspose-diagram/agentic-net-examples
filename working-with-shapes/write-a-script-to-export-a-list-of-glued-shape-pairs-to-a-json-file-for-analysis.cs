using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using Aspose.Diagram;

namespace GluedShapeExporter
{
    // DTO representing a pair of glued shapes
    public class GluedPair
    {
        public long ShapeId1 { get; set; }
        public long ShapeId2 { get; set; }
    }

    public class Program
    {
        public static void Main(string[] args)
        {
            try
            {

                // Input Visio file path (adjust as needed)
                string visioPath = "input.vsdx";

                // Output JSON file path
                string jsonOutputPath = "glued_pairs.json";

                // Load the diagram
                Diagram diagram = new Diagram(visioPath);

                // Collection to hold unique glued pairs
                List<GluedPair> gluedPairs = new List<GluedPair>();
                HashSet<string> seenPairs = new HashSet<string>();

                // Iterate through all pages
                foreach (Page page in diagram.Pages)
                {
                    // Iterate through all shapes on the page
                    foreach (Shape shape in page.Shapes)
                    {
                        // Skip deleted shapes
                        if (shape.Del == BOOL.True)
                            continue;

                        // Retrieve IDs of shapes glued to the current shape (1‑D connectors)
                        long[] gluedIds = shape.GluedShapes(GluedShapesFlags.GluedShapesAll1D, null, null);
                        if (gluedIds == null)
                            continue;

                        foreach (long targetId in gluedIds)
                        {
                            // Retrieve the target shape
                            Shape targetShape = page.Shapes.GetShape(targetId);
                            if (targetShape == null || targetShape.Del == BOOL.True)
                                continue;

                            long id1 = shape.ID;
                            long id2 = targetShape.ID;

                            // Ensure each pair is stored only once (order‑independent)
                            long minId = Math.Min(id1, id2);
                            long maxId = Math.Max(id1, id2);
                            string key = $"{minId}-{maxId}";

                            if (!seenPairs.Contains(key))
                            {
                                seenPairs.Add(key);
                                gluedPairs.Add(new GluedPair { ShapeId1 = minId, ShapeId2 = maxId });
                            }
                        }
                    }
                }

                // Serialize the list to JSON with indentation
                string json = JsonSerializer.Serialize(gluedPairs, new JsonSerializerOptions { WriteIndented = true });

                // Write JSON to file
                File.WriteAllText(jsonOutputPath, json);

                Console.WriteLine($"Exported {gluedPairs.Count} glued shape pairs to '{jsonOutputPath}'.");

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }
}