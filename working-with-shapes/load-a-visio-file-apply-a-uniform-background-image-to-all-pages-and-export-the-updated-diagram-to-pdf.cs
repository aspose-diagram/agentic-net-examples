using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Input Visio file, background image, and output PDF paths.
        string visioPath = "input.vsdx";
        if (!File.Exists(visioPath)) { Console.Error.WriteLine($"File not found: {visioPath}"); return; }

        string imagePath = "background.png";
        if (!File.Exists(imagePath)) { Console.Error.WriteLine($"File not found: {imagePath}"); return; }

        string pdfPath = "output.pdf";

        try
        {
            // Load the Visio diagram.
            Diagram diagram = new Diagram(visioPath);

            // Apply the background image to each page.
            foreach (Page page in diagram.Pages)
            {
                // Retrieve page dimensions (in inches).
                double pageWidth = page.PageSheet.PageProps.PageWidth.Value;
                double pageHeight = page.PageSheet.PageProps.PageHeight.Value;

                // Calculate the center position for the image shape.
                double centerX = pageWidth / 2.0;
                double centerY = pageHeight / 2.0;

                // Insert the image as a shape covering the whole page.
                using (FileStream imgStream = new FileStream(imagePath, FileMode.Open, FileAccess.Read))
                {
                    long shapeId = page.AddShape(centerX, centerY, pageWidth, pageHeight, imgStream);
                    Shape bgShape = page.Shapes.GetShape(shapeId);

                    // Send the image shape to the back so other content appears above it.
                    bgShape.SendToBack();

                    // Optional: remove any outline by setting line pattern to 0 (no line).
                    bgShape.Line.LinePattern.Value = 0;

                    // Optional: set a solid fill pattern (required for image visibility in some cases).
                    bgShape.Fill.FillPattern.Value = 1;

                    // Make the background shape non‑selectable.
                    bgShape.Protection.LockSelect.Value = BOOL.True;
                }
            }

            // Configure PDF save options.
            PdfSaveOptions pdfOptions = new PdfSaveOptions();
            pdfOptions.DefaultFont = "Arial";

            // Save the updated diagram as PDF.
            diagram.Save(pdfPath, pdfOptions);

            Console.WriteLine("Diagram exported to PDF successfully.");
        }
        catch (Exception ex)
        {
            // Write any errors to the error stream.
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}