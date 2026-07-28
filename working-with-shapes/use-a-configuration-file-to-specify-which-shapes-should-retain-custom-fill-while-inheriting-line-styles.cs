using System;
using System.IO;
using System.Collections.Generic;
using System.Text.Json;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
    {
        static void Main()
        {
            try
            {

                // Paths for input diagram, configuration file and output diagram
                string diagramPath = "input.vsdx";
                string configPath = "shapeConfig.json";
                string outputPath = "output.vsdx";

                // Load the diagram
                Diagram diagram = new Diagram(diagramPath);

                // Read and parse the configuration file (expects JSON: {"retainCustomFill":[1,2,5]})
                List<long> targetShapeIds = LoadShapeIdsFromConfig(configPath);

                // Process each target shape
                foreach (long shapeId in targetShapeIds)
                {
                    // Attempt to locate the shape on any page
                    Shape shape = FindShapeById(diagram, shapeId);
                    if (shape == null)
                    {
                        Console.WriteLine($"Shape with ID {shapeId} not found.");
                        continue;
                    }

                    // Skip deleted shapes
                    if (shape.Del == BOOL.True)
                    {
                        Console.WriteLine($"Shape ID {shapeId} is marked as deleted. Skipping.");
                        continue;
                    }

                    // Inherit line style from the shape's inherited line values
                    shape.Line.LineColor.Value = shape.InheritLine.LineColor.Value;
                    shape.Line.LineWeight.Value = shape.InheritLine.LineWeight.Value;
                    shape.Line.LinePattern.Value = shape.InheritLine.LinePattern.Value;
                    shape.Line.BeginArrow.Value = shape.InheritLine.BeginArrow.Value;
                    shape.Line.EndArrow.Value = shape.InheritLine.EndArrow.Value;
                    shape.Line.BeginArrowSize.Value = shape.InheritLine.BeginArrowSize.Value;
                    shape.Line.EndArrowSize.Value = shape.InheritLine.EndArrowSize.Value;
                    shape.Line.LineCap.Value = shape.InheritLine.LineCap.Value;
                    shape.Line.Rounding.Value = shape.InheritLine.Rounding.Value;
                    shape.Line.LineColorTrans.Value = shape.InheritLine.LineColorTrans.Value;

                    Console.WriteLine($"Processed shape ID {shapeId}: custom fill retained, line style inherited.");
                }

                // Save the modified diagram
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
                Console.WriteLine($"Diagram saved to '{outputPath}'.");

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }

        // Loads a list of shape IDs from a JSON configuration file
        private static List<long> LoadShapeIdsFromConfig(string configFilePath)
        {
            var ids = new List<long>();

            if (!File.Exists(configFilePath))
            {
                Console.WriteLine($"Configuration file '{configFilePath}' not found.");
                return ids;
            }

            try
            {
                string json = File.ReadAllText(configFilePath);
                using JsonDocument doc = JsonDocument.Parse(json);
                if (doc.RootElement.TryGetProperty("retainCustomFill", out JsonElement array))
                {
                    foreach (JsonElement element in array.EnumerateArray())
                    {
                        if (element.TryGetInt64(out long id))
                        {
                            ids.Add(id);
                        }
                    }
                }
                else
                {
                    Console.WriteLine("Configuration does not contain 'retainCustomFill' array.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error reading configuration: {ex.Message}");
            }

            return ids;
        }

        // Searches all pages for a shape with the specified ID
        private static Shape FindShapeById(Diagram diagram, long shapeId)
        {
            foreach (Page page in diagram.Pages)
            {
                // Shapes.GetShape expects a long ID; it returns null if not found
                Shape shape = page.Shapes.GetShape(shapeId);
                if (shape != null)
                {
                    return shape;
                }
            }
            return null;
        }
    }