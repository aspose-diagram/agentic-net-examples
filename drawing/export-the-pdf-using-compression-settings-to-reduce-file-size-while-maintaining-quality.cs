using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Expect input Visio file path and output PDF file path as command‑line arguments.
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <inputVisioPath> <outputPdfPath>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

        // Guard: ensure the input Visio file exists.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the Visio diagram inside a using block as required.
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Configure PDF save options with compression to reduce file size.
                PdfSaveOptions pdfOptions = new PdfSaveOptions();

                // Apply Flate text compression (ZIP‑based) for smaller PDFs while keeping quality.
                pdfOptions.TextCompression = PdfTextCompression.Flate;

                // Explicitly set the target format.
                pdfOptions.SaveFormat = SaveFileFormat.Pdf;

                // Save the diagram as a PDF using the configured options.
                diagram.Save(outputPath, pdfOptions);

                Console.WriteLine($"PDF exported successfully to: {outputPath}");
            }
        }
        catch (Exception ex)
        {
            // Write any errors to the error stream.
            Console.Error.WriteLine($"Error during PDF export: {ex.Message}");
        }
    }
}