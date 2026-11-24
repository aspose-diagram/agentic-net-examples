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
            // Load the Visio diagram inside a using block to ensure proper disposal
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Configure PDF save options
                PdfSaveOptions pdfOptions = new PdfSaveOptions();

                // Explicitly set the format tracker (required by Aspose)
                pdfOptions.SaveFormat = SaveFileFormat.Pdf;

                // Set a default font to ensure text is rendered correctly
                pdfOptions.DefaultFont = "Arial";

                // Export hidden pages if any (property expects a bool, not BOOL)
                pdfOptions.ExportHiddenPage = true;

                // Save the diagram as a PDF preserving vector shapes and text quality
                diagram.Save(outputPath, pdfOptions);

                Console.WriteLine($"Diagram successfully saved to PDF: {outputPath}");
            }
        }
        catch (Exception ex)
        {
            // Write any errors to the error stream
            Console.Error.WriteLine($"Error during PDF conversion: {ex.Message}");
        }
    }
}
