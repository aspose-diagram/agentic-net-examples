using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
    {
        static void Main()
        {
            try
            {

                // Path to the source Visio file
                string inputPath = "input.vsdx";

                // Path for the resulting PDF file
                string outputPath = "output.pdf";

                // Load the Visio diagram
                Diagram diagram = new Diagram(inputPath);

                // Set the footer text to the current date (e.g., "2026-08-26")
                diagram.HeaderFooter.FooterRight = DateTime.Now.ToString("yyyy-MM-dd");

                // Optional: adjust footer margin (in inches) if needed
                // diagram.HeaderFooter.FooterMargin.Value = 0.2;

                // Configure PDF save options (set a default font to avoid missing glyphs)
                PdfSaveOptions pdfOptions = new PdfSaveOptions();
                pdfOptions.DefaultFont = "Arial";

                // Save the diagram as PDF
                diagram.Save(outputPath, pdfOptions);

                Console.WriteLine("Diagram saved as PDF with footer date.");

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }