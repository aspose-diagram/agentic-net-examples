using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
    {
        static void Main()
        {
            try
            {

                // Load an existing Visio diagram (replace with actual file path)
                Diagram diagram = new Diagram("input.vsdx");

                // Iterate through all pages in the diagram
                foreach (Page page in diagram.Pages)
                {
                    // Locate the 'Marketing' layer on the current page
                    Layer marketingLayer = null;
                    foreach (Layer layer in page.PageSheet.Layers)
                    {
                        if (layer.Name.Value == "Marketing")
                        {
                            marketingLayer = layer;
                            break;
                        }
                    }

                    // If the layer does not exist on this page, skip to the next page
                    if (marketingLayer == null)
                        continue;

                    // The index of the layer as a string (used in the shape's layer membership list)
                    string marketingLayerIndex = marketingLayer.IX.ToString();

                    // Iterate through all shapes on the page
                    foreach (Shape shape in page.Shapes)
                    {
                        // Skip shapes that are marked as deleted
                        if (shape.Del == BOOL.True)
                            continue;

                        // Retrieve the layer membership string (e.g., "0;2;5")
                        string layerMembers = shape.LayerMem.LayerMember.Value;

                        if (string.IsNullOrEmpty(layerMembers))
                            continue;

                        // Check if the shape belongs to the 'Marketing' layer
                        string[] members = layerMembers.Split(';');
                        bool belongsToMarketing = false;
                        foreach (string member in members)
                        {
                            if (member == marketingLayerIndex)
                            {
                                belongsToMarketing = true;
                                break;
                            }
                        }

                        if (!belongsToMarketing)
                            continue;

                        // Set the fill pattern to solid (1) and the foreground color to light gray (#D3D3D3)
                        shape.Fill.FillPattern.Value = 1;
                        shape.Fill.FillForegnd.Value = "#D3D3D3";
                    }
                }

                // Save the modified diagram (replace with desired output path)
                diagram.Save("output.vsdx", SaveFileFormat.Vsdx);

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }