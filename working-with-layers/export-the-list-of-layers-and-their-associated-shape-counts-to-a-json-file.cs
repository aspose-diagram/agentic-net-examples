using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using Aspose.Diagram;

namespace LayerExportExample
{
    // DTO for JSON output
    public class LayerInfo
    {
        public string Name { get; set; } = string.Empty;
        public int ShapeCount { get; set; }
    }

    public class Program
    {
        public static void Main(string[] args)
        {
            try
            {

                // Input Visio file path (default if not provided)
                string inputPath = args.Length > 0 ? args[0] : "input.vsdx";
                // Output JSON file path (default if not provided)
                string outputPath = args.Length > 1 ? args[1] : "layers.json";

                // Load the diagram
                Diagram diagram = new Diagram(inputPath);

                // Dictionary to aggregate shape counts per layer name across all pages
                Dictionary<string, int> layerCounts = new Dictionary<string, int>();

                // Iterate each page in the document
                foreach (Page page in diagram.Pages)
                {
                    // Map layer index (IX) to layer name for the current page
                    Dictionary<int, string> indexToName = new Dictionary<int, string>();
                    foreach (Layer layer in page.PageSheet.Layers)
                    {
                        int idx = layer.IX;
                        string name = layer.Name.Value;
                        indexToName[idx] = name;

                        // Ensure the layer exists in the global count dictionary
                        if (!layerCounts.ContainsKey(name))
                        {
                            layerCounts[name] = 0;
                        }
                    }

                    // Iterate each shape on the page
                    foreach (Shape shape in page.Shapes)
                    {
                        // Skip deleted shapes
                        if (shape.Del == BOOL.True)
                            continue;

                        // Retrieve the layer membership string (e.g., "0;2")
                        string memberString = shape.LayerMem.LayerMember.Value;
                        if (string.IsNullOrEmpty(memberString))
                            continue;

                        // Split and count for each referenced layer
                        string[] parts = memberString.Split(';', StringSplitOptions.RemoveEmptyEntries);
                        foreach (string part in parts)
                        {
                            if (int.TryParse(part, out int layerIdx) && indexToName.TryGetValue(layerIdx, out string layerName))
                            {
                                layerCounts[layerName] += 1;
                            }
                        }
                    }
                }

                // Prepare list for JSON serialization
                List<LayerInfo> layerInfoList = new List<LayerInfo>();
                foreach (var kvp in layerCounts)
                {
                    layerInfoList.Add(new LayerInfo { Name = kvp.Key, ShapeCount = kvp.Value });
                }

                // Serialize to JSON with indentation
                string json = JsonSerializer.Serialize(layerInfoList, new JsonSerializerOptions { WriteIndented = true });

                // Write JSON to file
                File.WriteAllText(outputPath, json);

                Console.WriteLine($"Layer information exported to '{outputPath}'.");

            }
            catch (Aspose.Diagram.DiagramException ex)
            {
                Console.Error.WriteLine($"[DiagramException] {ex.Message}");
            }
    }
    }
}