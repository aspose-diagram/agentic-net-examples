using System.IO;
using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main()
    {
        try
        {

            // Load the Visio diagram
            Diagram diagram = new Diagram("input.vsdx");

            // Name of the target layer
            const string targetLayerName = "Watermark";

            // Iterate through all pages in the diagram
            foreach (Page page in diagram.Pages)
            {
                // Find the layer with the specified name on the current page
                int watermarkLayerIndex = -1;
                foreach (Layer layer in page.PageSheet.Layers)
                {
                    if (layer.Name.Value == targetLayerName)
                    {
                        // Layer index (IX) is the identifier used in shape layer membership strings
                        watermarkLayerIndex = layer.IX;
                        break;
                    }
                }

                // If the layer does not exist on this page, skip to the next page
                if (watermarkLayerIndex == -1)
                    continue;

                // Process each shape on the page
                foreach (Shape shape in page.Shapes)
                {
                    // Retrieve the layer membership string (e.g., "0;2;5")
                    string layerMember = shape.LayerMem.LayerMember.Value;

                    if (string.IsNullOrEmpty(layerMember))
                        continue;

                    // Split the membership string and check if the shape belongs to the watermark layer
                    string[] members = layerMember.Split(';');
                    foreach (string member in members)
                    {
                        if (int.TryParse(member, out int idx) && idx == watermarkLayerIndex)
                        {
                            // Set fill foreground transparency to 50%
                            shape.Fill.FillForegndTrans.Value = 50;

                            // Set line color transparency to 50%
                            shape.Line.LineColorTrans.Value = 50;

                            // Optional: set text background transparency if needed
                            // shape.TextBlock.TextBkgndTrans.Value = 50;

                            // No need to check other members once matched
                            break;
                        }
                    }
                }
            }

            // Save the modified diagram
            diagram.Save("output.vsdx", SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
