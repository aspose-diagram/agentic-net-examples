using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
    {
        static void Main(string[] args)
        {
            try
            {

                // Path to the source Visio diagram
                string inputPath = "input.vsdx";

                // Load the diagram
                Diagram diagram = new Diagram(inputPath);

                // Configure PDF save options for high‑resolution print quality
                PdfSaveOptions pdfOptions = new PdfSaveOptions();
                // Use a common font as fallback for any missing fonts
                pdfOptions.DefaultFont = "Arial";
                // Do not include hidden pages in the PDF
                pdfOptions.ExportHiddenPage = false;
                // Set PDF/A compliance for reliable printing
                pdfOptions.Compliance = PdfCompliance.PdfA1b;

                // Export the diagram to PDF
                string outputPath = "output.pdf";
                diagram.Save(outputPath, pdfOptions);

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }