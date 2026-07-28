using System;
using System.Linq;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
    {
        static void Main()
        {
            try
            {

                // Input and output file paths
                string inputPath = "input.vsdx";
                string outputPath = "output.vsdx";

                // Load the Visio diagram
                Diagram diagram = new Diagram(inputPath);

                // Iterate through each page in the diagram
                foreach (Page page in diagram.Pages)
                {
                    // Find the index of the layer named "Marketing"
                    int marketingLayerIndex = -1;
                    foreach (Layer layer in page.PageSheet.Layers)
                    {
                        if (layer.Name.Value == "Marketing")
                        {
                            marketingLayerIndex = layer.IX;
                            break;
                        }
                    }

                    // If the Marketing layer does not exist on this page, skip to next page
                    if (marketingLayerIndex == -1)
                        continue;

                    // Iterate through each shape on the page
                    foreach (Shape shape in page.Shapes)
                    {
                        // Skip deleted shapes
                        if (shape.Del == BOOL.True)
                            continue;

                        // Get the layer membership string (e.g., "0;2;5")
                        string layerMember = shape.LayerMem.LayerMember.Value;

                        // If the shape belongs to the Marketing layer, apply light gray fill
                        if (!string.IsNullOrEmpty(layerMember) &&
                            layerMember.Split(';').Contains(marketingLayerIndex.ToString()))
                        {
                            // Set solid fill pattern
                            shape.Fill.FillPattern.Value = 1; // 1 = solid

                            // Set foreground fill color to light gray (#D3D3D3)
                            shape.Fill.FillForegnd.Value = "#D3D3D3";
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