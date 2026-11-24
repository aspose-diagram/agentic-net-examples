using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Input Visio file path (modify as needed)
        string inputPath = "input.vsdx";
        if (!System.IO.File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }
        // Output PDF file path
        string outputPath = "output.pdf";

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

            // Set PDF/A-1b conformance level
            pdfOptions.Compliance = PdfCompliance.PdfA1b;

            // Note: Aspose.Diagram's PdfSaveOptions does not provide an explicit
            // property to embed all fonts. Font embedding is handled automatically
            // based on the selected compliance level and available fonts.

            // Optionally set a default fallback font
            pdfOptions.DefaultFont = "Arial";

            // Save the diagram as PDF with the configured options
            diagram.Save(outputPath, pdfOptions);
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}