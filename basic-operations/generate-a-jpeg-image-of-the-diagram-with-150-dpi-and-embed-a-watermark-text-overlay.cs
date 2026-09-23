using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Path to the source Visio diagram
        string inputPath = "input.vsdx";
        // Guard: ensure the input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Path for the output JPEG image
        string outputPath = "output.jpg";

        try
        {
            // Load the diagram from the specified file
            Diagram diagram = new Diagram(inputPath);

            // Get the first page (assumes at least one page exists)
            Page page = diagram.Pages[0];

            // Retrieve page dimensions (in inches) for full‑page watermark sizing
            double pageWidth = page.PageSheet.PageProps.PageWidth.Value;
            double pageHeight = page.PageSheet.PageProps.PageHeight.Value;

            // Add a full‑page text shape to act as a watermark
            // Note: using positional arguments because named parameters are not supported for this overload
            Shape watermark = page.AddText(
                0,                 // pinX (left)
                0,                 // pinY (bottom)
                pageWidth,         // width (full page)
                pageHeight,        // height (full page)
                "CONFIDENTIAL",    // watermark text
                "Arial",           // font name
                "#CCCCCC",         // font color (hex)
                36.0 / 72.0        // font size in inches (36 pt)
            );

            // Send the watermark shape to the back so it doesn't obscure other content
            watermark.SendToBack();

            // Configure image export options: JPEG format with 150 DPI resolution
            ImageSaveOptions saveOptions = new ImageSaveOptions(SaveFileFormat.Jpeg);
            saveOptions.Resolution = 150f; // DPI

            // Export the diagram page as a JPEG image using the configured options
            diagram.Save(outputPath, saveOptions);

            Console.WriteLine($"Diagram exported to JPEG with watermark at '{outputPath}'.");
        }
        catch (Exception ex)
        {
            // Write any Aspose or I/O errors to the error stream
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}