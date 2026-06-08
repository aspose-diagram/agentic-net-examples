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

            // Load an existing Visio diagram or create a new one.
            // Replace "input.vsdx" with the path to your source file if needed.
            Diagram diagram;
            string inputPath = "input.vsdx";
            if (System.IO.File.Exists(inputPath))
            {
                diagram = new Diagram(inputPath);
            }
            else
            {
                diagram = new Diagram(); // creates an empty diagram with a default page
            }

            // Get the first (foreground) page.
            Page foregroundPage = diagram.Pages[0];

            // Retrieve page dimensions (in inches).
            double pageWidth = foregroundPage.PageSheet.PageProps.PageWidth.Value;
            double pageHeight = foregroundPage.PageSheet.PageProps.PageHeight.Value;

            // -------------------------------------------------
            // Create a background page that will hold the color.
            // -------------------------------------------------
            Page backgroundPage = new Page();

            // Assign a unique ID to the background page.
            int maxPageId = 0;
            foreach (Page p in diagram.Pages)
            {
                if (p.ID > maxPageId)
                    maxPageId = p.ID;
            }
            backgroundPage.ID = maxPageId + 1;

            // Mark the page as a background page.
            backgroundPage.Background = BOOL.True;

            // Ensure the background page has the same size as the foreground page.
            backgroundPage.PageSheet.PageProps.PageWidth.Value = pageWidth;
            backgroundPage.PageSheet.PageProps.PageHeight.Value = pageHeight;

            // Add the background page to the diagram.
            diagram.Pages.Add(backgroundPage);

            // -------------------------------------------------
            // Add a rectangle shape that spans the whole page.
            // -------------------------------------------------
            // PinX and PinY represent the centre of the shape.
            double pinX = pageWidth / 2.0;
            double pinY = pageHeight / 2.0;

            // Use the built‑in "Rectangle" master.
            long rectShapeId = backgroundPage.AddShape(pinX, pinY, pageWidth, pageHeight, "Rectangle");

            // Retrieve the shape object to modify its appearance.
            Shape rectShape = backgroundPage.Shapes.GetShape(rectShapeId);

            // Set a solid fill pattern.
            rectShape.Fill.FillPattern.Value = 1;               // 1 = solid
            rectShape.Fill.FillForegnd.Value = "#ADD8E6";       // Light blue background color

            // Remove any outline.
            rectShape.Line.LinePattern.Value = 0;               // No line pattern
            rectShape.Line.LineWeight.Value = 0;                // Zero weight

            // Make the background shape non‑selectable.
            rectShape.Protection.LockSelect.Value = BOOL.True;

            // Send the rectangle to the back of the page's Z‑order.
            backgroundPage.SendToBack(rectShapeId);

            // Link the foreground page to the background page.
            foregroundPage.BackPage = backgroundPage;

            // -------------------------------------------------
            // Export the diagram to PDF with branding.
            // -------------------------------------------------
            PdfSaveOptions pdfOptions = new PdfSaveOptions
            {
                DefaultFont = "Arial",               // Fallback font
                SaveFormat = SaveFileFormat.Pdf      // Explicitly set the format
            };

            string outputPath = "branded_output.pdf";
            diagram.Save(outputPath, pdfOptions);

            Console.WriteLine($"Diagram saved to '{outputPath}' with custom background color.");

        }
        catch (Aspose.Diagram.DiagramException ex)
        {
            Console.Error.WriteLine($"[DiagramException] {ex.Message}");
        }
    }
}
