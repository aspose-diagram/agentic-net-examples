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

                // Desired page size (A4 in inches)
                double newPageWidth = 8.27;   // Width in inches
                double newPageHeight = 11.69; // Height in inches

                // Load the diagram
                using (Diagram diagram = new Diagram(inputPath))
                {
                    // Update dimensions for each page while keeping existing layout
                    foreach (Page page in diagram.Pages)
                    {
                        page.PageSheet.PageProps.PageWidth.Value = newPageWidth;
                        page.PageSheet.PageProps.PageHeight.Value = newPageHeight;
                    }

                    // Configure PDF save options
                    PdfSaveOptions pdfOptions = new PdfSaveOptions
                    {
                        // Ensure the format is explicitly set
                        SaveFormat = SaveFileFormat.Pdf,
                        // Use a fallback font to avoid missing glyphs
                        DefaultFont = "Arial",
                        // Do not export hidden pages (optional)
                        ExportHiddenPage = false
                    };

                    // Save the modified diagram as PDF
                    diagram.Save(outputPath, pdfOptions);
                }

                Console.WriteLine("Diagram exported to PDF successfully.");

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }