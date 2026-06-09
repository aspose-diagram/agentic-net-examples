using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main()
    {
        try
        {

            // Load the Visio diagram from file
            Diagram diagram = new Diagram("input.vsdx");

            // Configure PDF save options for a multi‑page PDF
            PdfSaveOptions pdfOptions = new PdfSaveOptions();
            pdfOptions.SplitMultiPages = false;               // keep all pages in one PDF
            pdfOptions.SaveFormat = SaveFileFormat.Pdf;       // ensure PDF format

            // Export the whole diagram as a multi‑page PDF
            diagram.Save("output.pdf", pdfOptions);

            // -----------------------------------------------------------------
            // Extract each shape as an individual PDF (acting as an image file)
            // and store them in a folder to be used as separate attachments.
            // -----------------------------------------------------------------
            string attachmentsDir = Path.Combine(Directory.GetCurrentDirectory(), "ShapeAttachments");
            Directory.CreateDirectory(attachmentsDir);

            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    // Export the shape to a PDF file; the PDF can be treated as an image attachment.
                    string shapePdfPath = Path.Combine(
                        attachmentsDir,
                        $"Page{page.ID}_Shape{shape.ID}.pdf");

                    shape.ToPdf(shapePdfPath);
                }
            }

            // At this point:
            // - "output.pdf" contains the full multi‑page diagram.
            // - The folder "ShapeAttachments" holds individual PDFs for each shape,
            //   which can be attached to the main PDF using a PDF library if needed.
            // Cleanup
            diagram.Dispose();

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
