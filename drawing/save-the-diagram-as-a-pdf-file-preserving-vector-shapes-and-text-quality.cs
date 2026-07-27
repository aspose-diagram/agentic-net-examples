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

            // Load the Visio diagram from a file (replace with your actual file path)
            Diagram diagram = new Diagram("input.vsdx");

            // Create PDF save options to keep vector shapes and high‑quality text
            PdfSaveOptions pdfOptions = new PdfSaveOptions();
            // The default settings already preserve vector graphics; additional options can be set if needed
            // Example: pdfOptions.Compliance = PdfCompliance.Pdf15; // default PDF version
            // Example: pdfOptions.TextCompression = TextCompression.Flate; // default compression

            // Save the entire diagram as a PDF file using the specified options
            diagram.Save("output.pdf", pdfOptions);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
