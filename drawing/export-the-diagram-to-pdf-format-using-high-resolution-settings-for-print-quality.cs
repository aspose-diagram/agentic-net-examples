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
            Console.Error.WriteLine("Usage: Program <inputVisioFile> <outputPdfFile>");
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

            // Configure PDF save options for high‑resolution print quality.
            PdfSaveOptions pdfOptions = new PdfSaveOptions();

            // Explicitly set the save format to PDF.
            pdfOptions.SaveFormat = SaveFileFormat.Pdf;

            // Use a common font as fallback.
            pdfOptions.DefaultFont = "Arial";

            // Exclude hidden pages from the export.
            pdfOptions.ExportHiddenPage = false; // changed from BOOL.False to bool

            // Set PDF/A compliance for print‑ready output.
            pdfOptions.Compliance = PdfCompliance.PdfA1b; // corrected property name

            // Save the diagram as PDF with the configured options.
            diagram.Save(outputPath, pdfOptions);

            Console.WriteLine($"Diagram successfully exported to PDF: {outputPath}");
        }
        catch (Exception ex)
        {
            // Write any errors to the error stream.
            Console.Error.WriteLine($"Error during PDF export: {ex.Message}");
        }
    }
}
