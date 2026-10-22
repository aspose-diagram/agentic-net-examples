using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Define output file path
        string outputPath = "output.vsdx";

        try
        {
            // Create a new empty diagram
            Diagram diagram = new Diagram();

            // Access the first (default) page
            Page page = diagram.Pages[0];

            // Retrieve page dimensions (in inches)
            double pageWidth = page.PageSheet.PageProps.PageWidth.Value;
            double pageHeight = page.PageSheet.PageProps.PageHeight.Value;

            // Watermark settings
            string watermarkText = "CONFIDENTIAL";
            string fontName = "Arial";
            string fontColor = "#C0C0C0"; // Light gray
            double fontSize = 0.5; // Inches
            double boxWidth = 2.0; // Width of the text box
            double boxHeight = 0.5; // Height of the text box
            double rotationAngle = 45.0; // Degrees
            double transparency = 0.8; // 0 = opaque, 1 = fully transparent

            // Define spacing between watermarks
            double stepX = 3.0; // Horizontal step (inches)
            double stepY = 3.0; // Vertical step (inches)

            // Loop over the page area and add repeated watermark shapes
            for (double y = 0; y < pageHeight; y += stepY)
            {
                for (double x = 0; x < pageWidth; x += stepX)
                {
                    // Add a text shape as watermark
                    Shape wmShape = page.AddText(x, y, boxWidth, boxHeight, watermarkText, fontName, fontColor, fontSize);

                    // Rotate the watermark diagonally
                    wmShape.XForm.Angle.Value = rotationAngle;

                    // Set semi‑transparent fill (foreground color transparency)
                    wmShape.Fill.FillForegndTrans.Value = transparency;
                }
            }

            // Save the diagram with the repeated diagonal watermark pattern
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Diagram saved successfully to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            // Write any errors to the error stream
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}