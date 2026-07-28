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

            // Create PDF save options – by default only the fonts actually used are embedded
            PdfSaveOptions pdfOptions = new PdfSaveOptions();

            // (Optional) Specify a fallback font for characters that cannot be rendered
            // pdfOptions.DefaultFont = "Arial";

            // Save the diagram as PDF using the specified options
            diagram.Save("output.pdf", pdfOptions);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
