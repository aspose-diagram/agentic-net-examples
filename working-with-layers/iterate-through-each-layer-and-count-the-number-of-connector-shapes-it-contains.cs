using System;
using Aspose.Diagram;

class Program
    {
        static void Main()
        {
            try
            {

                // Load the Visio diagram (replace with your actual file path)
                string diagramPath = "input.vsdx";
                Diagram diagram = new Diagram(diagramPath);

                // Iterate through each page in the diagram
                foreach (Page page in diagram.Pages)
                {
                    // Get the collection of layers for the current page
                    var layers = page.PageSheet.Layers;

                    // Iterate through each layer by index
                    for (int layerIndex = 0; layerIndex < layers.Count; layerIndex++)
                    {
                        Layer layer = layers[layerIndex];
                        int connectorCount = 0;

                        // Iterate through all shapes on the page
                        foreach (Shape shape in page.Shapes)
                        {
                            // Check if the shape is a connector (1‑D shape)
                            if (shape.OneD)
                            {
                                // Ensure the shape has layer membership information
                                if (shape.LayerMem != null && shape.LayerMem.LayerMember != null)
                                {
                                    string memberValue = shape.LayerMem.LayerMember.Value; // e.g., "0;2"
                                    if (!string.IsNullOrEmpty(memberValue))
                                    {
                                        // Split the semicolon‑separated list of layer indexes
                                        string[] parts = memberValue.Split(new[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
                                        foreach (string part in parts)
                                        {
                                            if (int.TryParse(part, out int idx) && idx == layerIndex)
                                            {
                                                connectorCount++;
                                                break; // Shape belongs to this layer, no need to check further
                                            }
                                        }
                                    }
                                }
                            }
                        }

                        // Output the result for the current layer
                        Console.WriteLine($"Page \"{page.Name}\" - Layer \"{layer.Name.Value}\" (Index {layerIndex}) contains {connectorCount} connector shape(s).");
                    }
                }

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }