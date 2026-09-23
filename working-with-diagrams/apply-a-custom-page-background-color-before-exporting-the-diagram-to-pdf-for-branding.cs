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

            // Input Visio file path
            string inputPath = "input.vsdx";
            // Output PDF file path
            string outputPath = "output.pdf";

            // Load the diagram
            Diagram diagram = new Diagram(inputPath);

            // Define the background color (hex string)
            string backgroundHex = "#ADD8E6"; // Light blue

            // Process each foreground page
            foreach (Page page in diagram.Pages)
            {
                // Skip pages that are already background pages
                if (page.Background == BOOL.True)
                    continue;

                // Retrieve page dimensions (in inches)
                double pageWidth = page.PageSheet.PageProps.PageWidth.Value;
                double pageHeight = page.PageSheet.PageProps.PageHeight.Value;

                // Create a new background page
                Page bgPage = new Page();

                // Assign a unique ID and name to the background page
                int maxId = 0;
                foreach (Page p in diagram.Pages)
                {
                    if (p.ID > maxId) maxId = p.ID;
                }
                bgPage.ID = maxId + 1;
                bgPage.Name = "Background_" + bgPage.ID;
                bgPage.Background = BOOL.True;

                // Calculate the center position for the rectangle shape
                double pinX = pageWidth / 2.0;
                double pinY = pageHeight / 2.0;

                // Add a rectangle shape that spans the entire page
                // Parameters: pinX, pinY, width, height, master name, isCalculate
                long shapeId = bgPage.AddShape(pinX, pinY, pageWidth, pageHeight, "Rectangle", false);
                Shape bgShape = bgPage.Shapes.GetShape(shapeId);

                // Set solid fill pattern
                bgShape.Fill.FillPattern.Value = 1; // Solid
                // Set the desired background color
                bgShape.Fill.FillForegnd.Value = backgroundHex;
                // Remove outline by setting line pattern to none
                bgShape.Line.LinePattern.Value = (LinePatternValue)0;
                // Send the shape to the back so other content appears above it
                bgShape.SendToBack();
                // Make the background shape non‑selectable
                bgShape.Protection.LockSelect.Value = BOOL.True;

                // Add the background page to the diagram
                diagram.Pages.Add(bgPage);
                // Link the foreground page to its background page
                page.BackPage = bgPage;
            }

            // Configure PDF save options (optional: set default font)
            PdfSaveOptions pdfOptions = new PdfSaveOptions();
            pdfOptions.DefaultFont = "Arial";

            // Save the diagram as PDF with the branding background
            diagram.Save(outputPath, pdfOptions);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
