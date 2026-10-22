using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

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
            // Load the Visio diagram inside a using block as required.
            using (Diagram diagram = new Diagram(inputVisioPath))
            {
                // Apply the watermark image to each page.
                foreach (Page page in diagram.Pages)
                {
                    // Retrieve page dimensions (in inches).
                    double pageWidth = page.PageSheet.PageProps.PageWidth.Value;
                    double pageHeight = page.PageSheet.PageProps.PageHeight.Value;

                    // Center coordinates for the shape.
                    double pinX = pageWidth / 2.0;
                    double pinY = pageHeight / 2.0;

                    // Insert the image as a shape that covers the whole page.
                    using (FileStream fs = new FileStream(watermarkImagePath, FileMode.Open, FileAccess.Read))
                    {
                        long shapeId = page.AddShape(pinX, pinY, pageWidth, pageHeight, fs);
                        Shape watermarkShape = page.Shapes.GetShape(shapeId);

                        // Send the watermark shape to the back so it appears behind other content.
                        watermarkShape.SendToBack();

                        // Make the watermark non‑selectable.
                        watermarkShape.Protection.LockSelect.Value = BOOL.True;
                    }
                }

                // Configure PDF save options.
                PdfSaveOptions pdfOptions = new PdfSaveOptions();
                pdfOptions.SaveFormat = SaveFileFormat.Pdf;
                pdfOptions.DefaultFont = "Arial";

                // Save the watermarked diagram as PDF.
                diagram.Save(outputPdfPath, pdfOptions);
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}