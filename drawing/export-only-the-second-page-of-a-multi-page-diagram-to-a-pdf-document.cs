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

            // Load the multi‑page Visio diagram
            string inputPath = "input.vsdx";
            Diagram diagram = new Diagram(inputPath);

            // Configure PDF save options to export only the second page (zero‑based index 1)
            PdfSaveOptions pdfOptions = new PdfSaveOptions();
            pdfOptions.PageIndex = 1;   // second page
            pdfOptions.PageCount = 1;   // export a single page
            pdfOptions.DefaultFont = "Arial";

            // Save the selected page as a PDF file
            string outputPath = "second_page.pdf";
            diagram.Save(outputPath, pdfOptions);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
