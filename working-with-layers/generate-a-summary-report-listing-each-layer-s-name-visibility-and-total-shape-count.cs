using System;
using System.Collections.Generic;
using Aspose.Diagram;

class Program
    {
        static void Main(string[] args)
        {
            try
            {

                // Path to the Visio file (replace with actual file path)
                string visioFilePath = "input.vsdx";

                // Load the diagram
                Diagram diagram = new Diagram(visioFilePath);

                // Iterate through each page in the diagram
                foreach (Page page in diagram.Pages)
                {
                    // Retrieve the layers collection for the current page
                    LayerCollection layers = page.PageSheet.Layers;

                    // Prepare a dictionary to hold shape counts per layer index
                    Dictionary<int, int> layerShapeCounts = new Dictionary<int, int>();

                    // Initialize counts for all layers on this page
                    foreach (Layer layer in layers)
                    {
                        layerShapeCounts[layer.IX] = 0;
                    }

                    // Iterate through all shapes on the page
                    foreach (Shape shape in page.Shapes)
                    {
                        // Get the layer membership string (e.g., "0;2")
                        string member = shape.LayerMem.LayerMember.Value;

                        if (string.IsNullOrEmpty(member))
                            continue; // Shape does not belong to any layer

                        // Split the membership string and count the shape for each layer
                        string[] indices = member.Split(new[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
                        foreach (string idxStr in indices)
                        {
                            if (int.TryParse(idxStr, out int idx) && layerShapeCounts.ContainsKey(idx))
                            {
                                layerShapeCounts[idx]++;
                            }
                        }
                    }

                    // Output the summary for each layer on this page
                    Console.WriteLine($"Page: {page.Name}");
                    foreach (Layer layer in layers)
                    {
                        int count = layerShapeCounts.TryGetValue(layer.IX, out int c) ? c : 0;
                        string visibility = layer.Visible.Value == BOOL.True ? "Visible" : "Hidden";
                        Console.WriteLine($"Layer: {layer.Name.Value}, Visibility: {visibility}, Shape Count: {count}");
                    }

                    Console.WriteLine(); // Blank line between pages
                }

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }