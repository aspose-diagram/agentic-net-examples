using System.IO;
using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main()
    {
        try
        {

            // Paths for input diagram and output diagram with watermark
            string inputPath = "input.vsdx";
            string outputPath = "output_watermarked.vsdx";

            // Load the existing Visio diagram
            Diagram diagram = new Diagram(inputPath);

            // Watermark configuration
            string watermarkText = "CONFIDENTIAL";
            string fontName = "Arial";
            string fontColor = "#CCCCCC"; // Light gray color in hex
            double fontSizePoints = 72;   // Font size in points
            double fontSizeInches = fontSizePoints / 72.0; // Convert points to inches (required by AddText)

            // Add the watermark to every page
            foreach (Page page in diagram.Pages)
            {
                // Retrieve page dimensions (in inches)
                double pageWidth = page.PageSheet.PageProps.PageWidth.Value;
                double pageHeight = page.PageSheet.PageProps.PageHeight.Value;

                // Center of the page – used as the pin position for the text shape
                double pinX = pageWidth / 2.0;
                double pinY = pageHeight / 2.0;

                // Add a full‑page text shape that acts as a watermark.
                // Overload: AddText(pinX, pinY, width, height, text, fontName, fontColor, fontSizeInches)
                page.AddText(pinX, pinY, pageWidth, pageHeight, watermarkText, fontName, fontColor, fontSizeInches);
            }

            // Configure save options to avoid extra layout processing
            DiagramSaveOptions saveOptions = new DiagramSaveOptions(SaveFileFormat.Vsdx);
            saveOptions.AutoFitPageToDrawingContent = false; // Prevent page resizing during save
            saveOptions.DefaultFont = "Arial";               // Fallback font for missing glyphs

            // Save the modified diagram
            diagram.Save(outputPath, saveOptions);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
