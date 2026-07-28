using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Expect three arguments: input Visio file, background image file, output PDF file.
        if (args.Length < 3)
        {
            Console.WriteLine("Usage: <program> <inputVisioPath> <backgroundImagePath> <outputPdfPath>");
            return;
        }

        string inputVisioPath = args[0];
        string backgroundImagePath = args[1];
        string outputPdfPath = args[2];

        try
        {
            // Load the Visio diagram.
            Diagram diagram = new Diagram(inputVisioPath);

            // Iterate over all pages and add the background image.
            foreach (Page page in diagram.Pages)
            {
                // Retrieve page dimensions (in inches).
                double pageWidth = page.PageSheet.PageProps.PageWidth.Value;
                double pageHeight = page.PageSheet.PageProps.PageHeight.Value;

                // Center coordinates for the shape (pin is at the center).
                double pinX = pageWidth / 2.0;
                double pinY = pageHeight / 2.0;

                // Insert the image as a shape covering the entire page.
                using (FileStream imgStream = new FileStream(backgroundImagePath, FileMode.Open, FileAccess.Read))
                {
                    long shapeId = page.AddShape(pinX, pinY, pageWidth, pageHeight, imgStream);

                    // Retrieve the created shape.
                    Shape bgShape = page.Shapes.GetShape(shapeId);

                    // Send the shape to the back so it appears behind other content.
                    page.SendToBack(shapeId);

                    // Make the background shape non‑selectable.
                    bgShape.Protection.LockSelect.Value = BOOL.True;
                }
            }

            // Configure PDF save options (optional: set default font).
            PdfSaveOptions pdfOptions = new PdfSaveOptions();
            pdfOptions.DefaultFont = "Arial";

            // Save the updated diagram as PDF.
            diagram.Save(outputPdfPath, pdfOptions);

            Console.WriteLine("Diagram exported to PDF successfully.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
    }
}
