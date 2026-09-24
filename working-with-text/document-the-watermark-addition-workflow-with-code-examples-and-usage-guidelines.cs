using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Path to the source Visio file
        string sourcePath = "input.vsdx";

        // Verify the source file exists before proceeding
        if (!File.Exists(sourcePath))
        {
            Console.Error.WriteLine($"File not found: {sourcePath}");
            return;
        }

        try
        {
            // Load the diagram (no LoadOptions needed)
            Diagram diagram = new Diagram(sourcePath);

            // Get the first page (index 0)
            Page page = diagram.Pages[0];

            // Retrieve page dimensions (in inches)
            double pageWidth = page.PageSheet.PageProps.PageWidth.Value;
            double pageHeight = page.PageSheet.PageProps.PageHeight.Value;

            // Calculate center position for the watermark
            double centerX = pageWidth / 2.0;
            double centerY = pageHeight / 2.0;

            // Watermark text and formatting
            string watermarkText = "CONFIDENTIAL";
            string fontName = "Calibri";
            string fontColorHex = "#A5A5A5"; // Light gray
            double fontSizeInInches = 0.25; // Approx. 18 points (1 point = 1/72 inch)

            // Add a full‑page text shape that will act as the watermark
            // AddText(pinX, pinY, width, height, text, fontName, fontColor, fontSize)
            Shape watermarkShape = page.AddText(
                centerX,
                centerY,
                pageWidth,
                pageHeight,
                watermarkText,
                fontName,
                fontColorHex,
                fontSizeInInches);

            // Rotate the watermark 45 degrees
            watermarkShape.XForm.Angle.Value = 45;

            // -----------------------------------------------------------------
            // Save the diagram with the watermark applied
            // -----------------------------------------------------------------

            // Example 1: Save as PDF with default font fallback
            string pdfOutput = "output.pdf";
            PdfSaveOptions pdfOptions = new PdfSaveOptions();
            pdfOptions.DefaultFont = "Arial"; // Fallback font if original font is missing
            diagram.Save(pdfOutput, pdfOptions);

            // Example 2: Save as VSDX (Visio) format
            string vsdxOutput = "output.vsdx";
            diagram.Save(vsdxOutput, SaveFileFormat.Vsdx);

            Console.WriteLine("Watermark added and files saved successfully.");
        }
        catch (Exception ex)
        {
            // Write any Aspose or I/O errors to the error stream
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}