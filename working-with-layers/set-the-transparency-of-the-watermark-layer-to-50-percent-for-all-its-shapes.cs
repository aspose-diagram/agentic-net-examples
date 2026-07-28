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

            // Iterate through each page in the document
            foreach (Page page in diagram.Pages)
            {
                // Locate the layer named "Watermark" on the current page
                Layer watermarkLayer = null;
                foreach (Layer layer in page.PageSheet.Layers)
                {
                    if (layer.Name.Value == "Watermark")
                    {
                        watermarkLayer = layer;
                        break;
                    }
                }

                // If the Watermark layer does not exist on this page, skip to the next page
                if (watermarkLayer == null)
                    continue;

                int watermarkIndex = watermarkLayer.IX; // Index of the Watermark layer

                // Process every shape on the page
                foreach (Shape shape in page.Shapes)
                {
                    // Ensure the shape has layer membership information
                    if (shape.LayerMem == null)
                        continue;

                    string layerMember = shape.LayerMem.LayerMember.Value;
                    if (string.IsNullOrEmpty(layerMember))
                        continue;

                    // Check if the shape belongs to the Watermark layer
                    string[] members = layerMember.Split(';');
                    bool belongsToWatermark = false;
                    foreach (string member in members)
                    {
                        if (int.TryParse(member, out int idx) && idx == watermarkIndex)
                        {
                            belongsToWatermark = true;
                            break;
                        }
                    }

                    if (!belongsToWatermark)
                        continue;

                    // Set fill foreground transparency to 50%
                    shape.Fill.FillForegndTrans.Value = 50;

                    // Set line color transparency to 50%
                    shape.Line.LineColorTrans.Value = 50;
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
