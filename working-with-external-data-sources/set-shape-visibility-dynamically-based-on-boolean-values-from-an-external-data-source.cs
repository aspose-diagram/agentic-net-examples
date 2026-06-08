using System;
using System.IO;
using System.Collections.Generic;
using System.Text.Json;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
    {
        static void Main(string[] args)
        {
            // Expect three arguments: input diagram path, JSON data path, output diagram path
            if (args.Length < 3)
            {
                Console.WriteLine("Usage: ShapeVisibilityDemo <input.vsdx> <visibility.json> <output.vsdx>");
                return;
            }

            string diagramPath = args[0];
            string jsonPath = args[1];
            string outputPath = args[2];

            // Load the Visio diagram
            Diagram diagram = new Diagram(diagramPath);

            // Read visibility data from JSON.
            // Expected format: { "123": true, "124": false, ... } where keys are shape IDs.
            string jsonContent = File.ReadAllText(jsonPath);
            Dictionary<string, bool> visibilityMap = JsonSerializer.Deserialize<Dictionary<string, bool>>(jsonContent);

            // Iterate through all pages and shapes, applying visibility based on the map
            foreach (Aspose.Diagram.Page page in diagram.Pages)
            {
                foreach (Aspose.Diagram.Shape shape in page.Shapes)
                {
                    // Use shape ID as string key
                    string shapeIdKey = shape.ID.ToString();

                    if (visibilityMap != null && visibilityMap.TryGetValue(shapeIdKey, out bool isVisible))
                    {
                        // Set the deletion flag: TRUE hides the shape, FALSE shows it
                        shape.Del = isVisible ? BOOL.False : BOOL.True;
                    }
                }
            }

            // Save the modified diagram
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
        }
    }