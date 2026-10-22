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
            // Load the Visio diagram.
            Diagram diagram = new Diagram(inputPath);

            // Configure PDF save options with compression to reduce file size.
            PdfSaveOptions pdfOptions = new PdfSaveOptions();

            // Explicitly set the format tracker.
            pdfOptions.SaveFormat = SaveFileFormat.Pdf;

            // Apply flate (ZIP) compression to text streams inside the PDF.
            pdfOptions.TextCompression = PdfTextCompression.Flate;

            // Optional: set a default font to avoid missing‑font warnings.
            pdfOptions.DefaultFont = "Arial";

            // Save the diagram as a PDF using the configured options.
            diagram.Save(outputPath, pdfOptions);
        }
        catch (Exception ex)
        {
            // Write any errors to the error stream.
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}