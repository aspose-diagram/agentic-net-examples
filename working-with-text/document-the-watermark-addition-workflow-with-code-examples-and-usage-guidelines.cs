using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Path to the source Visio file
        string inputPath = "input.vsdx";
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Path for the output file with watermark
        string outputPath = "output_with_watermark.vsdx";

        try
        {
            // Load the diagram from file
            Diagram diagram = new Diagram(inputPath);

            // Define watermark text and appearance
            string watermarkText = "CONFIDENTIAL";
            string watermarkFont = "Calibri";
            string watermarkColor = "#A5A5A5"; // Light gray in HEX
            double watermarkFontSizeInches = 0.5; // Approx. 36 points (1 inch = 72 points)

            // Add watermark to every page in the diagram
            foreach (Page page in diagram.Pages)
            {
                AddWatermarkToPage(page, watermarkText, watermarkFont, watermarkColor, watermarkFontSizeInches);
            }

            // Save the modified diagram
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }

    /// <summary>
    /// Adds a full‑page watermark text shape to the specified page.
    /// </summary>
    /// <param name="page">The page to which the watermark will be added.</param>
    /// <param name="text">Watermark text.</param>
    /// <param name="fontName">Font name for the watermark.</param>
    /// <param name="fontColorHex">Font color in HEX format (e.g., "#A5A5A5").</param>
    /// <param name="fontSizeInches">Font size expressed in inches.</param>
    private static void AddWatermarkToPage(Page page, string text, string fontName, string fontColorHex, double fontSizeInches)
    {
        // Retrieve page dimensions (in inches)
        double pageWidth = page.PageSheet.PageProps.PageWidth.Value;
        double pageHeight = page.PageSheet.PageProps.PageHeight.Value;

        // Position the watermark at the centre of the page.
        double pinX = pageWidth / 2.0;
        double pinY = pageHeight / 2.0;

        // Use the full page size for the text shape so the text can be rotated or stretched if needed.
        double shapeWidth = pageWidth;
        double shapeHeight = pageHeight;

        // Add the text shape. This overload returns a Shape object.
        Shape watermarkShape = page.AddText(
            pinX,               // PinX – centre X
            pinY,               // PinY – centre Y
            shapeWidth,         // Width of the shape (full page)
            shapeHeight,        // Height of the shape (full page)
            text,               // Watermark text
            fontName,           // Font name
            fontColorHex,       // Font color in HEX
            fontSizeInches);    // Font size in inches

        // Optional: adjust additional properties (e.g., rotation).
        watermarkShape.TextXForm.TxtAngle.Value = (Math.PI / 180) * 45; // Rotate 45°
        // Note: Transparency for text is not directly supported via Shape properties.
    }
}