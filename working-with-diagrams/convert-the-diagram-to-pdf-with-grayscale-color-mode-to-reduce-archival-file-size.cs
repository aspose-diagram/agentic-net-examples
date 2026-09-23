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

            // Path to the source Visio diagram
            string inputPath = "input.vsdx";

            // Path for the resulting PDF file
            string outputPath = "output.pdf";

            // Load the diagram from file
            Diagram diagram = new Diagram(inputPath);

            // Configure PDF save options
            PdfSaveOptions pdfOptions = new PdfSaveOptions();
            // Set a default font to avoid missing‑font warnings
            pdfOptions.DefaultFont = "Arial";

            // Note: Aspose.Diagram does not expose a direct grayscale mode for PDF export.
            // The PDF will be saved with the existing colors; further post‑processing
            // would be required to convert it to grayscale if needed.

            // Save the diagram as a PDF file
            diagram.Save(outputPath, pdfOptions);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
