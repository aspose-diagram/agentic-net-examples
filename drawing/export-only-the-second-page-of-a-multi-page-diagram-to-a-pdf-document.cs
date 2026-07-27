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

            // Path to the source Visio diagram (multi‑page)
            string inputPath = "input.vsdx";

            // Path for the resulting PDF containing only the second page
            string outputPath = "second_page.pdf";

            // Load the diagram from file
            Diagram diagram = new Diagram(inputPath);

            // Configure PDF save options to export only the second page (zero‑based index = 1)
            PdfSaveOptions pdfOptions = new PdfSaveOptions();
            pdfOptions.PageIndex = 1;   // start from the second page
            pdfOptions.PageCount = 1;   // export a single page

            // Save the selected page as PDF
            diagram.Save(outputPath, pdfOptions);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
