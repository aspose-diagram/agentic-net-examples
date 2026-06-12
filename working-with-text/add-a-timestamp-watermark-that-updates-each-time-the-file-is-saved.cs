using System.IO;
using System;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        try
        {

            // Input and output file paths (adjust as needed)
            string inputPath = "input.vsdx";
            string outputPath = "output.vsdx";

            // Load the diagram
            Diagram diagram = new Diagram(inputPath);

            // Add or update timestamp watermark on each page
            AddTimestampWatermark(diagram);

            // Save the diagram (the watermark will be persisted)
            diagram.Save(outputPath, SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }

    // Adds a timestamp watermark to every page of the diagram
    static void AddTimestampWatermark(Diagram diagram)
    {
        // Current timestamp string
        string timestamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

        // Font settings for the watermark
        const string fontName = "Calibri";
        const string fontColor = "#A0A0A0"; // Light gray
        const double fontSizeInches = 0.2;   // Approx. 14.4 points (0.2 * 72)

        foreach (Page page in diagram.Pages)
        {
            // Full page dimensions
            double pageWidth = page.PageSheet.PageProps.PageWidth.Value;
            double pageHeight = page.PageSheet.PageProps.PageHeight.Value;

            // Position the watermark at the bottom-right corner with a small margin
            double margin = 0.2; // inches
            double pinX = pageWidth - margin;
            double pinY = margin;

            // Add the watermark text shape
            Shape watermarkShape = page.AddText(pinX, pinY, pageWidth, pageHeight,
                                                timestamp, fontName, fontColor, fontSizeInches);

            // Optional: send the watermark to the back so it doesn't obscure other shapes
            // Note: BringToFront/SendToBack use shape IDs (long)
            page.SendToBack(watermarkShape.ID);
        }
    }
}
