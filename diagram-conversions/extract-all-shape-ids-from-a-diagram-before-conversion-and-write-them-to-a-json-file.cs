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

                // Input Visio file path (first argument) or default.
                string inputPath = args.Length > 0 ? args[0] : "input.vsdx";

                // Output JSON file path (second argument) or default.
                string outputPath = args.Length > 1 ? args[1] : "shapeIds.json";

                // Load the diagram.
                Diagram diagram = new Diagram(inputPath);

                // Collect all shape IDs.
                List<long> shapeIds = new List<long>();
                foreach (Page page in diagram.Pages)
                {
                    foreach (Shape shape in page.Shapes)
                    {
                        shapeIds.Add(shape.ID);
                    }
                }

                // Serialize IDs to JSON with indentation.
                string json = JsonSerializer.Serialize(shapeIds, new JsonSerializerOptions { WriteIndented = true });

                // Write JSON to file.
                File.WriteAllText(outputPath, json);

                Console.WriteLine($"Extracted {shapeIds.Count} shape IDs to \"{outputPath}\".");

            }
            catch (Aspose.Diagram.DiagramException ex)
            {
                Console.Error.WriteLine($"[DiagramException] {ex.Message}");
            }
    }
    }