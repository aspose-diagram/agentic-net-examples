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

            // Load the Visio diagram from file
            Diagram diagram = new Diagram("input.vsdx");

            // Set up PDF save options (default behavior retains original colors)
            PdfSaveOptions pdfOptions = new PdfSaveOptions
            {
                // Optional: do not export hidden pages or guide shapes
                ExportHiddenPage = false,
                ExportGuideShapes = false
            };

            // Export the diagram to PDF
            diagram.Save("output.pdf", pdfOptions);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
