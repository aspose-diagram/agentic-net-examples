using System.IO;
using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    // Applies a full‑page text watermark with the specified opacity (0.1‑0.9).
    static void ApplyWatermark(Diagram diagram, string watermarkText, double opacity)
    {
        // Clamp opacity to the allowed range.
        if (opacity < 0.1) opacity = 0.1;
        if (opacity > 0.9) opacity = 0.9;

        // Get the first page (or any target page).
        Page page = diagram.Pages[0];

        // Retrieve page dimensions (in inches).
        double pageWidth = page.PageSheet.PageProps.PageWidth.Value;
        double pageHeight = page.PageSheet.PageProps.PageHeight.Value;

        // Position the watermark at the centre of the page.
        double pinX = pageWidth / 2.0;
        double pinY = pageHeight / 2.0;

        // Add a text shape that spans the whole page.
        // Font size is given in inches; 0.5 inches ≈ 36 pt.
        Shape watermarkShape = page.AddText(pinX, pinY, pageWidth, pageHeight,
                                            watermarkText, "Calibri", "#808080", 0.5);

        // Set the fill transparency to simulate watermark opacity.
        // FillForegndTrans expects a value between 0.0 (opaque) and 1.0 (fully transparent).
        watermarkShape.Fill.FillForegndTrans.Value = 1.0 - opacity; // inverse because 0 = opaque

        // Optionally make the text itself semi‑transparent.
        watermarkShape.Line.LineColorTrans.Value = (int)((1.0 - opacity) * 100);
    }

    static void Main(string[] args)
    {
        try
        {

            // Input Visio file.
            string inputPath = "input.vsdx";

            // Output Visio file with watermark applied.
            string outputPath = "output.vsdx";

            // Watermark text and desired opacity (10%‑90%).
            string watermarkText = "CONFIDENTIAL";
            double opacity = 0.5; // 50 % opacity

            // Load the diagram.
            Diagram diagram = new Diagram(inputPath);

            // Apply the watermark.
            ApplyWatermark(diagram, watermarkText, opacity);

            // Save the modified diagram.
            diagram.Save(outputPath, SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
