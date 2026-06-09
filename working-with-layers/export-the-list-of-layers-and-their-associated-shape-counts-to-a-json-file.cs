using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using Aspose.Diagram;

namespace DiagramLayerExport
{
    // DTO for JSON output
    public class LayerInfo
    {
        public string Name { get; set; } = string.Empty;
        public int ShapeCount { get; set; }
    }

    public class PageInfo
    {
        public string Name { get; set; } = string.Empty;
        public List<LayerInfo> Layers { get; set; } = new();
    }

    class Program
    {
        static void Main(string[] args)
        {
            try
            {

                // Input Visio file path (adjust as needed)
                string diagramPath = "input.vsdx";

                // Output JSON file path
                string jsonOutputPath = "layers_summary.json";

                // Load the diagram
                Diagram diagram = new Diagram(diagramPath);

                // Prepare list to hold page information
                List<PageInfo> pagesInfo = new List<PageInfo>();

                // Iterate through each page in the diagram
                foreach (Page page in diagram.Pages)
                {
                    PageInfo pageInfo = new PageInfo
                    {
                        Name = page.Name
                    };

                    // Build a dictionary to map layer index to LayerInfo for quick lookup
                    Dictionary<int, LayerInfo> layerMap = new Dictionary<int, LayerInfo>();

                    // Iterate through layers on the current page
                    foreach (Layer layer in page.PageSheet.Layers)
                    {
                        LayerInfo layerInfo = new LayerInfo
                        {
                            Name = layer.Name.Value,
                            ShapeCount = 0
                        };
                        // Layer.IX is the zero‑based index of the layer
                        layerMap[layer.IX] = layerInfo;
                        pageInfo.Layers.Add(layerInfo);
                    }

                    // Iterate through all shapes on the page
                    foreach (Shape shape in page.Shapes)
                    {
                        // Ensure the shape has a LayerMem object
                        if (shape.LayerMem == null)
                            continue;

                        string memberString = shape.LayerMem.LayerMember.Value;
                        if (string.IsNullOrEmpty(memberString))
                            continue; // Shape is not assigned to any layer

                        // Split the semicolon‑separated list of layer indexes
                        string[] memberIndexes = memberString.Split(';', StringSplitOptions.RemoveEmptyEntries);
                        foreach (string idxStr in memberIndexes)
                        {
                            if (int.TryParse(idxStr, out int idx) && layerMap.TryGetValue(idx, out LayerInfo li))
                            {
                                li.ShapeCount++;
                            }
                        }
                    }

                    pagesInfo.Add(pageInfo);
                }

                // Serialize the result to JSON with indentation
                JsonSerializerOptions jsonOptions = new JsonSerializerOptions
                {
                    WriteIndented = true
                };
                string json = JsonSerializer.Serialize(pagesInfo, jsonOptions);

                // Write JSON to file
                File.WriteAllText(jsonOutputPath, json);

                Console.WriteLine($"Layer summary exported to '{jsonOutputPath}'.");

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }
}