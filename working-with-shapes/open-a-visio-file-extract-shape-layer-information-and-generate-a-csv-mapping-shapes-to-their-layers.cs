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
                Console.WriteLine("Usage: VisioLayerExport <inputVisioPath> <outputCsvPath>");
                return;
            }

            string inputPath = args[0];
            string outputCsvPath = args[1];

            // Load the Visio diagram
            Diagram diagram = new Diagram(inputPath);

            using (StreamWriter writer = new StreamWriter(outputCsvPath))
            {
                // Write CSV header
                writer.WriteLine("ShapeID,ShapeName,LayerIndexes,LayerNames");

                // Iterate through all pages
                foreach (Page page in diagram.Pages)
                {
                    // Cache layer names for quick lookup by index
                    List<string> layerNames = new List<string>();
                    foreach (Layer layer in page.PageSheet.Layers)
                    {
                        layerNames.Add(layer.Name.Value);
                    }

                    // Iterate through all shapes on the page
                    foreach (Shape shape in page.Shapes)
                    {
                        long shapeId = shape.ID;
                        string shapeName = shape.Name ?? string.Empty;

                        // Retrieve layer membership string (e.g., "0;2")
                        string layerMember = shape.LayerMem?.LayerMember?.Value ?? string.Empty;
                        string layerIndexes = layerMember;
                        string layerNamesJoined = string.Empty;

                        if (!string.IsNullOrWhiteSpace(layerMember))
                        {
                            string[] indexTokens = layerMember.Split(new[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
                            List<string> names = new List<string>();

                            foreach (string token in indexTokens)
                            {
                                if (int.TryParse(token, out int idx) && idx >= 0 && idx < layerNames.Count)
                                {
                                    names.Add(layerNames[idx]);
                                }
                            }

                            layerNamesJoined = string.Join(";", names);
                        }

                        // Write CSV line
                        writer.WriteLine($"{shapeId},{EscapeCsv(shapeName)},{EscapeCsv(layerIndexes)},{EscapeCsv(layerNamesJoined)}");
                    }
                }
            }

            Console.WriteLine($"Layer mapping exported to: {outputCsvPath}");
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