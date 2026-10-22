using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Expect two arguments: input Visio file path and output Visio file path.
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <input.vsdx> <output.vsdx>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

        // Guard against missing input file.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the Visio diagram inside a using block as required.
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Iterate through each page to add a watermark behind existing shapes.
                foreach (Page page in diagram.Pages)
                {
                    // Retrieve page dimensions (in inches).
                    double pageWidth = page.PageSheet.PageProps.PageWidth.Value;
                    double pageHeight = page.PageSheet.PageProps.PageHeight.Value;

                    // Calculate center position for the watermark.
                    double pinX = pageWidth / 2.0;
                    double pinY = pageHeight / 2.0;

                    // Add a full‑page text shape that will serve as the watermark.
                    // The AddText overload returns a Shape instance.
                    Shape watermarkShape = page.AddText(
                        pinX,                     // PinX (center X)
                        pinY,                     // PinY (center Y)
                        pageWidth,                // Width (full page)
                        pageHeight,               // Height (full page)
                        "CONFIDENTIAL",           // Watermark text
                        "Calibri",                // Font name
                        "#a5a5a5",                // Font color (light gray)
                        0.25);                    // Font size in inches (≈18 pt)

                    // OPTIONAL: make the watermark semi‑transparent by adjusting fill transparency.
                    // 0 = opaque, 100 = fully transparent.
                    watermarkShape.Fill.FillForegndTrans.Value = 80; // 80% transparent.

                    // OPTIONAL: remove outline.
                    watermarkShape.Line.LineWeight.Value = 0;

                    // Ensure a dedicated background layer exists for the watermark.
                    Layer watermarkLayer = null;
                    foreach (Layer existingLayer in page.PageSheet.Layers)
                    {
                        if (existingLayer.Name.Value.Equals("WatermarkLayer", StringComparison.OrdinalIgnoreCase))
                        {
                            watermarkLayer = existingLayer;
                            break;
                        }
                    }

                    if (watermarkLayer == null)
                    {
                        // Create a new layer for the watermark.
                        watermarkLayer = new Layer();
                        watermarkLayer.Name.Value = "WatermarkLayer";
                        watermarkLayer.Visible.Value = BOOL.True;
                        watermarkLayer.Print.Value = BOOL.True;
                        watermarkLayer.Lock.Value = BOOL.False;
                        watermarkLayer.IsColorChecked = BOOL.False;
                        watermarkLayer.Color.Value = "#FFFFFF";

                        // Add the layer to the page's layer collection.
                        page.PageSheet.Layers.Add(watermarkLayer);

                        // Attempt to set the new layer as the background (index 0).
                        try
                        {
                            watermarkLayer.IX = 0;
                        }
                        catch
                        {
                            // If setting IX fails, continue without forcing the index.
                        }
                    }

                    // Assign the watermark shape to the background layer.
                    // LayerMember expects a semicolon‑separated list of layer indexes.
                    watermarkShape.LayerMem.LayerMember.Value = watermarkLayer.IX.ToString();
                }

                // Save the modified diagram to the specified output path using the correct overload.
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
                Console.WriteLine($"Diagram saved successfully to: {outputPath}");
            }
        }
        catch (Exception ex)
        {
            // Write any errors to the error console.
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}