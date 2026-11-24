using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Expect input Visio file path and output PDF file path as arguments.
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <inputVisioPath> <outputPdfPath>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

        // Guard against missing input file.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the Visio diagram inside a using block.
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Create PDF save options with default settings.
                PdfSaveOptions pdfOptions = new PdfSaveOptions();

                // Explicitly set the format tracking flag.
                pdfOptions.SaveFormat = SaveFileFormat.Pdf;

                // Optional: set a default font for fallback.
                pdfOptions.DefaultFont = "Arial";

                // Save the diagram as a PDF document.
                diagram.Save(outputPath, pdfOptions);

                Console.WriteLine($"Diagram successfully saved to PDF: {outputPath}");
            }
        }
        catch (Exception ex)
        {
            // Write any errors to the error console.
            Console.Error.WriteLine($"Error during PDF export: {ex.Message}");
        }
    }
}