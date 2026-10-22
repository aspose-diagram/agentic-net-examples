using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Validate command‑line arguments
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <inputVisioFile> <outputPdfFile>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

        // Guard: ensure the input Visio file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the Visio diagram from the specified file
            Diagram diagram = new Diagram(inputPath);

            // Configure each page for print‑quality PDF export
            foreach (Page page in diagram.Pages)
            {
                // Enable fit‑to‑page printing
                page.PageSheet.PrintProps.OnPage.Value = BOOL.True;

                // Set scaling to 100 % (no reduction)
                page.PageSheet.PrintProps.ScaleX.Value = 1.0;
                page.PageSheet.PrintProps.ScaleY.Value = 1.0;

                // Use small margins to maximize printable area (values are in inches)
                page.PageSheet.PrintProps.PageLeftMargin.Value = 0.1;
                page.PageSheet.PrintProps.PageRightMargin.Value = 0.1;
                page.PageSheet.PrintProps.PageTopMargin.Value = 0.1;
                page.PageSheet.PrintProps.PageBottomMargin.Value = 0.1;
            }

            // Prepare PDF save options
            PdfSaveOptions pdfOptions = new PdfSaveOptions();

            // Do not export hidden pages (bool, not BOOL)
            pdfOptions.ExportHiddenPage = false;

            // Fallback font if a font is missing in the source diagram
            pdfOptions.DefaultFont = "Arial";

            // Explicitly set the format tracker (required by the API)
            pdfOptions.SaveFormat = SaveFileFormat.Pdf;

            // Save the diagram as a high‑resolution PDF
            diagram.Save(outputPath, pdfOptions);

            Console.WriteLine($"Diagram successfully exported to PDF: {outputPath}");
        }
        catch (Exception ex)
        {
            // Write any errors to the error stream
            Console.Error.WriteLine($"Error during PDF export: {ex.Message}");
        }
    }
}