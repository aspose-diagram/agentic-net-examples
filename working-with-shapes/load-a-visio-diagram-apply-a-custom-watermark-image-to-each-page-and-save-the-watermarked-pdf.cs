using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;
using Aspose.Drawing;

class Program
{
    static void Main(string[] args)
    {
        // Validate command‑line arguments.
        if (args.Length < 3)
        {
            Console.Error.WriteLine("Usage: Program <inputVisioPath> <watermarkImagePath> <outputPdfPath>");
            return;
        }

        string inputVisioPath = args[0];
        string watermarkImagePath = args[1];
        string outputPdfPath = args[2];

        // Guard against missing files.
        if (!File.Exists(inputVisioPath))
        {
            Console.Error.WriteLine($"Input Visio file not found: {inputVisioPath}");
            return;
        }

        if (!File.Exists(watermarkImagePath))
        {
            Console.Error.WriteLine($"Watermark image file not found: {watermarkImagePath}");
            return;
        }

        try
        {
            // Load the Visio diagram inside a using block (required by the API).
            using (Diagram diagram = new Diagram(inputVisioPath))
            {
                // Load the watermark image once to obtain its dimensions.
                using (FileStream watermarkStream = new FileStream(watermarkImagePath, FileMode.Open, FileAccess.Read))
                using (Aspose.Drawing.Image watermarkImg = Aspose.Drawing.Image.FromStream(watermarkStream))
                {
                    double imgWidth = watermarkImg.Width;
                    double imgHeight = watermarkImg.Height;

                    // Iterate over each page and insert the watermark shape.
                    foreach (Page page in diagram.Pages)
                    {
                        // Retrieve page dimensions (in inches).
                        double pageWidth = page.PageSheet.PageProps.PageWidth.Value;
                        double pageHeight = page.PageSheet.PageProps.PageHeight.Value;

                        // Calculate top‑left position to centre the image.
                        double pinX = (pageWidth - imgWidth) / 2.0;
                        double pinY = (pageHeight - imgHeight) / 2.0;

                        // Reset stream position before each insertion.
                        watermarkStream.Position = 0;

                        // Add the image as a shape on the page. Returns a shape ID (long).
                        long shapeId = page.AddShape(pinX, pinY, imgWidth, imgHeight, watermarkStream);

                        // Retrieve the newly added shape via GetShape (expects a long ID).
                        Shape watermarkShape = page.Shapes.GetShape(shapeId);

                        // Send the shape to back so it appears as a watermark.
                        watermarkShape.SendToBack();

                        // Optional: make the watermark non‑selectable.
                        watermarkShape.Protection.LockSelect.Value = BOOL.True;
                    }
                }

                // Configure PDF save options.
                PdfSaveOptions pdfOptions = new PdfSaveOptions();
                pdfOptions.DefaultFont = "Arial";               // Fallback font.
                pdfOptions.SaveFormat = SaveFileFormat.Pdf;    // Explicitly set format.

                // Save the watermarked diagram as PDF.
                diagram.Save(outputPdfPath, pdfOptions);
            }

            Console.WriteLine($"Watermarked PDF saved to: {outputPdfPath}");
        }
        catch (Exception ex)
        {
            // Write any errors to the error stream.
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}