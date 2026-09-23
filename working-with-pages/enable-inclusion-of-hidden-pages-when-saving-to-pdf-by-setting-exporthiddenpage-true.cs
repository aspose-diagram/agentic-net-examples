using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
    {
        static void Main(string[] args)
        {
            try
            {

                // Input Visio file path (replace with actual file location)
                string inputPath = "input.vsdx";

                // Output PDF file path
                string outputPath = "output.pdf";

                // Load the diagram from file
                using (Diagram diagram = new Diagram(inputPath))
                {
                    // Configure PDF save options to include hidden pages
                    PdfSaveOptions pdfOptions = new PdfSaveOptions();
                    pdfOptions.ExportHiddenPage = true;

                    // Save the diagram as PDF with the specified options
                    diagram.Save(outputPath, pdfOptions);
                }

                Console.WriteLine("Diagram saved to PDF with hidden pages included.");

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }