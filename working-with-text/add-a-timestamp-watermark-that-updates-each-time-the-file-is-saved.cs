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
        // Guard against missing input file
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        Diagram diagram;
        try
        {
            // Load the diagram from the file system
            diagram = new Diagram(inputPath);
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error loading diagram: {ex.Message}");
            return;
        }

        try
        {
            // Add or update the timestamp watermark on the first page
            AddOrUpdateTimestampWatermark(diagram);
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error processing watermark: {ex.Message}");
            return;
        }

        // Path for the output PDF file
        string outputPath = "output.pdf";

        try
        {
            // Configure PDF save options (fallback font specified)
            PdfSaveOptions pdfOptions = new PdfSaveOptions();
            pdfOptions.DefaultFont = "Arial";

            // Save the diagram as PDF using the configured options
            diagram.Save(outputPath, pdfOptions);
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error saving diagram: {ex.Message}");
        }
    }

    static void AddOrUpdateTimestampWatermark(Diagram diagram)
    {
        // Use the first page of the diagram for the watermark
        Page page = diagram.Pages[0];

        // Retrieve page dimensions (in inches)
        double pageWidth = page.PageSheet.PageProps.PageWidth.Value;
        double pageHeight = page.PageSheet.PageProps.PageHeight.Value;

        // Look for an existing watermark shape by its universal name
        Shape watermarkShape = null;
        foreach (Shape shape in page.Shapes)
        {
            if (shape.NameU != null && shape.NameU.Equals("TimestampWatermark", StringComparison.OrdinalIgnoreCase))
            {
                watermarkShape = shape;
                break;
            }
        }

        // Current timestamp string to display
        string timestamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

        if (watermarkShape != null)
        {
            // Update the text of the existing watermark shape
            watermarkShape.Text.Value.Clear();
            watermarkShape.Text.Value.Add(new Txt(timestamp));
        }
        else
        {
            // Create a new full‑page text shape as the watermark
            // PinX/PinY define the lower‑left corner; width/height cover the whole page
            Shape newShape = page.AddText(
                0,                 // pinX
                0,                 // pinY
                pageWidth,         // width
                pageHeight,        // height
                timestamp,         // text
                "Calibri",         // fontName
                "#A0A0A0",         // fontColor (light gray)
                0.25               // fontSize in inches (≈18pt)
            );

            // Assign a recognizable universal name for future updates
            newShape.NameU = "TimestampWatermark";

            // Ensure the text is not rotated
            newShape.TextXForm.TxtAngle.Value = 0;
        }
    }
}