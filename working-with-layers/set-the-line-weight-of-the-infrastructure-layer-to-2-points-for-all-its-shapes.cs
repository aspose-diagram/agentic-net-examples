using System;
using Aspose.Diagram;

class Program
    {
        static void Main()
        {
            try
            {

                // Load an existing Visio diagram (replace with actual file path)
                Diagram diagram = new Diagram("input.vsdx");

                // Find the index (IX) of the layer named "Infrastructure"
                int infrastructureLayerIndex = -1;
                // Layers are stored in the PageSheet of each page; they are shared across pages,
                // so checking the first page is sufficient.
                if (diagram.Pages.Count > 0)
                {
                    foreach (Layer layer in diagram.Pages[0].PageSheet.Layers)
                    {
                        if (layer.Name.Value == "Infrastructure")
                        {
                            infrastructureLayerIndex = layer.IX;
                            break;
                        }
                    }
                }

                if (infrastructureLayerIndex == -1)
                {
                    Console.WriteLine("Layer 'Infrastructure' not found.");
                    return;
                }

                // Convert the layer index to string for comparison with the shape's layer membership cell
                string layerIndexString = infrastructureLayerIndex.ToString();

                // Iterate through all pages and shapes, updating line weight where the shape belongs to the target layer
                foreach (Page page in diagram.Pages)
                {
                    foreach (Shape shape in page.Shapes)
                    {
                        // The LayerMember cell contains a semicolon‑separated list of layer indexes
                        string layerMember = shape.LayerMem.LayerMember.Value;
                        if (string.IsNullOrEmpty(layerMember))
                            continue;

                        // Split the list and check for the target layer index
                        string[] members = layerMember.Split(';');
                        foreach (string member in members)
                        {
                            if (member.Trim() == layerIndexString)
                            {
                                // Set line weight to 2 points (2/72 inches)
                                shape.Line.LineWeight.Value = 2.0 / 72.0;
                                break;
                            }
                        }
                    }
                }

                // Save the modified diagram
                diagram.Save("output.vsdx", SaveFileFormat.Vsdx);
                Console.WriteLine("Line weight updated for all shapes in the 'Infrastructure' layer.");

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }