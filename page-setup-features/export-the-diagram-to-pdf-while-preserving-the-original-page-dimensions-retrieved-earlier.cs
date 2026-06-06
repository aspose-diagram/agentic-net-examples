using System.IO;
using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class ExportDiagramToPdf
{
    static void Main()
    {
        try
        {

            // Load the Visio diagram (replace with your actual file path)
            Diagram diagram = new Diagram(@"C:\Input\sample.vsdx");

            // Create PDF save options to keep original page dimensions
            PdfSaveOptions pdfOptions = new PdfSaveOptions();

            // Null PageSize tells Aspose to use the source diagram's page size
            pdfOptions.PageSize = null;

            // Do not enlarge the page; keep the original dimensions
            pdfOptions.EnlargePage = false;

            // Save the diagram as PDF using the configured options
            diagram.Save(@"C:\Output\sample.pdf", pdfOptions);

        }
        catch (System.IO.DirectoryNotFoundException ex)
        {
            Console.Error.WriteLine($"[DirectoryNotFoundException] {ex.Message}");
        }
    }
}
