using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
    {
        static void Main(string[] args)
        {
            try
            {

                // Input Visio file path
                string inputPath = "input.vsdx";
                // Output PDF file path
                string outputPath = "output.pdf";

                try
                {
                    // Load the Visio diagram
                    Diagram diagram = new Diagram(inputPath);

                    // Configure PDF save options
                    PdfSaveOptions pdfOptions = new PdfSaveOptions();
                    // Ensure the format is set explicitly
                    pdfOptions.SaveFormat = SaveFileFormat.Pdf;
                    // Use ZIP (Flate) compression for text streams in the PDF
                    pdfOptions.TextCompression = PdfTextCompression.Flate;

                    // Save the diagram as PDF with the specified options
                    diagram.Save(outputPath, pdfOptions);

                    Console.WriteLine("Diagram successfully exported to PDF: " + outputPath);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error during export: " + ex.Message);
                    throw;
                }

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }