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

            // Path to the source Visio diagram (VDX, VSDX, etc.)
            string sourceDiagramPath = "input.vsdx";

            // Path where the resulting PDF will be saved
            string outputPdfPath = "output.pdf";

            // Load the diagram from the file system
            Diagram diagram = new Diagram(sourceDiagramPath);

            // Create PDF save options to control the export
            PdfSaveOptions pdfOptions = new PdfSaveOptions
            {
                // Export all pages (default is MaxValue, set explicitly for clarity)
                PageCount = int.MaxValue,
                // Ensure that both foreground and background pages are included
                SaveForegroundPagesOnly = false,
                // Keep the original page size
                PageSize = null
            };

            // Export the diagram to PDF using the save options
            diagram.Save(outputPdfPath, pdfOptions);

            // Simple inspection: list all page names to verify that every page was processed
            Console.WriteLine("Exported pages:");
            foreach (Page page in diagram.Pages)
            {
                Console.WriteLine($"- Page {page.ID}: {page.Name}");
            }

            // Clean up resources
            diagram.Dispose();

            Console.WriteLine($"Diagram successfully exported to PDF at '{outputPdfPath}'.");

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
