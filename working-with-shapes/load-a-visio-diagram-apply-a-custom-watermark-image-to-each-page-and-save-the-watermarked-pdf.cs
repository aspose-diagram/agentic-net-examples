using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
    {
        static void Main(string[] args)
        {
            try
            {

                // Input Visio file, output PDF file, and watermark image file paths.
                string inputVisioPath = "input.vsdx";
                string outputPdfPath = "output.pdf";
                string watermarkImagePath = "watermark.png";

                // Load the Visio diagram.
                Diagram diagram = new Diagram(inputVisioPath);

                // Iterate through each page and add the watermark image as a background shape.
                foreach (Page page in diagram.Pages)
                {
                    // Retrieve page dimensions (in inches).
                    double pageWidth = page.PageSheet.PageProps.PageWidth.Value;
                    double pageHeight = page.PageSheet.PageProps.PageHeight.Value;

                    // Center coordinates for the shape (PinX, PinY).
                    double pinX = pageWidth / 2.0;
                    double pinY = pageHeight / 2.0;

                    // Add the image shape covering the entire page.
                    using (FileStream imgStream = new FileStream(watermarkImagePath, FileMode.Open, FileAccess.Read))
                    {
                        // AddShape returns the shape ID (long).
                        long shapeId = page.AddShape(pinX, pinY, pageWidth, pageHeight, imgStream);

                        // Retrieve the shape object to modify its properties.
                        Shape watermarkShape = page.Shapes.GetShape(shapeId);

                        // Send the watermark to the back so it does not obscure other content.
                        watermarkShape.SendToBack();

                        // Make the watermark non‑selectable.
                        watermarkShape.Protection.LockSelect.Value = BOOL.True;
                    }
                }

                // Configure PDF save options (optional: set default font for missing glyphs).
                PdfSaveOptions pdfOptions = new PdfSaveOptions();
                pdfOptions.DefaultFont = "Arial";

                // Save the modified diagram as a PDF.
                diagram.Save(outputPdfPath, pdfOptions);

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }