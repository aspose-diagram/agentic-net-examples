using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
    {
        static void Main()
        {
            try
            {

                // Path to the Visio file
                string diagramPath = "input.vsdx";

                // Load the diagram
                Diagram diagram = new Diagram(diagramPath);

                // Path to the external data source (e.g., CSV with "ShapeId,Visible")
                string dataPath = "visibility.txt";

                // Read visibility settings into a dictionary
                var visibilityMap = new Dictionary<long, bool>();
                foreach (var line in File.ReadAllLines(dataPath))
                {
                    // Skip empty lines
                    if (string.IsNullOrWhiteSpace(line))
                        continue;

                    // Expected format: ShapeId,Visible (e.g., 123,true)
                    var parts = line.Split(',');
                    if (parts.Length != 2)
                        continue; // Invalid line, ignore

                    if (long.TryParse(parts[0].Trim(), out long shapeId) &&
                        bool.TryParse(parts[1].Trim(), out bool isVisible))
                    {
                        visibilityMap[shapeId] = isVisible;
                    }
                }

                // Apply visibility settings to shapes
                foreach (Page page in diagram.Pages)
                {
                    foreach (Shape shape in page.Shapes)
                    {
                        if (visibilityMap.TryGetValue(shape.ID, out bool isVisible))
                        {
                            // Shape.Del is of type BOOL. TRUE means hidden (deleted), FALSE means visible.
                            shape.Del = isVisible ? BOOL.False : BOOL.True;
                        }
                    }
                }

                // Save the modified diagram
                string outputPath = "output.vsdx";
                diagram.Save(outputPath, SaveFileFormat.Vsdx);

                Console.WriteLine("Shape visibility updated and diagram saved to " + outputPath);

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }