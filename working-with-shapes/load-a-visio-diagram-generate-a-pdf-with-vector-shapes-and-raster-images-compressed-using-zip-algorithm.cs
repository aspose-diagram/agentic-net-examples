using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
    {
        static void Main(string[] args)
        {
            try
            {

                // Input Visio file path (modify as needed)
                string inputPath = "input.vsdx";

                // Output PDF file path
                string outputPath = "output.pdf";

                // Load the Visio diagram
                Diagram diagram = new Diagram(inputPath);

                // Configure PDF save options with ZIP (Flate) text compression
                PdfSaveOptions pdfOptions = new PdfSaveOptions();
                pdfOptions.DefaultFont = "Arial";                     // Fallback font
                pdfOptions.TextCompression = PdfTextCompression.Flate; // ZIP/Flate compression for PDF streams
                pdfOptions.SaveFormat = SaveFileFormat.Pdf;           // Explicitly set the format

                // Save the diagram as PDF with the specified options
                diagram.Save(outputPath, pdfOptions);

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }