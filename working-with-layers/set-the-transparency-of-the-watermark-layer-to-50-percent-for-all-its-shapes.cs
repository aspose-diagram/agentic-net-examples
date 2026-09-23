using System.IO;
using System;
using System.Collections.Generic;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        try
        {

            // Load an existing Visio diagram
            Diagram diagram = new Diagram("input.vsdx");

            // Iterate through all pages in the diagram
            foreach (Page page in diagram.Pages)
            {
                // Find the layer named "Watermark"
                Layer watermarkLayer = null;
                foreach (Layer layer in page.PageSheet.Layers)
                {
                    if (layer.Name.Value == "Watermark")
                    {
                        watermarkLayer = layer;
                        break;
                    }
                }

                // If the layer does not exist on this page, continue to next page
                if (watermarkLayer == null)
                    continue;

                // Get the index of the watermark layer as a string
                string layerIndexStr = watermarkLayer.IX.ToString();

                // Iterate through all shapes on the page
                foreach (Shape shape in page.Shapes)
                {
                    // Check if the shape belongs to the watermark layer
                    // The LayerMember property contains semicolon‑separated layer indexes
                    string member = shape.LayerMem.LayerMember.Value;
                    if (string.IsNullOrEmpty(member))
                        continue;

                    // Split the member string and see if it contains the watermark layer index
                    string[] indexes = member.Split(new[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
                    bool belongsToWatermark = false;
                    foreach (string idx in indexes)
                    {
                        if (idx == layerIndexStr)
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
