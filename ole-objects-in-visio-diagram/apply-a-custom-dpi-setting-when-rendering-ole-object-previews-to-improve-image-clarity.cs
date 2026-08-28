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

            // Load the diagram file
            Diagram diagram = new Diagram("input.vsdx");

            // ---------- Render OLE object previews to images with custom DPI ----------
            // Create image save options for PNG format
            ImageSaveOptions imgOptions = new ImageSaveOptions(SaveFileFormat.Png);
            // Set the desired resolution (dots per inch) for higher clarity
            imgOptions.Resolution = 300f; // 300 DPI

            // Save the diagram (or specific pages) as PNG images using the custom DPI
            diagram.Save("output_page.png", imgOptions);

            // ---------- Optionally, render to PDF with custom DPI ----------
            PdfSaveOptions pdfOptions = new PdfSaveOptions();
            // Apply the same DPI settings for PDF rendering
            pdfOptions.HorizontalResolution = 300;
            pdfOptions.VerticalResolution = 300;

            // Save the diagram as PDF with the higher DPI settings
            diagram.Save("output.pdf", pdfOptions);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
