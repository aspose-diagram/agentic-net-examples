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

                // Paths – adjust as needed
                string visioPath = "input.vsdx";
                string watermarkImagePath = "watermark.png";
                string outputPdfPath = "output.pdf";

                // Load the Visio diagram
                Diagram diagram = new Diagram(visioPath);

                // Iterate through each page and add the watermark image
                foreach (Page page in diagram.Pages)
                {
                    // Get page dimensions (in inches)
                    double pageWidth = page.PageSheet.PageProps.PageWidth.Value;
                    double pageHeight = page.PageSheet.PageProps.PageHeight.Value;

                    // Center position for the image shape
                    double pinX = pageWidth / 2.0;
                    double pinY = pageHeight / 2.0;

                    // Add the image shape covering the whole page
                    using (FileStream imgStream = new FileStream(watermarkImagePath, FileMode.Open, FileAccess.Read))
                    {
                        // width and height set to page size so the image fills the page
                        long shapeId = page.AddShape(pinX, pinY, pageWidth, pageHeight, imgStream);
                        Shape watermarkShape = page.Shapes.GetShape(shapeId);

                        // Send the image to back so it does not obscure other content
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

                Console.WriteLine("Watermarked PDF saved to: " + outputPdfPath);

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }