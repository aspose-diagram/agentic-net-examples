using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
    {
        static void Main(string[] args)
        {
            try
            {

                // Input and output file paths (adjust as needed)
                string inputPath = "input.vsdx";
                string outputPath = "output.vsdx";

                // Load the diagram
                Diagram diagram = new Diagram(inputPath);

                // Process each page in the diagram
                foreach (Page page in diagram.Pages)
                {
                    // Find the existing "Design" layer
                    Layer designLayer = null;
                    foreach (Layer layer in page.PageSheet.Layers)
                    {
                        if (layer.Name.Value == "Design")
                        {
                            designLayer = layer;
                            break;
                        }
                    }

                    // If the "Design" layer does not exist on this page, skip to next page
                    if (designLayer == null)
                        continue;

                    // Create the new "Prototype" layer
                    Layer prototypeLayer = new Layer();
                    prototypeLayer.Name.Value = "Prototype";
                    prototypeLayer.Visible.Value = BOOL.True;
                    // IsColorChecked is a direct BOOL assignment (no .Value)
                    prototypeLayer.IsColorChecked = BOOL.True;

                    // Add the new layer to the page's layer collection
                    page.PageSheet.Layers.Add(prototypeLayer);

                    // Retrieve the indexes of the layers
                    int designLayerIndex = designLayer.IX;
                    int prototypeLayerIndex = prototypeLayer.IX;

                    // Iterate all shapes on the page
                    foreach (Shape shape in page.Shapes)
                    {
                        // Get current layer membership string (e.g., "0;2")
                        string layerMember = shape.LayerMem.LayerMember.Value ?? string.Empty;

                        // Split into individual indexes
                        string[] members = layerMember.Split(new[] { ';' }, StringSplitOptions.RemoveEmptyEntries);

                        // Check if shape belongs to the "Design" layer
                        bool belongsToDesign = false;
                        foreach (string m in members)
                        {
                            if (int.TryParse(m, out int idx) && idx == designLayerIndex)
                            {
                                belongsToDesign = true;
                                break;
                            }
                        }

                        if (!belongsToDesign)
                            continue; // Shape is not on the Design layer

                        // Add the Prototype layer index if not already present
                        bool alreadyInPrototype = false;
                        foreach (string m in members)
                        {
                            if (int.TryParse(m, out int idx) && idx == prototypeLayerIndex)
                            {
                                alreadyInPrototype = true;
                                break;
                            }
                        }

                        if (!alreadyInPrototype)
                        {
                            // Append the new layer index
                            string newLayerMember = string.IsNullOrEmpty(layerMember)
                                ? prototypeLayerIndex.ToString()
                                : layerMember + ";" + prototypeLayerIndex.ToString();

                            shape.LayerMem.LayerMember.Value = newLayerMember;
                        }
                    }
                }

                // Save the modified diagram
                diagram.Save(outputPath, SaveFileFormat.Vsdx);

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }