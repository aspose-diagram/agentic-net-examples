using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using Aspose.Diagram;

class Program
    {
        static void Main(string[] args)
        {
            try
            {

                // Path to the source Visio file
                const string inputPath = "input.vsdx";

                // Load the Visio diagram (uses Aspose.Diagram's built‑in load functionality)
                Diagram diagram = new Diagram(inputPath);

                // Collection to hold unique glued shape pairs
                var gluedPairs = new List<GluedPair>();
                var seenPairs = new HashSet<string>(); // to avoid duplicate entries

                // Iterate through all pages and shapes
                foreach (Page page in diagram.Pages)
                {
                    foreach (Shape shape in page.Shapes)
                    {
                        // Retrieve IDs of all shapes glued to the current shape (both 1‑D and 2‑D)
                        long[] gluedShapeIds = shape.GluedShapes(
                            GluedShapesFlags.GluedShapesAll2D, // all relevant glued shapes
                            null,                             // no category filter
                            null);                            // no additional shape filter

                        foreach (long gluedId in gluedShapeIds)
                        {
                            // Build a deterministic key to prevent duplicate (A‑B and B‑A) entries
                            string key = shape.ID < gluedId
                                ? $"{shape.ID}-{gluedId}"
                                : $"{gluedId}-{shape.ID}";

                            if (!seenPairs.Contains(key))
                            {
                                seenPairs.Add(key);
                                gluedPairs.Add(new GluedPair
                                {
                                    ShapeId = shape.ID,
                                    GluedShapeId = gluedId
                                });
                            }
                        }
                    }
                }

                // Serialize the list of pairs to JSON with indentation for readability
                string json = JsonSerializer.Serialize(
                    gluedPairs,
                    new JsonSerializerOptions { WriteIndented = true });

                // Write the JSON to a file (uses standard .NET file I/O)
                const string outputPath = "glued_shapes.json";
                File.WriteAllText(outputPath, json);

                Console.WriteLine($"Exported {gluedPairs.Count} glued shape pairs to '{outputPath}'.");

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }

        // Simple DTO for JSON serialization
        private class GluedPair
        {
            public long ShapeId { get; set; }
            public long GluedShapeId { get; set; }
        }
    }