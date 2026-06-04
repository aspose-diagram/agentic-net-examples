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

            // Load the Visio diagram from a file
            Diagram diagram = new Diagram("input.vsdx");

            // Create PDF save options
            PdfSaveOptions pdfOptions = new PdfSaveOptions();

            // Set a default font to ensure characters are rendered correctly
            // and the font is embedded in the resulting PDF.
            pdfOptions.DefaultFont = "Arial Unicode MS";

            // Save the diagram as PDF using the configured options
            diagram.Save("output.pdf", pdfOptions);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
