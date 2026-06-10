using System.IO;
using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class SavePageAsPdf
{
    static void Main()
    {
        try
        {

            // Path to the source Visio file
            string sourceFile = @"C:\Diagrams\sample.vsdx";

            // Path for the output PDF file
            string outputPdf = @"C:\Diagrams\page3.pdf";

            // Index of the page to be saved (0‑based). For example, page 3 => index 2
            int pageIndex = 2;

            // Load the Visio diagram
            Diagram diagram = new Diagram(sourceFile);

            // Configure PDF save options to render only the required page
            PdfSaveOptions pdfOptions = new PdfSaveOptions
            {
                // Render only the page at the specified index
                PageIndex = pageIndex,
                PageCount = 1,

                // Preserve vector graphics and text (default behavior)
                // Additional optional settings can be adjusted here if needed
                // e.g., TextCompression = TextCompression.Flate;
            };

            // Save the selected page as a PDF document
            diagram.Save(outputPdf, pdfOptions);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
