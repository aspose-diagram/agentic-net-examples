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

            // Load the Visio diagram from a file
            Diagram diagram = new Diagram("input.vsdx");

            // Configure PDF save options for high‑resolution, print‑ready output
            PdfSaveOptions pdfOptions = new PdfSaveOptions
            {
                // Set DPI to 300 for both horizontal and vertical resolution
                HorizontalResolution = 300,
                VerticalResolution = 300,

                // Ensure all fonts are embedded; set a fallback font for missing glyphs
                DefaultFont = "Arial",

                // Export all pages (including background) for full document fidelity
                ExportHiddenPage = false,
                SaveForegroundPagesOnly = false
            };

            // Save the diagram as a PDF using the configured options
            diagram.Save("output.pdf", pdfOptions);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
