using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Input Visio file path
        string inputPath = "input.vsdx";
        // Guard: ensure the source file exists
        if (!File.Exists(inputPath)) { Console.Error.WriteLine($"File not found: {inputPath}"); return; }

        try
        {
            // Load the diagram from the file
            Diagram diagram = new Diagram(inputPath);

            // Access the first page (current page)
            Page page = diagram.Pages[0];

            // Retrieve page dimensions (in inches)
            double pageWidth = page.PageSheet.PageProps.PageWidth.Value;
            double pageHeight = page.PageSheet.PageProps.PageHeight.Value;

            // Watermark text and style settings
            string watermarkText = "CONFIDENTIAL";
            string fontName = "Calibri";
            string fontColor = "#808080"; // Light gray
            double fontSizePoints = 72;   // 72 points
            double fontSizeInches = fontSizePoints / 72.0; // Convert points to inches

            // Center position for the watermark
            double pinX = pageWidth / 2.0;
            double pinY = pageHeight / 2.0;

            // Add a full‑page text shape; AddText returns a Shape object directly
            Shape watermarkShape = page.AddText(pinX, pinY, pageWidth, pageHeight,
                                                watermarkText, fontName, fontColor, fontSizeInches);

            // Make the shape fill semi‑transparent (white with 80% transparency)
            watermarkShape.Fill.FillForegnd.Value = "#FFFFFF";
            watermarkShape.Fill.FillForegndTrans.Value = 80;

            // Remove any outline by setting line pattern to None and weight to zero
            watermarkShape.Line.LinePattern.Value = LinePatternValue.None;
            watermarkShape.Line.LineWeight.Value = 0;

            // Rotate the watermark diagonally (-45 degrees)
            double angleDeg = -45;
            double angleRad = (Math.PI / 180.0) * angleDeg;
            watermarkShape.TextXForm.TxtAngle.Value = angleRad;

            // Ensure a dedicated layer for watermarks exists
            string watermarkLayerName = "WatermarkLayer";
            Layer watermarkLayer = null;
            foreach (Layer layer in page.PageSheet.Layers)
            {
                if (layer.Name.Value == watermarkLayerName)
                {
                    watermarkLayer = layer;
                    break;
                }
            }
            if (watermarkLayer == null)
            {
                watermarkLayer = new Layer();
                watermarkLayer.Name.Value = watermarkLayerName;
                watermarkLayer.Visible.Value = BOOL.True;
                page.PageSheet.Layers.Add(watermarkLayer);
            }

            // Assign the shape to the watermark layer (single layer index as string)
            watermarkShape.LayerMem.LayerMember.Value = watermarkLayer.IX.ToString();

            // Save the modified diagram
            string outputPath = "output_with_watermark.vsdx";
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
        }
        catch (Exception ex)
        {
            // Write any Aspose or I/O errors to the error stream
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}