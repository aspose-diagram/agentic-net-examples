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

            // Create PDF save options
            PdfSaveOptions pdfOptions = new PdfSaveOptions();
            // Specify that the output format is PDF
            pdfOptions.SaveFormat = SaveFileFormat.Pdf;
            // Preserve original page orientation (do not enlarge the page)
            pdfOptions.EnlargePage = false;

            // Save the diagram as a PDF file using the specified options
            diagram.Save("output.pdf", pdfOptions);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
