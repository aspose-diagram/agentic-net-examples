using System;
using System.IO;
using System.Collections.Generic;
using System.Text.Json;
using Aspose.Diagram;

class Program
{
    // Helper class to store layer information
    private class LayerInfo
    {
        public int Index { get; set; }
        public string Name { get; set; }
    }

    static void Main(string[] args)
    {
        // Expect two arguments: input Visio file path and output JSON file path
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <inputVisioPath> <outputJsonPath>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

        // Guard against missing input file
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file does not exist: {inputPath}");
            return;
        }

        try
        {
            // Load the Visio diagram
            Diagram diagram = new Diagram(inputPath);

            // Dictionaries to hold layer counts and layer metadata
            Dictionary<int, int> layerShapeCounts = new Dictionary<int, int>();
            List<LayerInfo> layerInfos = new List<LayerInfo>();

            // Iterate through all pages in the diagram
            foreach (Page page in diagram.Pages)
            {
                // Collect layers defined on this page
                foreach (Layer layer in page.PageSheet.Layers)
                {
                    int ix = layer.IX;
                    if (!layerShapeCounts.ContainsKey(ix))
                    {
                        layerShapeCounts[ix] = 0;
                        layerInfos.Add(new LayerInfo
                        {
                            Index = ix,
                            Name = layer.Name.Value
                        });
                    }
                }

                // Iterate through all shapes on the page
                foreach (Shape shape in page.Shapes)
                {
                    // Retrieve the layer membership string (e.g., "1,2")
                    string member = shape.LayerMem.LayerMember.Value;
                    if (string.IsNullOrEmpty(member))
                        continue; // Shape does not belong to any layer

                    // Split the membership string and update counts
                    string[] parts = member.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                    foreach (string part in parts)
                    {
                        if (int.TryParse(part.Trim(), out int ix))
                        {
                            if (layerShapeCounts.ContainsKey(ix))
                            {
                                layerShapeCounts[ix]++;
                            }
                            else
                            {
                                // Layer index not previously recorded (possible cross-page reference)
                                layerShapeCounts[ix] = 1;
                                // Add placeholder name if unknown
                                layerInfos.Add(new LayerInfo
                                {
                                    Index = ix,
                                    Name = $"Layer_{ix}"
                                });
                            }
                        }
                    }
                }
            }

            // Prepare the result list for JSON serialization
            var result = new List<object>();
            foreach (LayerInfo info in layerInfos)
            {
                int count = layerShapeCounts.TryGetValue(info.Index, out int c) ? c : 0;
                result.Add(new
                {
                    Name = info.Name,
                    ShapeCount = count
                });
            }

            // Serialize to JSON with indentation
            string json = JsonSerializer.Serialize(result, new JsonSerializerOptions { WriteIndented = true });

            // Write JSON to the output file
            File.WriteAllText(outputPath, json);
        }
        catch (Exception ex)
        {
            // Write any errors to the error stream
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}