using System;
using System.IO;
using System.Collections.Generic;
using System.Text.Json;
using Aspose.Diagram;

namespace DiagramLayerExport
{
    // Simple DTO for JSON serialization
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

                // Input Visio file path (adjust as needed)
                string inputPath = "input.vsdx";
                // Output JSON file path
                string outputPath = "layers.json";

                // Load the diagram
                Diagram diagram = new Diagram(inputPath);

                // Dictionary to hold layer index -> LayerInfo
                var layerMap = new Dictionary<int, LayerInfo>();

                // Iterate all pages
                foreach (Page page in diagram.Pages)
                {
                    // Ensure the page has a layer collection
                    if (page.PageSheet?.Layers == null) continue;

                    // Register layers from this page (if not already registered)
                    foreach (Layer layer in page.PageSheet.Layers)
                    {
                        int ix = layer.IX;
                        if (!layerMap.ContainsKey(ix))
                        {
                            layerMap[ix] = new LayerInfo
                            {
                                Name = layer.Name.Value,
                                ShapeCount = 0
                            };
                        }
                    }

                    // Count shapes per layer on this page
                    foreach (Shape shape in page.Shapes)
                    {
                        // Skip deleted shapes
                        if (shape.Del == BOOL.True) continue;

                        string memberString = shape.LayerMem?.LayerMember?.Value;
                        if (string.IsNullOrEmpty(memberString)) continue;

                        // LayerMember stores semicolon‑separated layer indexes
                        string[] parts = memberString.Split(';', StringSplitOptions.RemoveEmptyEntries);
                        foreach (string part in parts)
                        {
                            if (int.TryParse(part, out int layerIndex) && layerMap.ContainsKey(layerIndex))
                            {
                                layerMap[layerIndex].ShapeCount++;
                                // A shape can belong to multiple layers; count for each applicable layer
                            }
                        }
                    }
                }

                // Prepare list for JSON output
                var result = new List<LayerInfo>(layerMap.Values);

                // Serialize to JSON with indentation
                var jsonOptions = new JsonSerializerOptions { WriteIndented = true };
                string json = JsonSerializer.Serialize(result, jsonOptions);

                // Write JSON to file
                File.WriteAllText(outputPath, json);

                Console.WriteLine($"Layer information exported to '{outputPath}'.");

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }
}