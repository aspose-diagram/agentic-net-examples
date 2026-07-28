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

            // Input Visio file and watermark image paths
            string visioPath = "input.vsdx";
            string watermarkImagePath = "watermark.png";
            string outputPdfPath = "output.pdf";

            // Load the Visio diagram
            using (Diagram diagram = new Diagram(visioPath))
            {
                // Open the watermark image once and reuse the stream for each page
                using (FileStream imgStream = new FileStream(watermarkImagePath, FileMode.Open, FileAccess.Read))
                {
                    // Iterate over all pages in the diagram
                    foreach (Page page in diagram.Pages)
                    {
                        // Retrieve page dimensions (in inches)
                        double pageWidth = page.PageSheet.PageProps.PageWidth.Value;
                        double pageHeight = page.PageSheet.PageProps.PageHeight.Value;

                        // Center position for the image shape
                        double pinX = pageWidth / 2.0;
                        double pinY = pageHeight / 2.0;

                        // Add the image as a shape covering the whole page
                        long shapeId = page.AddShape(pinX, pinY, pageWidth, pageHeight, imgStream);

                        // Retrieve the shape object to adjust its properties
                        Shape watermarkShape = page.Shapes.GetShape(shapeId);

                        // Send the watermark to the back so other content appears above it
                        watermarkShape.SendToBack();

                        // Make the watermark non‑selectable
                        watermarkShape.Protection.LockSelect.Value = BOOL.True;
                    }
                }

                // Configure PDF save options
                PdfSaveOptions pdfOptions = new PdfSaveOptions();
                pdfOptions.DefaultFont = "Arial";
                pdfOptions.SaveFormat = SaveFileFormat.Pdf;

                // Save the diagram as a PDF with the watermarks applied
                diagram.Save(outputPdfPath, pdfOptions);
            }

            Console.WriteLine("Watermarked PDF saved to: " + outputPdfPath);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
