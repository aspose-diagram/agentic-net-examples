using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Validate arguments
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <inputVisioPath> <outputVisioPath>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

        // Guard: check input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the Visio diagram inside a using block
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Retrieve the first page (current page)
                Page page = diagram.Pages[0];

                // -----------------------------------------------------------------
                // Create a new layer for the watermark
                // -----------------------------------------------------------------
                Layer watermarkLayer = new Layer();
                watermarkLayer.Name.Value = "WatermarkLayer";
                watermarkLayer.Visible.Value = BOOL.True;   // Make layer visible
                watermarkLayer.Print.Value = BOOL.False;    // Do not print by default
                // Add the layer to the page's layer collection
                page.PageSheet.Layers.Add(watermarkLayer);

                // -----------------------------------------------------------------
                // Determine page dimensions to cover the full page
                // -----------------------------------------------------------------
                double pageWidth = page.PageSheet.PageProps.PageWidth.Value;
                double pageHeight = page.PageSheet.PageProps.PageHeight.Value;

                // Position the watermark at the top-left corner (0,0) and span full page
                double pinX = 0;
                double pinY = 0;
                double width = pageWidth;
                double height = pageHeight;

                // Watermark text and appearance
                string watermarkText = "CONFIDENTIAL";
                string fontName = "Calibri";
                string fontColor = "#808080"; // Light gray
                double fontSizePoints = 72;    // 72 points = 1 inch
                double fontSizeInches = fontSizePoints / 72.0;

                // Add the text shape to the page
                Shape watermarkShape = page.AddText(pinX, pinY, width, height, watermarkText, fontName, fontColor, fontSizeInches);

                // Assign the shape to the watermark layer
                watermarkShape.LayerMem.LayerMember.Value = watermarkLayer.IX.ToString();

                // -----------------------------------------------------------------
                // Set semi‑transparent fill for the shape (background of the text)
                // -----------------------------------------------------------------
                // FillForegndTrans: 0.0 = opaque, 1.0 = fully transparent
                watermarkShape.Fill.FillForegndTrans.Value = 0.5; // 50% transparency
                // Ensure a solid fill pattern
                watermarkShape.Fill.FillPattern.Value = 1;

                // -----------------------------------------------------------------
                // Ensure the text character formatting matches the desired style
                // -----------------------------------------------------------------
                if (watermarkShape.Chars.Count > 0)
                {
                    // Set font name, color, and size for the first character run
                    watermarkShape.Chars[0].FontName.Value = fontName;
                    watermarkShape.Chars[0].Color.Value = fontColor;
                    watermarkShape.Chars[0].Size.Value = fontSizeInches;
                }

                // -----------------------------------------------------------------
                // Save the modified diagram
                // -----------------------------------------------------------------
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
                Console.WriteLine($"Watermark added and diagram saved to: {outputPath}");
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}