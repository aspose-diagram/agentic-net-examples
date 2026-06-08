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

            // Create PDF save options; default settings preserve the original page orientation
            PdfSaveOptions pdfOptions = new PdfSaveOptions();
            pdfOptions.SaveFormat = SaveFileFormat.Pdf; // Explicitly set format to PDF

            // Save the diagram as a PDF file using the specified options
            diagram.Save("output.pdf", pdfOptions);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
