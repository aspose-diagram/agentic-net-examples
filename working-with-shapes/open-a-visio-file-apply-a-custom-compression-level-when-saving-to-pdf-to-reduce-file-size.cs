using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Expect two arguments: input Visio file path and output PDF file path.
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <inputVisioPath> <outputPdfPath>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

        // Guard: verify input file exists.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file does not exist: {inputPath}");
            return;
        }

        try
        {
            // Load the Visio diagram.
            Diagram diagram = new Diagram(inputPath);

            // Configure PDF save options with custom compression.
            PdfSaveOptions pdfOptions = new PdfSaveOptions();

            // Explicitly set the target format.
            pdfOptions.SaveFormat = SaveFileFormat.Pdf;

            // Apply text compression (Flate) to reduce file size.
            pdfOptions.TextCompression = PdfTextCompression.Flate;

            // Save the diagram as PDF using the configured options.
            diagram.Save(outputPath, pdfOptions);

            Console.WriteLine($"Diagram saved to PDF successfully: {outputPath}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during PDF conversion: {ex.Message}");
        }
    }
}