using System.IO;
using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main()
    {
        try
        {

            // Input Visio file path
            string inputPath = "input.vsdx";
            // Output PDF file path
            string outputPath = "output.pdf";

            // Load the diagram
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Ensure all pages are visible in the UI
                foreach (Page page in diagram.Pages)
                {
                    // Set UI visibility to Visible for each page
                    page.PageSheet.PageProps.UIVisibility.Value = UIVisibilityValue.Visible;
                }

                // Configure PDF save options to exclude hidden pages
                PdfSaveOptions pdfOptions = new PdfSaveOptions
                {
                    ExportHiddenPage = false,
                    // Optional: set a default font to avoid missing glyphs
                    DefaultFont = "Arial"
                };

                // Save the diagram as PDF
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
