using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Validate command‑line arguments.
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <input.vsdx> <output.vsdx>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

        // Guard: ensure the input file exists.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Watermark configuration – adjust as needed.
        string watermarkText = "CONFIDENTIAL";
        double opacity = 0.3;               // 0.0 = fully opaque, 1.0 = fully transparent
        double rotationDegrees = 45.0;      // Rotation angle in degrees
        string fontName = "Arial";
        string fontColor = "#C0C0C0";       // Light gray in hex
        double fontSizePoints = 72.0;       // Font size in points
        // Convert points to inches (Visio uses inches for font size)
        double fontSizeInches = fontSizePoints / 72.0;

        try
        {
            // Load the diagram inside a using block as required.
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Iterate over every page in the diagram.
                foreach (Page page in diagram.Pages)
                {
                    // Retrieve page dimensions (in inches).
                    double pageWidth = page.PageSheet.PageProps.PageWidth.Value;
                    double pageHeight = page.PageSheet.PageProps.PageHeight.Value;

                    // Calculate the centre position for the watermark shape.
                    double pinX = pageWidth / 2.0;
                    double pinY = pageHeight / 2.0;

                    // Add a full‑page text shape that will serve as the watermark.
                    // This overload returns a Shape object directly.
                    Shape watermarkShape = page.AddText(
                        pinX,
                        pinY,
                        pageWidth,
                        pageHeight,
                        watermarkText,
                        fontName,
                        fontColor,
                        fontSizeInches);

                    // Apply rotation to the watermark shape.
                    watermarkShape.XForm.Angle.Value = rotationDegrees;

                    // Apply fill foreground transparency to make the watermark semi‑transparent.
                    // 0.0 = fully opaque, 1.0 = fully transparent.
                    watermarkShape.Fill.FillForegndTrans.Value = opacity;
                }

                // Save the modified diagram to the specified output path.
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
            }
        }
        catch (Exception ex)
        {
            // Report any errors that occur during processing.
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}