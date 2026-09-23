using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Input Visio file path
        string inputPath = "input.vsdx";
        // Guard: ensure the input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Output Visio file path
        string outputPath = "output_with_watermark.vsdx";

        // Watermark configuration
        string watermarkText = "CONFIDENTIAL";
        double opacityPercent = 30.0;   // 0 (transparent) to 100 (opaque)
        double rotationDegrees = 45.0;  // Rotation angle in degrees
        string fontName = "Calibri";
        string fontColorHex = "#A0A0A0"; // Light gray
        double fontSizePoints = 72.0;    // Font size in points

        try
        {
            // Load the diagram from the input file
            Diagram diagram = new Diagram(inputPath);

            // Iterate through all pages and add the watermark shape
            foreach (Page page in diagram.Pages)
            {
                // Page dimensions (in inches)
                double pageWidth = page.PageSheet.PageProps.PageWidth.Value;
                double pageHeight = page.PageSheet.PageProps.PageHeight.Value;

                // Center position for the watermark shape
                double pinX = pageWidth / 2.0;
                double pinY = pageHeight / 2.0;

                // Convert font size from points to inches (1 point = 1/72 inch)
                double fontSizeInches = fontSizePoints / 72.0;

                // Add a full‑page text shape containing the watermark text.
                // AddText returns a Shape instance, not an ID.
                Shape watermarkShape = page.AddText(pinX, pinY, pageWidth, pageHeight,
                                                    watermarkText, fontName, fontColorHex, fontSizeInches);

                // Apply rotation (degrees) to the shape.
                watermarkShape.XForm.Angle.Value = rotationDegrees;

                // Apply opacity by setting the foreground fill transparency (0 = opaque, 100 = fully transparent).
                // This affects the shape's fill; the text inherits the same visual opacity.
                watermarkShape.Fill.FillForegndTrans.Value = opacityPercent;

                // Optional: remove any outline to keep the watermark clean.
                watermarkShape.Line.LinePattern.Value = LinePatternValue.None;
            }

            // Save the modified diagram with the watermark applied.
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
        }
        catch (Exception ex)
        {
            // Write any Aspose or I/O errors to the error stream.
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}