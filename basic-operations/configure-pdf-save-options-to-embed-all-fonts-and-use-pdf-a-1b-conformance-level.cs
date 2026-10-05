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

            // Set a default fallback font (Aspose.Diagram does not support explicit font embedding)
            pdfOptions.DefaultFont = "Arial";

            // Note: Embedding all fonts is not supported by Aspose.Diagram's PdfSaveOptions.
            // The library will embed fonts automatically when possible.

            // Save the diagram as PDF with the configured options
            diagram.Save(outputPath, pdfOptions);

            Console.WriteLine($"Diagram successfully saved to PDF: {outputPath}");
        }
        catch (Exception ex)
        {
            // Write any errors to the error console
            Console.Error.WriteLine($"Error during PDF export: {ex.Message}");
        }
    }
}