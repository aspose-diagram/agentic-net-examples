using System.IO;
using System;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        try
        {

            // Load an existing Visio diagram (replace with your file path)
            string inputPath = "input.vsdx";
            Diagram diagram = new Diagram(inputPath);

            // Get the first (current) page
            Page page = diagram.Pages[0];

            // Retrieve page dimensions (in inches)
            double pageWidth = page.PageSheet.PageProps.PageWidth.Value;
            double pageHeight = page.PageSheet.PageProps.PageHeight.Value;

            // Center position for the watermark
            double pinX = pageWidth / 2.0;
            double pinY = pageHeight / 2.0;

            // Watermark text and formatting
            string watermarkText = "CONFIDENTIAL";
            string fontName = "Arial";
            string fontColor = "#808080"; // Gray color
            double fontSizeInInches = 1.0; // 72 points = 1 inch

            // Add the watermark as a text shape covering the whole page
            Shape watermarkShape = page.AddText(
                pinX,               // PinX (center X)
                pinY,               // PinY (center Y)
                pageWidth,          // Width of the text box
                pageHeight,         // Height of the text box
                watermarkText,
                fontName,
                fontColor,
                fontSizeInInches);

            // Rotate the watermark (optional, e.g., 45 degrees)
            watermarkShape.TextXForm.TxtAngle.Value = (float)((Math.PI / 180) * 45);

            // Apply semi‑transparent background to the text block to simulate opacity
            // 0 = opaque, 100 = fully transparent
            watermarkShape.TextBlock.TextBkgndTrans.Value = 50; // 50 % transparency

            // Save the modified diagram
            string outputPath = "output.vsdx";
            diagram.Save(outputPath, SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
