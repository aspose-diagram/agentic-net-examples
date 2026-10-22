using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Validate input arguments
        if (args.Length == 0)
        {
            Console.Error.WriteLine("Please provide the path to the Visio diagram file as an argument.");
            return;
        }

        string diagramPath = args[0];

        // Guard against missing file
        if (!File.Exists(diagramPath))
        {
            Console.Error.WriteLine($"File not found: {diagramPath}");
            return;
        }

        try
        {
            // Load the Visio diagram inside a using block (mandatory pattern)
            using (Diagram diagram = new Diagram(diagramPath))
            {
                // Dictionary to aggregate layer information across all pages
                // Key: layer name, Value: aggregated info (visibility and shape count)
                Dictionary<string, LayerInfo> aggregatedLayers = new Dictionary<string, LayerInfo>(StringComparer.OrdinalIgnoreCase);

                // Iterate through each page in the diagram
                foreach (Page page in diagram.Pages)
                {
                    // Map of layer index (IX) to layer name for the current page
                    Dictionary<int, string> ixToName = new Dictionary<int, string>();

                    // Collect layers defined on this page
                    foreach (Layer layer in page.PageSheet.Layers)
                    {
                        // Retrieve the layer's name and visibility using the correct .Value properties
                        string layerName = layer.Name.Value;
                        BOOL visibility = layer.Visible.Value;

                        // Store the index-to-name mapping for later shape processing
                        ixToName[layer.IX] = layerName;

                        // Ensure a unique entry in the aggregated dictionary
                        if (!aggregatedLayers.ContainsKey(layerName))
                        {
                            aggregatedLayers[layerName] = new LayerInfo
                            {
                                Visible = visibility,
                                Count = 0
                            };
                        }
                    }

                    // Iterate through each shape on the page to count its layer memberships
                    foreach (Shape shape in page.Shapes)
                    {
                        // Skip shapes without layer membership information
                        if (shape.LayerMem == null || shape.LayerMem.LayerMember == null)
                            continue;

                        string memberValue = shape.LayerMem.LayerMember.Value;
                        if (string.IsNullOrWhiteSpace(memberValue))
                            continue;

                        // LayerMember.Value may contain multiple layer indices separated by commas or semicolons
                        string[] parts = memberValue.Split(new[] { ',', ';' }, StringSplitOptions.RemoveEmptyEntries);
                        foreach (string part in parts)
                        {
                            if (int.TryParse(part.Trim(), out int ix))
                            {
                                // Resolve the layer index to a layer name using the page-specific map
                                if (ixToName.TryGetValue(ix, out string layerName))
                                {
                                    // Increment the shape count for the resolved layer
                                    if (aggregatedLayers.TryGetValue(layerName, out LayerInfo info))
                                    {
                                        info.Count++;
                                    }
                                }
                            }
                        }
                    }
                }

                // Output the summary report
                Console.WriteLine("Layer Summary Report");
                Console.WriteLine("--------------------");
                foreach (KeyValuePair<string, LayerInfo> kvp in aggregatedLayers)
                {
                    LayerInfo info = kvp.Value;
                    string visibilityText = info.Visible == BOOL.True ? "True" : "False";
                    Console.WriteLine($"Name: {kvp.Key}, Visible: {visibilityText}, Shape Count: {info.Count}");
                }
            }
        }
        catch (Exception ex)
        {
            // Write any Aspose or runtime errors to the error stream
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }

    // Helper class to store aggregated layer details
    private class LayerInfo
    {
        public BOOL Visible { get; set; }
        public int Count { get; set; }
    }
}