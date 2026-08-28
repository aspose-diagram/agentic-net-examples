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
                string outputPath = "second_page.pdf";

                // Load the Visio diagram
                using (Diagram diagram = new Diagram(inputPath))
                {
                    // Configure PDF save options to export only the second page (zero‑based index = 1)
                    PdfSaveOptions pdfOptions = new PdfSaveOptions
                    {
                        PageIndex = 1,   // Start from the second page
                        PageCount = 1    // Export only one page
                    };

                    // Save the selected page as PDF
                    diagram.Save(outputPath, pdfOptions);
                }

                Console.WriteLine($"Second page exported to '{outputPath}'.");

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }