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

            // Path to the source Visio file (multi‑page diagram)
            string sourcePath = "input.vsdx";

            // Load the diagram
            Diagram diagram = new Diagram(sourcePath);

            // Configure PDF save options to export only the second page (index 1)
            PdfSaveOptions pdfOptions = new PdfSaveOptions
            {
                // 0‑based page index; 1 selects the second page
                PageIndex = 1,
                // Export only one page starting from PageIndex
                PageCount = 1,
                // Optional: do not include hidden pages
                ExportHiddenPage = false,
                // Optional: set a default font to avoid missing‑font issues
                DefaultFont = "Arial"
            };

            // Save the selected page as PDF
            string outputPath = "second_page.pdf";
            diagram.Save(outputPath, pdfOptions);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
