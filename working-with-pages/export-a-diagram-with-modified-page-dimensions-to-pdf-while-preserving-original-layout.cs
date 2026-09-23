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

                // Load the diagram
                Diagram diagram = new Diagram(inputPath);

                // Desired page size in inches (example: A4 size)
                double targetWidthInches = 8.27;
                double targetHeightInches = 11.69;

                // Adjust each page's dimensions while keeping the existing layout unchanged
                foreach (Page page in diagram.Pages)
                {
                    page.PageSheet.PageProps.PageWidth.Value = targetWidthInches;
                    page.PageSheet.PageProps.PageHeight.Value = targetHeightInches;
                }

                // Configure PDF save options
                PdfSaveOptions pdfOptions = new PdfSaveOptions();
                pdfOptions.SaveFormat = SaveFileFormat.Pdf; // Explicitly set the format
                pdfOptions.DefaultFont = "Arial"; // Fallback font if needed

                // Save the diagram as PDF with the modified page size
                diagram.Save(outputPath, pdfOptions);

                Console.WriteLine("Diagram exported to PDF with updated page dimensions.");

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }