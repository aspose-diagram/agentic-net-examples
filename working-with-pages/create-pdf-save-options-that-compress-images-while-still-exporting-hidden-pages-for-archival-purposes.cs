using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
    {
        static void Main()
        {
            try
            {

                // Load an existing Visio diagram.
                // Replace "input.vsdx" with the actual file path.
                Diagram diagram = new Diagram("input.vsdx");

                // Configure PDF save options.
                PdfSaveOptions pdfOptions = new PdfSaveOptions();

                // Export hidden pages (required for archival purposes).
                pdfOptions.ExportHiddenPage = true;

                // Compress images by reducing JPEG quality.
                // This affects JPEG images embedded in the diagram.
                pdfOptions.JpegQuality = 80; // Value between 0 (worst) and 100 (best).

                // Compress text streams using Flate compression.
                pdfOptions.TextCompression = PdfTextCompression.Flate;

                // Set a default font to avoid missing‑font issues.
                pdfOptions.DefaultFont = "Arial";

                // Save the diagram as a PDF with the configured options.
                // Replace "output.pdf" with the desired output path.
                diagram.Save("output.pdf", pdfOptions);

                // Clean up resources.
                diagram.Dispose();

                Console.WriteLine("PDF export completed successfully.");

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }