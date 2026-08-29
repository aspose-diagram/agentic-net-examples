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
            string inputVisioPath = "input.vsdx";
            string backgroundImagePath = "background.png";
            string outputPdfPath = "output.pdf";

            // Load the Visio diagram
            using (Diagram diagram = new Diagram(inputVisioPath))
            {
                // Iterate through all pages
                foreach (Page page in diagram.Pages)
                {
                    // Retrieve page dimensions (in inches)
                    double pageWidth = page.PageSheet.PageProps.PageWidth.Value;
                    double pageHeight = page.PageSheet.PageProps.PageHeight.Value;

                    // Center coordinates for the shape (pin is at the center)
                    double pinX = pageWidth / 2.0;
                    double pinY = pageHeight / 2.0;

                    // Insert the background image as a shape covering the whole page
                    using (FileStream imgStream = new FileStream(backgroundImagePath, FileMode.Open, FileAccess.Read))
                    {
                        long shapeId = page.AddShape(pinX, pinY, pageWidth, pageHeight, imgStream);

                        // Retrieve the created shape
                        Shape bgShape = page.Shapes.GetShape(shapeId);

                        // Ensure the shape has no outline
                        bgShape.Line.LinePattern.Value = LinePatternValue.None;

                        // Set fill pattern to solid (required for image shapes)
                        bgShape.Fill.FillPattern.Value = 1;

                        // Send the shape to the back so it appears behind other content
                        page.SendToBack(shapeId);

                        // Make the background non‑selectable
                        bgShape.Protection.LockSelect.Value = BOOL.True;
                    }
                }

                // Configure PDF save options (optional: set default font)
                PdfSaveOptions pdfOptions = new PdfSaveOptions();
                pdfOptions.DefaultFont = "Arial";

                // Save the updated diagram as PDF
                diagram.Save(outputPdfPath, pdfOptions);
            }

            Console.WriteLine("Diagram processed and saved to PDF successfully.");

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
