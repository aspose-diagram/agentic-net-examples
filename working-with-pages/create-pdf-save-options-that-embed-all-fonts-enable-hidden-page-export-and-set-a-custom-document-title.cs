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

        // Guard: ensure the input Visio file exists.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Error: Input file \"{inputPath}\" does not exist.");
            return;
        }

        try
        {
            // Load the Visio diagram inside a using block (mandatory pattern).
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Set a custom document title (will be stored in PDF metadata).
                diagram.DocumentProps.Title = "My Custom PDF Title";

                // Configure PDF save options.
                PdfSaveOptions pdfOptions = new PdfSaveOptions();

                // Explicitly set the format tracker.
                pdfOptions.SaveFormat = SaveFileFormat.Pdf;

                // Enable export of hidden pages.
                pdfOptions.ExportHiddenPage = true; // BOOL enum not required; property expects bool.

                // NOTE: Aspose.Diagram does not provide a direct property to embed all fonts.
                // Font embedding is handled internally by the library and cannot be forced via PdfSaveOptions.

                // Save the diagram as PDF with the configured options.
                diagram.Save(outputPath, pdfOptions);
            }
        }
        catch (Exception ex)
        {
            // Write any errors to the error stream.
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}
