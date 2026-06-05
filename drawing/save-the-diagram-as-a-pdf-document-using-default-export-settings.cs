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

            // Load an existing Visio diagram (replace with your source file)
            Diagram diagram = new Diagram("input.vsdx");

            // Create PDF save options with default settings
            PdfSaveOptions pdfOptions = new PdfSaveOptions();

            // Save the diagram as a PDF file using the default export options
            diagram.Save("output.pdf", pdfOptions);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
