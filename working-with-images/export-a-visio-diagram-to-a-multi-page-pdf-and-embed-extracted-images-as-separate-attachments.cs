using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class VisioToPdfWithAttachments
{
    static void Main()
    {
        try
        {

            // Paths
            string inputVisio = @"C:\Docs\sample.vsdx";
            string outputPdf = @"C:\Docs\sample_multi_page.pdf";
            string attachmentsFolder = @"C:\Docs\Attachments";

            // Load the Visio diagram (uses the Diagram(string) constructor)
            Diagram diagram = new Diagram(inputVisio);

            // Configure PDF save options to split diagram into multiple pages
            PdfSaveOptions pdfOptions = new PdfSaveOptions
            {
                SplitMultiPages = true // ensures each Visio page becomes a PDF page
            };

            // Save the diagram as a multi‑page PDF (uses Diagram.Save(string, SaveOptions))
            diagram.Save(outputPdf, pdfOptions);

            // Create folder for extracted shape PDFs (each shape saved as a separate PDF)
            Directory.CreateDirectory(attachmentsFolder);

            int shapeCounter = 0;
            // Iterate through all pages and shapes
            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    // Save each shape to its own PDF file (uses Shape.ToPdf(string))
                    string shapePdfPath = Path.Combine(attachmentsFolder, $"Shape_{shapeCounter}.pdf");
                    shape.ToPdf(shapePdfPath);
                    shapeCounter++;
                }
            }

            // Cleanup
            diagram.Dispose();

        }
        catch (System.IO.DirectoryNotFoundException ex)
        {
            Console.Error.WriteLine($"[DirectoryNotFoundException] {ex.Message}");
        }
    }
}
