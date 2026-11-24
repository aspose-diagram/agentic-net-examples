using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Validate arguments
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <inputVisioPath> <outputPdfPath>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

        // Guard: check input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file does not exist: {inputPath}");
            return;
        }

        try
        {
            // Load the Visio diagram
            Diagram diagram = new Diagram(inputPath);

            // Configure PDF save options with compression settings
            PdfSaveOptions pdfOptions = new PdfSaveOptions();

            // Reduce image quality and resolution to lower PDF size
            pdfOptions.JpegQuality = 75;               // JPEG quality (0-100)
            pdfOptions.HorizontalResolution = 150;    // DPI
            pdfOptions.VerticalResolution = 150;      // DPI

            // Do not export hidden pages
            pdfOptions.ExportHiddenPage = false;

            // Save the diagram as PDF with the specified options
            diagram.Save(outputPath, pdfOptions);
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error processing file: {ex.Message}");
        }
    }
}