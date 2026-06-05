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

            // Load the Visio diagram (replace with your source file path)
            Diagram diagram = new Diagram("input.vsdx");

            // Set up PDF save options with high resolution (300 DPI) for print quality
            PdfSaveOptions pdfOptions = new PdfSaveOptions
            {
                HorizontalResolution = 300, // 300 dots per inch horizontally
                VerticalResolution = 300    // 300 dots per inch vertically
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
