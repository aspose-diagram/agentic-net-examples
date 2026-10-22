using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Input Visio file path (first argument or default)
        string inputPath = args.Length > 0 ? args[0] : "input.vsdx";
        // Output PDF file path (second argument or default)
        string outputPath = args.Length > 1 ? args[1] : "output.pdf";

        // Guard: ensure the input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the diagram inside a using block as required
        using (Diagram diagram = new Diagram(inputPath))
        {
            try
            {
                // Configure PDF save options
                PdfSaveOptions pdfOptions = new PdfSaveOptions();
                // Do not export hidden pages (property expects a bool, not BOOL)
                pdfOptions.ExportHiddenPage = false; // corrected from BOOL.False
                // Set a default font for any missing fonts
                pdfOptions.DefaultFont = "Arial";
                // Explicitly set the target format
                pdfOptions.SaveFormat = SaveFileFormat.Pdf;

                // Save the diagram as PDF
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
}
