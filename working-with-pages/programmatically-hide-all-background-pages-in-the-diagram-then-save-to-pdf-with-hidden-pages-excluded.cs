using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main()
    {
        try
        {

            // Load the diagram from a file
            Diagram diagram = new Diagram("input.vsdx");

            // Configure PDF save options:
            // - Do not export hidden pages
            // - Render only foreground pages (background pages are excluded)
            PdfSaveOptions pdfOptions = new PdfSaveOptions
            {
                ExportHiddenPage = false,
                SaveForegroundPagesOnly = true
            };

            // Save the diagram to PDF using the configured options
            diagram.Save("output.pdf", pdfOptions);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
