using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
    {
        static void Main(string[] args)
        {
            try
            {

                // Path to the source Visio file (VSD format)
                string inputPath = "input.vsd";

                // Path for the exported PDF/A file
                string outputPath = "output.pdf";

                // Load the Visio diagram
                Diagram diagram = new Diagram(inputPath);

                // Configure PDF save options for PDF/A compliance
                PdfSaveOptions pdfOptions = new PdfSaveOptions
                {
                    // Set PDF/A compliance level (PDF/A-1b)
                    Compliance = PdfCompliance.PdfA1b
                    // Images are embedded by default; lossless compression is used when possible.
                };

                // Save the diagram as a PDF/A compliant document
                diagram.Save(outputPath, pdfOptions);

                Console.WriteLine($"Diagram successfully exported to PDF/A at: {outputPath}");

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }