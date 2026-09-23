using System;
using System.Collections.Generic;
using Aspose.Diagram;

namespace DiagramLayerValidator
{
    // Simple holder for layer location information
    class LayerInfo
    {
        public string PageName { get; set; }
        public int LayerIndex { get; set; }
    }

    class Program
    {
        static void Main(string[] args)
        {
            try
            {

                // Path to the Visio file to be validated
                string diagramPath = "input.vsdx";
                if (args.Length > 0)
                {
                    diagramPath = args[0];
                }

                // Load the diagram
                Diagram diagram = new Diagram(diagramPath);

                // Dictionary to track layer names and where they appear
                Dictionary<string, List<LayerInfo>> layerMap = new Dictionary<string, List<LayerInfo>>(StringComparer.OrdinalIgnoreCase);

                // Iterate through all pages and their layers
                foreach (Page page in diagram.Pages)
                {
                    foreach (Layer layer in page.PageSheet.Layers)
                    {
                        string name = layer.Name.Value ?? string.Empty;

                        if (!layerMap.ContainsKey(name))
                        {
                            layerMap[name] = new List<LayerInfo>();
                        }

                        layerMap[name].Add(new LayerInfo
                        {
                            PageName = page.Name,
                            LayerIndex = layer.IX
                        });
                    }
                }

                // Report duplicate layer names
                bool duplicatesFound = false;
                foreach (var kvp in layerMap)
                {
                    if (kvp.Value.Count > 1)
                    {
                        duplicatesFound = true;
                        Console.WriteLine($"Duplicate layer name '{kvp.Key}' found in {kvp.Value.Count} locations:");
                        foreach (var info in kvp.Value)
                        {
                            Console.WriteLine($"  Page: {info.PageName}, Layer Index: {info.LayerIndex}");
                        }
                    }
                }

                if (!duplicatesFound)
                {
                    Console.WriteLine("No duplicate layer names were found.");
                }

            }
            catch (Aspose.Diagram.DiagramException ex)
            {
                Console.Error.WriteLine($"[DiagramException] {ex.Message}");
            }
    }
    }
}