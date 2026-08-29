using System.IO;
using System;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        // Create a new diagram instance
        using (Diagram diagram = new Diagram())
        {
            // Access the first (default) page
            Page page = diagram.Pages[0];

            // Retrieve page dimensions (in inches)
            double pageWidth = page.PageSheet.PageProps.PageWidth.Value;
            double pageHeight = page.PageSheet.PageProps.PageHeight.Value;

            // Center coordinates for the watermark
            double pinX = pageWidth / 2;
            double pinY = pageHeight / 2;

            // Watermark text and formatting
            string watermarkText = "CONFIDENTIAL";
            string fontName = "Arial";
            string fontColor = "#808080"; // Gray color
            double fontSize = 72.0 / 72.0; // 72 points = 1 inch

            // Add a full‑page text shape that will serve as the watermark
            Shape watermarkShape = page.AddText(
                pinX,               // PinX (center X)
                pinY,               // PinY (center Y)
                pageWidth,          // Width of the text box (full page)
                pageHeight,         // Height of the text box (full page)
                watermarkText,      // Text content
                fontName,           // Font name
                fontColor,          // Font color
                fontSize);          // Font size (in inches)

            // Apply semi‑transparent fill to the shape (50 % opacity)
            watermarkShape.Fill.FillForegndTrans.Value = 50;

            // Save the diagram with the watermark applied
            diagram.Save("WatermarkedDiagram.vsdx", SaveFileFormat.Vsdx);
        }
    }
}
