using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Expect three arguments: input Visio file, watermark image file, output PDF file
        if (args.Length < 3)
        {
            Console.Error.WriteLine("Usage: WatermarkPdfExample <inputVisioPath> <watermarkImagePath> <outputPdfPath>");
            return;
        }

        // Assign and validate input Visio file path
        string inputVisioPath = args[0];
        if (!File.Exists(inputVisioPath))
        {
            Console.Error.WriteLine($"File not found: {inputVisioPath}");
            return;
        }

        // Assign and validate watermark image file path
        string watermarkImagePath = args[1];
        if (!File.Exists(watermarkImagePath))
        {
            Console.Error.WriteLine($"File not found: {watermarkImagePath}");
            return;
        }

        // Assign output PDF file path (no existence check needed)
        string outputPdfPath = args[2];

        try
        {
            // Load the Visio diagram from the specified file
            Diagram diagram = new Diagram(inputVisioPath);

            // Iterate through each page to add the watermark image as a background shape
            foreach (Page page in diagram.Pages)
            {
                // Retrieve page dimensions (in inches)
                double pageWidth = page.PageSheet.PageProps.PageWidth.Value;
                double pageHeight = page.PageSheet.PageProps.PageHeight.Value;

                // Calculate the center position for the shape (PinX, PinY)
                double pinX = pageWidth / 2.0;
                double pinY = pageHeight / 2.0;

                // Insert the image covering the whole page
                using (FileStream imgStream = new FileStream(watermarkImagePath, FileMode.Open, FileAccess.Read))
                {
                    // AddShape returns a long shape ID; use it to retrieve the Shape object
                    long shapeId = page.AddShape(pinX, pinY, pageWidth, pageHeight, imgStream);
                    Shape watermarkShape = page.Shapes.GetShape(shapeId);

                    // Send the image to the back so it appears behind other content
                    watermarkShape.SendToBack();

                    // Make the watermark non‑selectable
                    watermarkShape.Protection.LockSelect.Value = BOOL.True;

                    // Optional: set a fill pattern that works with image textures
                    watermarkShape.Fill.FillPattern.Value = 25;
                }
            }

            // Configure PDF save options (e.g., default font fallback)
            PdfSaveOptions pdfOptions = new PdfSaveOptions();
            pdfOptions.DefaultFont = "Arial";

            // Save the diagram as a PDF with the watermarks applied
            diagram.Save(outputPdfPath, pdfOptions);
        }
        catch (Exception ex)
        {
            // Write any processing errors to the error console
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}