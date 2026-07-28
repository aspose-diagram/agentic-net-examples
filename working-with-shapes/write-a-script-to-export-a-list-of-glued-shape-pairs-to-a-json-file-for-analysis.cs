using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using Aspose.Diagram;

namespace GluedShapesExport
{
    // Simple DTO to hold a pair of glued shape identifiers
    public class GluedPair
    {
        public long ShapeId { get; set; }
        public long GluedShapeId { get; set; }
    }

    class Program
    {
        static void Main(string[] args)
        {
            try
            {

                // Input Visio file path (adjust as needed)
                string visioFilePath = @"C:\Diagrams\sample.vsdx";

                // Output JSON file path
                string jsonOutputPath = @"C:\Diagrams\glued_shapes.json";

                // Load the Visio diagram using Aspose.Diagram (lifecycle rule)
                Diagram diagram = new Diagram(visioFilePath);

                // List to collect all glued shape pairs
                List<GluedPair> gluedPairs = new List<GluedPair>();

                // Iterate through each page in the diagram
                foreach (Page page in diagram.Pages)
                {
                    // Iterate through each shape on the page
                    foreach (Shape shape in page.Shapes)
                    {
                        // Retrieve IDs of shapes glued to the current shape.
                        // Using GluedShapesAll2D to capture all 2‑D glue relationships.
                        long[] gluedShapeIds = shape.GluedShapes(
                            GluedShapesFlags.GluedShapesAll2D,   // flag
                            null,                               // no category filter
                            null);                              // no additional shape filter

                        // Record each glue relationship as a pair (shape -> gluedShape)
                        foreach (long gluedId in gluedShapeIds)
                        {
                            gluedPairs.Add(new GluedPair
                            {
                                ShapeId = shape.ID,
                                GluedShapeId = gluedId
                            });
                        }
                    }
                }

                // Serialize the list of pairs to JSON with indentation for readability
                string json = JsonSerializer.Serialize(
                    gluedPairs,
                    new JsonSerializerOptions { WriteIndented = true });

                // Write JSON to the specified file (save rule)
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