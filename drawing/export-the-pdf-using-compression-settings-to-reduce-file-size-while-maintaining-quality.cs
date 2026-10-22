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
            // Use Flate compression for text streams.
            pdfOptions.TextCompression = PdfTextCompression.Flate;
            // Explicitly set the target format.
            pdfOptions.SaveFormat = SaveFileFormat.Pdf;

            // Save the diagram as a compressed PDF.
            diagram.Save(outputPath, pdfOptions);

            // Release resources.
            diagram.Dispose();

            Console.WriteLine($"PDF exported successfully to: {outputPath}");
        }
        catch (Exception ex)
        {
            // Write any errors to the error stream.
            Console.Error.WriteLine($"Error during PDF export: {ex.Message}");
        }
    }
}