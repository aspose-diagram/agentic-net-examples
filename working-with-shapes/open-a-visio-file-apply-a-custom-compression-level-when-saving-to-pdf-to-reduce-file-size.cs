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

            // Path to the source Visio file
            string inputPath = "input.vsdx";

            // Path for the resulting PDF file
            string outputPath = "output.pdf";

            // Load the Visio diagram
            Diagram diagram = new Diagram(inputPath);

            // Set up PDF save options with text compression to reduce file size
            PdfSaveOptions pdfOptions = new PdfSaveOptions();
            pdfOptions.TextCompression = PdfTextCompression.Flate;
            pdfOptions.DefaultFont = "Arial";

            // Save the diagram as a PDF using the configured options
            diagram.Save(outputPath, pdfOptions);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
