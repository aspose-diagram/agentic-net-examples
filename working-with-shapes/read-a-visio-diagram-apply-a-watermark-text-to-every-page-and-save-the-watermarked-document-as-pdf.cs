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
            Console.Error.WriteLine("Usage: Program <inputVisioPath> <outputPdfPath>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

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

            // Iterate over all pages and add a watermark.
            foreach (Page page in diagram.Pages)
            {
                // Retrieve page dimensions.
                double pageWidth = page.PageSheet.PageProps.PageWidth.Value;
                double pageHeight = page.PageSheet.PageProps.PageHeight.Value;

                // Position the watermark at the centre of the page.
                double pinX = pageWidth / 2.0;
                double pinY = pageHeight / 2.0;

                // Add watermark text that spans the whole page.
                // Font color is a light gray; font size is modest.
                page.AddText(pinX, pinY, pageWidth, pageHeight,
                             "Watermark", "Calibri", "#a5a5a5", 0.25);
            }

            // Configure PDF save options.
            PdfSaveOptions pdfOptions = new PdfSaveOptions();
            pdfOptions.SaveFormat = SaveFileFormat.Pdf;
            // Optional: enable text compression to reduce file size.
            pdfOptions.TextCompression = PdfTextCompression.Flate;

            // Save the watermarked diagram as PDF.
            diagram.Save(outputPath, pdfOptions);
        }
        catch (Exception ex)
        {
            // Write any Aspose or I/O errors to the error stream.
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}