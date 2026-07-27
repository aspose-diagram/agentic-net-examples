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

            // Load the Visio diagram from file
            Diagram diagram = new Diagram("input.vsdx");

            // Set up PDF save options with high resolution (e.g., 300 DPI) for print quality
            PdfSaveOptions pdfOptions = new PdfSaveOptions();
            pdfOptions.HorizontalResolution = 300; // DPI horizontally
            pdfOptions.VerticalResolution = 300;   // DPI vertically

            // Save the diagram as a PDF using the configured options
            diagram.Save("output.pdf", pdfOptions);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
