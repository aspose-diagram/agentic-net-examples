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

        // Load the Visio diagram and save it as PDF with the required options.
        try
        {
            // Load the diagram from the specified file.
            Diagram diagram = new Diagram(inputPath);

            // Configure PDF save options.
            PdfSaveOptions pdfOptions = new PdfSaveOptions();

            // Enable export of hidden pages.
            pdfOptions.ExportHiddenPage = true;

            // Set the format tracker explicitly (required by Aspose.Diagram).
            pdfOptions.SaveFormat = SaveFileFormat.Pdf;

            // Apply text compression to reduce file size (optional but useful).
            pdfOptions.TextCompression = PdfTextCompression.Flate;

            // NOTE: Aspose.Diagram's PdfSaveOptions does not provide a property to embed all fonts.
            // Font embedding is handled automatically by the library where possible.

            // Save the diagram as PDF using the configured options.
            diagram.Save(outputPath, pdfOptions);
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during diagram loading or PDF export: {ex.Message}");
            return;
        }

        // After the PDF is created, set a custom document title using Aspose.Pdf.
        try
        {
            // Load the generated PDF (fully qualified type to avoid namespace ambiguity).
            Aspose.Pdf.Document pdfDoc = new Aspose.Pdf.Document(outputPath);

            // Set the custom title in the PDF metadata.
            pdfDoc.Info.Title = "Custom Document Title";

            // Overwrite the existing PDF with the updated metadata.
            pdfDoc.Save(outputPath);
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error setting PDF title: {ex.Message}");
            return;
        }

        Console.WriteLine("PDF saved successfully with hidden pages exported and custom title applied.");
    }
}