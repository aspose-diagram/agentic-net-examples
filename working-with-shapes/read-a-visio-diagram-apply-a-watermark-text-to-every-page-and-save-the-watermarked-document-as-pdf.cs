using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Define input Visio file and output PDF file paths
        string inputPath = "input.vsdx";
        if (!System.IO.File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }
        string outputPath = "output.pdf";

        // Guard: ensure the input file exists before proceeding
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the Visio diagram from the specified file
            Diagram diagram = new Diagram(inputPath);

            // Define the watermark text to be applied to each page
            string watermarkText = "CONFIDENTIAL";

            // Iterate over all pages in the diagram and add the watermark
            foreach (Page page in diagram.Pages)
            {
                // Retrieve the page dimensions (width and height)
                double pageWidth = page.PageSheet.PageProps.PageWidth.Value;
                double pageHeight = page.PageSheet.PageProps.PageHeight.Value;

                // Add watermark text covering the full page area.
                // pinX and pinY are set to 0 to start at the top‑left corner.
                // Font name "Calibri", light gray color "#a5a5a5", and size 36 are used.
                page.AddText(0, 0, pageWidth, pageHeight, watermarkText, "Calibri", "#a5a5a5", 36);
            }

            // Configure PDF save options
            PdfSaveOptions pdfOptions = new PdfSaveOptions();
            // Explicitly set the format tracker
            pdfOptions.SaveFormat = SaveFileFormat.Pdf;
            // Apply flate compression to reduce PDF size
            pdfOptions.TextCompression = PdfTextCompression.Flate;
            // Optional: set high resolution for raster images embedded in the PDF
            pdfOptions.HorizontalResolution = 300;
            pdfOptions.VerticalResolution = 300;
            pdfOptions.JpegQuality = 100;

            // Save the watermarked diagram as a PDF file using the configured options
            diagram.Save(outputPath, pdfOptions);
        }
        catch (Exception ex)
        {
            // Write any errors that occur during processing to the error console
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}