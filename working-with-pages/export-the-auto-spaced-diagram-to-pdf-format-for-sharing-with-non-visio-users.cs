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

            // Load the Visio diagram (auto‑spaced) from a file
            // Diagram(string) constructor loads the diagram from the specified path
            var diagram = new Diagram("AutoSpacedDiagram.vsdx");

            // Create PDF save options – you can customize options here if needed
            var pdfOptions = new PdfSaveOptions
            {
                // Example: export all pages (default), you can set PageCount, PageIndex, etc.
                // PageCount = 1,
                // PageIndex = 0,
                // EnlargePage = true,
            };

            // Save the diagram as PDF using the Save(string, SaveOptions) overload
            diagram.Save("AutoSpacedDiagram.pdf", pdfOptions);

            // Dispose the diagram object to release resources
            diagram.Dispose();

            Console.WriteLine("Diagram exported to PDF successfully.");

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
