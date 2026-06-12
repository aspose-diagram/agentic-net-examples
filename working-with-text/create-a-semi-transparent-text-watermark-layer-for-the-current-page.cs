using System.IO;
using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main()
    {
        // Create a new empty diagram
        using (Diagram diagram = new Diagram())
        {
            // Add a blank page to the diagram
            Page page = new Page();
            diagram.Pages.Add(page);

            // Retrieve page dimensions (in inches)
            double pageWidth = page.PageSheet.PageProps.PageWidth.Value;
            double pageHeight = page.PageSheet.PageProps.PageHeight.Value;

            // Calculate center position for the watermark
            double pinX = pageWidth / 2.0;
            double pinY = pageHeight / 2.0;

            // Watermark text and formatting
            string watermarkText = "CONFIDENTIAL";
            string fontName = "Arial";
            string fontColor = "#808080"; // Light gray
            double fontSizeInInches = 1.0; // 72 points = 1 inch

            // Add a full‑page text shape as the watermark
            Shape watermarkShape = page.AddText(
                pinX,               // pinX (center)
                pinY,               // pinY (center)
                pageWidth,          // width (full page)
                pageHeight,         // height (full page)
                watermarkText,
                fontName,
                fontColor,
                fontSizeInInches);

            // Rotate the watermark text (optional)
            watermarkShape.TextXForm.TxtAngle.Value = (Math.PI / 180) * 45; // 45 degrees

            // Apply semi‑transparent fill to make the watermark less intrusive
            // Transparency value is a percentage (0 = opaque, 100 = fully transparent)
            watermarkShape.Fill.FillForegndTrans.Value = 70; // 70% transparent

            // Save the diagram with the watermark
            diagram.Save("WatermarkedDiagram.vsdx", SaveFileFormat.Vsdx);
        }
    }
}
