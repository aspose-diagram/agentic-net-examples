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

            // Path to the source Visio file
            string sourcePath = "input.vsdx";

            // Path for the output PDF file (will contain only the selected page)
            string outputPath = "selected_page.pdf";

            // Load the Visio diagram
            using (Diagram diagram = new Diagram(sourcePath))
            {
                // Zero‑based index of the page you want to export (e.g., first page)
                int pageIndex = 0;

                // Configure PDF save options to export only the specified page
                PdfSaveOptions pdfOptions = new PdfSaveOptions();
                pdfOptions.PageIndex = pageIndex;   // start page
                pdfOptions.PageCount = 1;           // number of pages to export
                pdfOptions.ExportHiddenPage = false; // do not include hidden pages
                pdfOptions.DefaultFont = "Arial";   // fallback font for missing glyphs
                pdfOptions.SaveFormat = SaveFileFormat.Pdf; // explicitly set format

                // Save the selected page as a PDF; vector graphics and text remain vectorized
                diagram.Save(outputPath, pdfOptions);
            }

            Console.WriteLine("Selected page saved as PDF successfully.");

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
