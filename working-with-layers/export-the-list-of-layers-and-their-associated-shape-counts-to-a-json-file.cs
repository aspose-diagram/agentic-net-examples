using System;
using System.IO;
using System.Collections.Generic;
using System.Text.Json;
using Aspose.Diagram;

namespace LayerExportExample
{
    // DTO for JSON output
    public class LayerInfo
    {
        public string PageName { get; set; } = string.Empty;
        public string LayerName { get; set; } = string.Empty;
        public int ShapeCount { get; set; }
    }

    public class Program
    {
        public static void Main(string[] args)
        {
            // Validate arguments
            if (args.Length < 2)
            {
                Console.WriteLine("Usage: LayerExportExample <inputVisioFile> <outputJsonFile>");
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

            var layerInfos = new List<LayerInfo>();

            // Iterate through each page in the diagram
            foreach (Page page in diagram.Pages)
            {
                string pageName = page.Name; // Page name

                // Build a dictionary to hold shape counts per layer index for this page
                var layerCountMap = new Dictionary<int, int>();

                // Initialize counts for all layers on the page
                foreach (Layer layer in page.PageSheet.Layers)
                {
                    layerCountMap[layer.IX] = 0;
                }

                // Count shapes per layer
                foreach (Shape shape in page.Shapes)
                {
                    // Skip deleted shapes
                    if (shape.Del == BOOL.True)
                        continue;

                    string memberString = shape.LayerMem.LayerMember.Value;
                    if (string.IsNullOrEmpty(memberString))
                        continue;

                    // Layer membership can be a semicolon‑separated list of indexes
                    string[] parts = memberString.Split(new[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
                    foreach (string part in parts)
                    {
                        if (int.TryParse(part, out int layerIdx) && layerCountMap.ContainsKey(layerIdx))
                        {
                            layerCountMap[layerIdx]++;
                        }
                    }
                }

                // Create LayerInfo objects for each layer
                foreach (Layer layer in page.PageSheet.Layers)
                {
                    int count = layerCountMap.TryGetValue(layer.IX, out int c) ? c : 0;
                    layerInfos.Add(new LayerInfo
                    {
                        PageName = pageName,
                        LayerName = layer.Name.Value,
                        ShapeCount = count
                    });
                }
            }

            // Serialize to JSON
            string json = JsonSerializer.Serialize(layerInfos, new JsonSerializerOptions { WriteIndented = true });

            // Write JSON to file
            try
            {
                File.WriteAllText(outputPath, json);
                Console.WriteLine($"Layer export completed successfully. Output written to: {outputPath}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to write JSON file: {ex.Message}");
            }
        }
    }
}