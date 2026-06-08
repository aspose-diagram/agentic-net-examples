using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
    {
        static void Main(string[] args)
        {
            try
            {

                // Input Visio file path (replace with actual path)
                string inputPath = "input.vsdx";

                // Output PDF file path
                string outputPath = "output.pdf";

                // Load the Visio diagram
                Diagram diagram = new Diagram(inputPath);

                // Configure PDF save options
                PdfSaveOptions pdfOptions = new PdfSaveOptions();

                // Set a default font to avoid missing font issues
                pdfOptions.DefaultFont = "Arial";

                // NOTE: Aspose.Diagram's PdfSaveOptions does not provide a direct
                // property to force grayscale rendering. Grayscale conversion must be
                // handled externally (e.g., post‑processing the PDF) or by using a
                // different library that supports such a feature.

                // Save the diagram as PDF
                diagram.Save(outputPath, pdfOptions);

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }