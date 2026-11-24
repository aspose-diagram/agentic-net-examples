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

        // Guard: ensure the input Visio file exists.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Guard: ensure the output directory exists (create if necessary).
        string outputDir = Path.GetDirectoryName(outputPath);
        if (!string.IsNullOrEmpty(outputDir) && !Directory.Exists(outputDir))
        {
            try
            {
                Directory.CreateDirectory(outputDir);
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Failed to create output directory: {ex.Message}");
                return;
            }
        }

        // Load the diagram, modify page dimensions, and export to PDF.
        try
        {
            // Load the Visio diagram inside a using block (mandatory pattern).
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Define new page dimensions (in inches). Adjust as needed.
                double newPageWidth = 11.0;   // Width in inches
                double newPageHeight = 8.5;   // Height in inches (landscape)

                // Iterate over all pages and set the new dimensions.
                foreach (Page page in diagram.Pages)
                {
                    // Page width and height are stored in DoubleValue objects.
                    page.PageSheet.PageProps.PageWidth.Value = newPageWidth;
                    page.PageSheet.PageProps.PageHeight.Value = newPageHeight;
                }

                // Configure PDF save options.
                PdfSaveOptions pdfOptions = new PdfSaveOptions();
                // Explicitly set the format tracker (required by the API).
                pdfOptions.SaveFormat = SaveFileFormat.Pdf;
                // Do not export hidden pages (preserve original visible layout only).
                pdfOptions.ExportHiddenPage = false;
                // Optional: set text compression to reduce file size.
                pdfOptions.TextCompression = PdfTextCompression.Flate;

                // Save the modified diagram as PDF.
                diagram.Save(outputPath, pdfOptions);
            }

            Console.WriteLine($"Diagram successfully exported to PDF: {outputPath}");
        }
        catch (Exception ex)
        {
            // Capture any Aspose.Diagram or I/O errors.
            Console.Error.WriteLine($"Error during processing: {ex.Message}");
        }
    }
}