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

            // Create a new diagram (or load an existing one)
            Diagram diagram = new Diagram();

            // Ensure there is at least one foreground page
            Page foregroundPage = diagram.Pages[0];

            // Retrieve page dimensions (in inches)
            double pageWidth = foregroundPage.PageSheet.PageProps.PageWidth.Value;
            double pageHeight = foregroundPage.PageSheet.PageProps.PageHeight.Value;

            // -------------------------------------------------
            // 1. Create a background page
            // -------------------------------------------------
            // Determine a new unique page ID
            int maxId = 0;
            foreach (Page p in diagram.Pages)
            {
                if (p.ID > maxId) maxId = p.ID;
            }
            Page backgroundPage = new Page();
            backgroundPage.ID = maxId + 1;
            backgroundPage.Name = "BackgroundPage";
            backgroundPage.Background = BOOL.True; // Mark as background page
            diagram.Pages.Add(backgroundPage);

            // -------------------------------------------------
            // 2. Add a rectangle shape that covers the whole page
            // -------------------------------------------------
            // AddShape(pinX, pinY, width, height, masterName)
            long bgShapeId = backgroundPage.AddShape(0, 0, pageWidth, pageHeight, "Rectangle");
            Shape bgShape = backgroundPage.Shapes.GetShape(bgShapeId);

            // Set solid fill pattern
            bgShape.Fill.FillPattern.Value = 1; // Solid
            // Set desired background color (hex string)
            bgShape.Fill.FillForegnd.Value = "#ADD8E6"; // Light blue

            // Remove any border
            bgShape.Line.LinePattern.Value = 0; // No line

            // Send the shape to the back and lock selection
            bgShape.SendToBack();
            bgShape.Protection.LockSelect.Value = BOOL.True;

            // -------------------------------------------------
            // 3. Associate the background page with the foreground page
            // -------------------------------------------------
            foregroundPage.BackPage = backgroundPage;

            // -------------------------------------------------
            // 4. Export the diagram to PDF with branding
            // -------------------------------------------------
            PdfSaveOptions pdfOptions = new PdfSaveOptions();
            pdfOptions.DefaultFont = "Arial"; // Fallback font for missing glyphs
            diagram.Save("BrandedOutput.pdf", pdfOptions);

        }
        catch (Aspose.Diagram.DiagramException ex)
        {
            Console.Error.WriteLine($"[DiagramException] {ex.Message}");
        }
    }
}
