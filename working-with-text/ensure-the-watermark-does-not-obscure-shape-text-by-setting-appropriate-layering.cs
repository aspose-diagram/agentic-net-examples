using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main()
    {
        try
        {

            // Load an existing Visio diagram
            string inputPath = "input.vsdx";
            using (FileStream fs = new FileStream(inputPath, FileMode.Open))
            {
                Diagram diagram = new Diagram(fs);

                // Work with the first page
                Page page = diagram.Pages[0];

                // Create a layer for the watermark (background)
                Layer watermarkLayer = new Layer();
                watermarkLayer.Name.Value = "WatermarkLayer";
                watermarkLayer.Visible.Value = BOOL.True;
                page.PageSheet.Layers.Add(watermarkLayer);
                int watermarkLayerIndex = watermarkLayer.IX;

                // Create a layer for regular content (foreground)
                Layer contentLayer = new Layer();
                contentLayer.Name.Value = "ContentLayer";
                contentLayer.Visible.Value = BOOL.True;
                page.PageSheet.Layers.Add(contentLayer);
                int contentLayerIndex = contentLayer.IX;

                // Determine page dimensions
                double pageWidth = page.PageSheet.PageProps.PageWidth.Value;
                double pageHeight = page.PageSheet.PageProps.PageHeight.Value;

                // Add a full‑page text watermark
                // Font size: 36 points => 36/72 inches
                Shape watermark = page.AddText(
                    pageWidth / 2,               // PinX (center)
                    pageHeight / 2,              // PinY (center)
                    pageWidth,                   // Width
                    pageHeight,                  // Height
                    "CONFIDENTIAL",              // Text
                    "Arial",                     // Font name
                    "#CCCCCC",                   // Font color (light gray)
                    36.0 / 72.0);                // Font size in inches

                // Assign the watermark shape to the background layer
                watermark.LayerMem.LayerMember.Value = watermarkLayerIndex.ToString();

                // Assign all other shapes to the foreground layer
                foreach (Shape shape in page.Shapes)
                {
                    if (shape.ID != watermark.ID)
                    {
                        shape.LayerMem.LayerMember.Value = contentLayerIndex.ToString();
                    }
                }

                // Save the modified diagram
                string outputPath = "output.vsdx";
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
            }

            Console.WriteLine("Watermark applied and diagram saved successfully.");

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
