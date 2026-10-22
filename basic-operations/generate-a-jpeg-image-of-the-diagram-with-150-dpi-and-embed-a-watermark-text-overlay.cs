using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Validate arguments: input diagram, output JPEG, optional watermark text.
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <inputDiagramPath> <outputJpegPath> [watermarkText]");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];
        string watermarkText = args.Length >= 3 ? args[2] : "Sample Watermark";

        // Guard against missing input file.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the Visio diagram.
            Diagram diagram = new Diagram(inputPath);

            // Ensure there is at least one page.
            if (diagram.Pages.Count == 0)
            {
                Console.Error.WriteLine("The diagram contains no pages.");
                return;
            }

            // Work with the first page.
            Page page = diagram.Pages[0];

            // Retrieve page dimensions (in inches).
            double pageWidth = page.PageSheet.PageProps.PageWidth.Value;
            double pageHeight = page.PageSheet.PageProps.PageHeight.Value;

            // Add a full‑page text shape as a watermark.
            // Font size is specified in inches (points / 72).
            double fontSizeInches = 36.0 / 72.0; // 36 points.
            Shape watermarkShape = page.AddText(
                0,                     // pinX (left)
                0,                     // pinY (bottom)
                pageWidth,             // width
                pageHeight,            // height
                watermarkText,         // text
                "Calibri",             // font name
                "#A0A0A0",             // font color (light gray)
                fontSizeInches);       // font size in inches

            // Optionally send the watermark to back so it appears behind other shapes.
            // watermarkShape.SendToBack();

            // Configure image save options for JPEG with 150 DPI.
            ImageSaveOptions saveOptions = new ImageSaveOptions(SaveFileFormat.Jpeg);
            saveOptions.Resolution = 150f;          // DPI.
            saveOptions.JpegQuality = 90;           // Quality (0‑100).
            saveOptions.ExportHiddenPage = false;   // Do not export hidden pages. (Corrected: use bool, not BOOL)

            // Save the diagram as a JPEG image.
            diagram.Save(outputPath, saveOptions);
            Console.WriteLine($"Diagram saved as JPEG with watermark to: {outputPath}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}
