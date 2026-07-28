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
                // Set a high DPI for both horizontal and vertical resolution
                HorizontalResolution = 300,
                VerticalResolution = 300,

                // Ensure that missing fonts are substituted with a reliable fallback
                DefaultFont = "Arial Unicode MS"
            };

            // Save the diagram pages to a PDF file using the configured options
            diagram.Save("output.pdf", pdfOptions);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
