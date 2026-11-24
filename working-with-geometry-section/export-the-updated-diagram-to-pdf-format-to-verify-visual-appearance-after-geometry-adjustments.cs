using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Validate command‑line arguments.
        if (args.Length < 1)
        {
            Console.Error.WriteLine("Usage: Program <inputVisioFilePath> [outputPdfFilePath]");
            return;
        }

        string inputPath = args[0];

        // Guard: ensure the input Visio file exists.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Determine output PDF path. If not supplied, place it beside the input file.
        string outputPath = args.Length >= 2 ? args[1] : Path.ChangeExtension(inputPath, ".pdf");

        try
        {
            // Load the Visio diagram inside a using block as required.
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Configure PDF save options.
                PdfSaveOptions pdfOptions = new PdfSaveOptions();
                pdfOptions.ExportHiddenPage = false;               // Do not export hidden pages.
                pdfOptions.DefaultFont = "Arial";                  // Fallback font if needed.
                pdfOptions.SaveFormat = SaveFileFormat.Pdf;        // Explicitly set the target format.

                // Save the diagram as PDF.
                diagram.Save(outputPath, pdfOptions);
            }

            Console.WriteLine($"Diagram successfully exported to PDF: {outputPath}");
        }
        catch (Exception ex)
        {
            // Report any errors that occur during loading or saving.
            Console.Error.WriteLine($"Error during PDF export: {ex.Message}");
        }
    }
}