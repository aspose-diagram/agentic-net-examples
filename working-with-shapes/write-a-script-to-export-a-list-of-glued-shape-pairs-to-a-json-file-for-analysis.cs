using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using Aspose.Diagram;

namespace GluedShapesExport
{
    // Simple DTO to hold a glued shape pair
    public class GluedPair
    {
        public long FromShapeId { get; set; }
        public string FromShapeName { get; set; }
        public long ToShapeId { get; set; }
        public string ToShapeName { get; set; }
    }

    class Program
    {
        static void Main(string[] args)
        {
            try
            {

                // Path to the source Visio file
                string inputPath = "input.vsdx";

                // Load the diagram using Aspose.Diagram
                Diagram diagram = new Diagram(inputPath);

                // Collection to store unique glued pairs
                var pairs = new List<GluedPair>();
                var seenPairs = new HashSet<string>(); // key format: "minId_maxId"

                // Iterate through all pages and shapes
                foreach (Page page in diagram.Pages)
                {
                    foreach (Shape shape in page.Shapes)
                    {
                        // Retrieve IDs of all shapes glued to the current shape (both 1‑D and 2‑D)
                        long[] gluedIds = shape.GluedShapes(
                            GluedShapesFlags.GluedShapesAll2D,   // return all glued shapes
                            null,                               // no category filter
                            null);                              // no additional shape filter

                        if (gluedIds == null) continue;

                        foreach (long gluedId in gluedIds)
                        {
                            // Avoid self‑gluing
                            if (gluedId == shape.ID) continue;

                            // Create a deterministic key to prevent duplicate entries (A‑B and B‑A)
                            long minId = Math.Min(shape.ID, gluedId);
                            long maxId = Math.Max(shape.ID, gluedId);
                            string pairKey = $"{minId}_{maxId}";

                            if (seenPairs.Contains(pairKey)) continue;
                            seenPairs.Add(pairKey);

                            // Find the glued shape object (search across all pages)
                            Shape gluedShape = FindShapeById(diagram, gluedId);
                            if (gluedShape == null) continue; // safety check

                            // Add the pair to the result list
                            pairs.Add(new GluedPair
                            {
                                FromShapeId = shape.ID,
                                FromShapeName = shape.Name,
                                ToShapeId = gluedShape.ID,
                                ToShapeName = gluedShape.Name
                            });
                        }
                    }
                }

                // Serialize the list to JSON with indentation for readability
                string json = JsonSerializer.Serialize(pairs, new JsonSerializerOptions { WriteIndented = true });

                // Write JSON to file
                string outputPath = "glued_pairs.json";
                File.WriteAllText(outputPath, json);

                Console.WriteLine($"Exported {pairs.Count} glued shape pairs to '{outputPath}'.");

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }

        // Helper method to locate a shape by its ID across all pages
        private static Shape FindShapeById(Diagram diagram, long id)
        {
            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    if (shape.ID == id)
                        return shape;
                }
            }
            return null;
        }
    }
}