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
            // Load the Visio diagram inside a using block as required.
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Define new page dimensions (in inches). Adjust as needed.
                double newPageWidth = 11.0;   // example: 11 inches
                double newPageHeight = 8.5;   // example: 8.5 inches (landscape)

                // Iterate through all pages and set the new dimensions.
                foreach (Page page in diagram.Pages)
                {
                    // Modify page width and height while keeping existing shapes unchanged.
                    page.PageSheet.PageProps.PageWidth.Value = newPageWidth;
                    page.PageSheet.PageProps.PageHeight.Value = newPageHeight;
                }

                // Configure PDF save options.
                PdfSaveOptions pdfOptions = new PdfSaveOptions();
                // Explicitly set the format tracker.
                pdfOptions.SaveFormat = SaveFileFormat.Pdf;
                // Example: do not export hidden pages.
                pdfOptions.ExportHiddenPage = false;

                // Save the modified diagram as PDF.
                diagram.Save(outputPath, pdfOptions);
            }

            Console.WriteLine($"Diagram exported successfully to PDF: {outputPath}");
        }
        catch (Exception ex)
        {
            // Write any Aspose or I/O errors to the error stream.
            Console.Error.WriteLine($"Error during processing: {ex.Message}");
        }
    }
}