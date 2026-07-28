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

            // Load an existing Visio diagram
            using (Diagram diagram = new Diagram("input.vsdx"))
            {
                // Process each page in the diagram
                foreach (Page page in diagram.Pages)
                {
                    // Retrieve page dimensions (in inches)
                    double pageWidth = page.PageSheet.PageProps.PageWidth.Value;
                    double pageHeight = page.PageSheet.PageProps.PageHeight.Value;

                    // Calculate center coordinates
                    double pinX = pageWidth / 2.0;
                    double pinY = pageHeight / 2.0;

                    // Add a text shape that will serve as the watermark
                    // Width and height are set to the full page size for full‑page coverage
                    Shape watermark = page.AddText(
                        pinX,
                        pinY,
                        pageWidth,
                        pageHeight,
                        "CONFIDENTIAL",
                        "Calibri",
                        "#A0A0A0",
                        0.5); // font size in inches

                    // Rotate the watermark 45 degrees (angle in radians)
                    watermark.SetAngle(Math.PI / 4);
                }

                // Save the modified diagram as PDF
                PdfSaveOptions pdfOptions = new PdfSaveOptions();
                pdfOptions.DefaultFont = "Calibri";
                diagram.Save("output.pdf", pdfOptions);
            }

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
