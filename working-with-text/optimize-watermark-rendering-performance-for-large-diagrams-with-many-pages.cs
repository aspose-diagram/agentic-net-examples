using System.IO;
using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        try
        {

            // Input and output file paths (adjust as needed)
            string inputPath = "input.vsdx";
            string outputPath = "output_with_watermark.vsdx";

            // Load the diagram
            Diagram diagram = new Diagram(inputPath);

            // Watermark text and appearance
            string watermarkText = "CONFIDENTIAL";
            string fontName = "Calibri";
            string fontColor = "#A5A5A5"; // Light gray in hex
            double fontSizePoints = 72; // 72 points = 1 inch
            double fontSizeInches = fontSizePoints / 72.0;

            // Add watermark to each page
            foreach (Page page in diagram.Pages)
            {
                // Retrieve page dimensions (in inches)
                double pageWidth = page.PageSheet.PageProps.PageWidth.Value;
                double pageHeight = page.PageSheet.PageProps.PageHeight.Value;

                // Center position for the watermark
                double pinX = pageWidth / 2.0;
                double pinY = pageHeight / 2.0;

                // Add a full‑page transparent text shape as watermark
                // Width and height are set to the full page size to cover the entire area
                page.AddText(pinX, pinY, pageWidth, pageHeight,
                             watermarkText, fontName, fontColor, fontSizeInches);
            }

            // Save the modified diagram
            diagram.Save(outputPath, SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
