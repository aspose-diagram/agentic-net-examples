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

        try
        {
            // Load the Visio diagram
            Diagram diagram = new Diagram(inputPath);

            // Configure PDF save options
            PdfSaveOptions pdfOptions = new PdfSaveOptions();
            // Set the format tracker explicitly
            pdfOptions.SaveFormat = SaveFileFormat.Pdf;
            // Export hidden pages? false to keep only visible pages (does not affect shape colors)
            pdfOptions.ExportHiddenPage = false;
            // Optional: set a default font to avoid missing font warnings
            pdfOptions.DefaultFont = "Arial";

            // Save the diagram as PDF with the specified options
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