using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        try
        {
            // Create a new empty diagram
            Diagram diagram = new Diagram();

            // Add a blank page to the diagram
            diagram.Pages.Add(new Page());

            // Get the first (and only) page
            Page page = diagram.Pages[0];

            // Ensure the page has standard dimensions (8.5" x 11")
            page.PageSheet.PageProps.PageWidth.Value = 8.5;
            page.PageSheet.PageProps.PageHeight.Value = 11.0;

            // Calculate center position for the watermark
            double pinX = page.PageSheet.PageProps.PageWidth.Value / 2.0;
            double pinY = page.PageSheet.PageProps.PageHeight.Value / 2.0;
            double pageWidth = page.PageSheet.PageProps.PageWidth.Value;
            double pageHeight = page.PageSheet.PageProps.PageHeight.Value;

            // Watermark text and styling
            string watermarkText = "CONFIDENTIAL";
            string fontName = "Arial";
            string fontColor = "#CCCCCC"; // Light gray
            double fontSizeInInches = 1.0; // 72 points = 1 inch

            // Add the watermark as a full‑page text shape (returns a Shape object)
            Shape watermarkShape = page.AddText(pinX, pinY, pageWidth, pageHeight,
                                               watermarkText, fontName, fontColor, fontSizeInInches);

            // Set line (border) transparency to 30%
            watermarkShape.Line.LineColorTrans.Value = 30;

            // Set fill (background) transparency to 30% for a subtle effect
            watermarkShape.Fill.FillForegndTrans.Value = 30;

            // Save the diagram in VSDX format
            diagram.Save("WatermarkedDiagram.vsdx", SaveFileFormat.Vsdx);
        }
        catch (Exception ex)
        {
            // Write any errors to the error stream
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}