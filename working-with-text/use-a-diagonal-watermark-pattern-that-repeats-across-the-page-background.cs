using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Define output file path
        string outputPath = "WatermarkedDiagram.vsdx";

        // Guard: ensure the directory for the output exists
        string outputDir = Path.GetDirectoryName(Path.GetFullPath(outputPath));
        if (!Directory.Exists(outputDir))
        {
            Console.Error.WriteLine($"Output directory does not exist: {outputDir}");
            return;
        }

        try
        {
            // Create a new empty diagram (contains a default page)
            Diagram diagram = new Diagram();

            // Access the first (default) page
            Page page = diagram.Pages[0];

            // Retrieve page dimensions (in inches)
            double pageWidth = page.PageSheet.PageProps.PageWidth.Value;
            double pageHeight = page.PageSheet.PageProps.PageHeight.Value;

            // Watermark settings
            string watermarkText = "CONFIDENTIAL";
            string fontName = "Calibri";
            string fontColor = "#a5a5a5"; // Light gray
            double fontSize = 0.25; // Approx. 18 pt (Visio uses inches)

            // Define spacing for repeated watermarks
            double stepX = pageWidth / 5.0;
            double stepY = pageHeight / 5.0;

            // Loop to place watermarks across the page
            for (double y = 0; y < pageHeight; y += stepY)
            {
                for (double x = 0; x < pageWidth; x += stepX)
                {
                    // Add a text shape as watermark
                    // Parameters: pinX, pinY, width, height, text, fontName, fontColor, fontSize
                    Shape wmShape = page.AddText(
                        x,               // pinX
                        y,               // pinY
                        2.0,             // width (arbitrary, enough to contain text)
                        0.5,             // height (arbitrary)
                        watermarkText,
                        fontName,
                        fontColor,
                        fontSize);

                    // Rotate the watermark 45 degrees for diagonal appearance
                    wmShape.XForm.Angle.Value = 45.0;

                    // Set semi‑transparent fill to make the watermark faint
                    // FillForegndTrans: 0.0 = opaque, 1.0 = fully transparent
                    wmShape.Fill.FillForegndTrans.Value = 0.8; // 80% transparent
                }
            }

            // Save the diagram with the watermarks
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Diagram saved successfully to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            // Write any Aspose or IO errors to the error stream
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}