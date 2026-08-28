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

            // Configure PDF save options to export comments as callout annotations
            PdfSaveOptions pdfOptions = new PdfSaveOptions();
            pdfOptions.IsExportComments = true;

            // Save the diagram as a PDF file with comments rendered
            diagram.Save("output.pdf", pdfOptions);

            // Clean up resources
            diagram.Dispose();

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
