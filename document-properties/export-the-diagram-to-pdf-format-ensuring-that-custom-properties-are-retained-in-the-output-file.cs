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

            // Load an existing Visio diagram (replace with your source file)
            Diagram diagram = new Diagram("input.vsdx");

            // Configure PDF save options.
            // Custom document properties are preserved automatically.
            // Additional options can be set here if needed.
            PdfSaveOptions pdfOptions = new PdfSaveOptions
            {
                // Example: include hidden pages so that any hidden custom data is kept.
                ExportHiddenPage = true
            };

            // Export the diagram to PDF using the configured options.
            diagram.Save("output.pdf", pdfOptions);

            // Clean up resources.
            diagram.Dispose();

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
