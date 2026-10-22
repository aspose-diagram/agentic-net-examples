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
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Output PDF file path
        string outputPath = "output.pdf";

        try
        {
            // Load the Visio diagram
            Diagram diagram = new Diagram(inputPath);

            // Configure PDF save options
            PdfSaveOptions pdfOptions = new PdfSaveOptions();

            // Set a default fallback font (Aspose.Diagram does not support explicit font embedding)
            pdfOptions.DefaultFont = "Arial";

            // Export hidden pages as well
            pdfOptions.ExportHiddenPage = true;

            // Optional: set PDF/A compliance
            pdfOptions.Compliance = PdfCompliance.PdfA1b;

            // Save the diagram as PDF with the configured options
            diagram.Save(outputPath, pdfOptions);

            Console.WriteLine($"PDF saved successfully to: {outputPath}");
        }
        catch (Exception ex)
        {
            // Log any errors that occur during processing
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
