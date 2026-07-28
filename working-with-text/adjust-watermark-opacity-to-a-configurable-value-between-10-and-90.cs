using System.IO;
using System;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        try
        {

            // Configurable opacity (0.1 = 10% opaque, 0.9 = 90% opaque)
            double opacity = 0.5; // Example value; adjust as needed

            // Validate opacity range
            if (opacity < 0.1 || opacity > 0.9)
                throw new Exception("Opacity must be between 0.1 (10%) and 0.9 (90%).");

            // Paths for input and output diagrams
            string inputPath = "input.vsdx";
            string outputPath = "output.vsdx";

            // Load the diagram
            Diagram diagram = new Diagram(inputPath);

            // Work with the active page
            Page page = diagram.ActivePage;

            // Retrieve page dimensions (in inches)
            double pageWidth = page.PageSheet.PageProps.PageWidth.Value;
            double pageHeight = page.PageSheet.PageProps.PageHeight.Value;

            // Center position for the watermark
            double pinX = pageWidth / 2.0;
            double pinY = pageHeight / 2.0;

            // Use full page size for the watermark shape
            double width = pageWidth;
            double height = pageHeight;

            // Add watermark text shape (font size expressed in inches; 72 points = 1 inch)
            Shape watermark = page.AddText(pinX, pinY, width, height,
                                          "CONFIDENTIAL",      // Text
                                          "Arial",             // Font name
                                          "#CCCCCC",           // Font color (light gray)
                                          1.0);                // Font size (1 inch ≈ 72 pt)

            // Convert opacity to transparency (0 = opaque, 1 = fully transparent)
            double transparency = 1.0 - opacity;

            // Apply transparency to the shape's fill (affects the watermark appearance)
            watermark.Fill.FillForegndTrans.Value = transparency;

            // Save the modified diagram
            diagram.Save(outputPath, SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
