using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Diagram;

class Program
    {
        static void Main(string[] args)
        {
            // Expect two arguments: input Visio file path and output CSV file path
            if (args.Length < 2)
            {
                Console.WriteLine("Usage: VisioLayerExport <inputVisioFile> <outputCsvFile>");
                return;
            }

            string inputPath = args[0];
            string outputPath = args[1];

            // Load the Visio diagram
            Diagram diagram;
            try
            {
                diagram = new Diagram(inputPath);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to load diagram: {ex.Message}");
                return;
            }

            // Prepare to write CSV
            using (var writer = new StreamWriter(outputPath))
            {
                // Write CSV header
                writer.WriteLine("ShapeID,ShapeName,LayerName");

                // Iterate through all pages
                foreach (Page page in diagram.Pages)
                {
                    // Build a dictionary of layer index -> layer name for the current page
                    var layerIndexToName = new Dictionary<int, string>();
                    foreach (Layer layer in page.PageSheet.Layers)
                    {
                        // Layer.IX provides the zero‑based index of the layer
                        layerIndexToName[layer.IX] = layer.Name.Value;
                    }

                    // Iterate through all shapes on the page
                    foreach (Shape shape in page.Shapes)
                    {
                        // Skip shapes that are marked as deleted
                        if (shape.Del == BOOL.True)
                            continue;

                        long shapeId = shape.ID;
                        string shapeName = shape.NameU ?? string.Empty;

                        // Retrieve the layer membership string (semicolon‑separated indexes)
                        string layerMember = shape.LayerMem.LayerMember.Value;

                        if (string.IsNullOrWhiteSpace(layerMember))
                        {
                            // Shape is not assigned to any layer
                            writer.WriteLine($"{shapeId},{EscapeCsv(shapeName)},");
                        }
                        else
                        {
                            // Split the indexes and map to layer names
                            string[] parts = layerMember.Split(new[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
                            foreach (string part in parts)
                            {
                                if (int.TryParse(part, out int layerIdx) && layerIndexToName.TryGetValue(layerIdx, out string layerName))
                                {
                                    writer.WriteLine($"{shapeId},{EscapeCsv(shapeName)},{EscapeCsv(layerName)}");
                                }
                                else
                                {
                                    // Index could not be parsed or layer not found; write empty layer name
                                    writer.WriteLine($"{shapeId},{EscapeCsv(shapeName)},");
                                }
                            }
                        }
                    }
                }
            }

            Console.WriteLine($"CSV export completed: {outputPath}");
        }

        // Helper to escape CSV fields that may contain commas or quotes
        private static string EscapeCsv(string field)
        {
            if (field == null)
                return string.Empty;

            if (field.Contains("\""))
                field = field.Replace("\"", "\"\"");

            if (field.Contains(",") || field.Contains("\"") || field.Contains("\n") || field.Contains("\r"))
                return $"\"{field}\"";

            return field;
        }
    }