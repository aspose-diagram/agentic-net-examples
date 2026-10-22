using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Verify that input and output file paths are provided.
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <inputVisioFile> <outputPdfFile>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

        // Guard: ensure the input Visio file exists.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file does not exist: {inputPath}");
            return;
        }

        // Ensure the output directory exists.
        string? outputDir = Path.GetDirectoryName(outputPath); // Changed to nullable to avoid CS8600 warning
        if (!string.IsNullOrEmpty(outputDir) && !Directory.Exists(outputDir))
        {
            Directory.CreateDirectory(outputDir);
        }

        try
        {
            // Load the diagram inside a using block as required.
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Create PDF save options.
                PdfSaveOptions pdfOptions = new PdfSaveOptions();

                // Explicitly set the format tracker.
                pdfOptions.SaveFormat = SaveFileFormat.Pdf;

                // Do not export hidden pages (use native bool).
                pdfOptions.ExportHiddenPage = false; // Changed from BOOL.False to bool false

                // Save the diagram to PDF using the options.
                diagram.Save(outputPath, pdfOptions);
            }

            Console.WriteLine($"Diagram successfully exported to PDF: {outputPath}");
        }
        catch (Exception ex)
        {
            // Write any errors to the error stream.
            Console.Error.WriteLine($"Error during PDF export: {ex.Message}");
        }
    }
}
