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

        // Guard: ensure the input file exists.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the Visio diagram.
            Diagram diagram = new Diagram(inputPath);

            // Initialize PDF save options.
            PdfSaveOptions pdfOptions = new PdfSaveOptions();

            // Enable inclusion of hidden pages in the exported PDF.
            pdfOptions.ExportHiddenPage = true;

            // Explicitly set the save format for the options.
            pdfOptions.SaveFormat = SaveFileFormat.Pdf;

            // Save the diagram as PDF using the configured options.
            diagram.Save(outputPath, pdfOptions);

            Console.WriteLine($"Diagram saved successfully to PDF: {outputPath}");
        }
        catch (Exception ex)
        {
            // Write any errors that occur during processing.
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}