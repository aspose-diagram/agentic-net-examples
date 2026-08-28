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
                    // Load the diagram from file
                    Diagram diagram = new Diagram(inputPath);

                    // Define new page dimensions (in inches)
                    // Example: set to A4 size (8.27 x 11.69 inches)
                    double newPageWidth = 8.27;
                    double newPageHeight = 11.69;

                    // Apply new dimensions to each page while preserving layout
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
                        // Optional: set a default font to avoid missing glyphs
                        DefaultFont = "Arial",
                        // Export hidden pages if needed (false to exclude)
                        ExportHiddenPage = false
                    };

                    // Save the modified diagram as PDF
                    diagram.Save(outputPath, pdfOptions);

                    Console.WriteLine("Diagram exported to PDF successfully.");
                }
                catch (Exception ex)
                {
                    // Report any errors that occur during processing
                    Console.WriteLine($"Error: {ex.Message}");
                    throw;
                }

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }