using System.IO;
using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        try
        {

            // Input Visio file path
            string inputPath = "input.vsdx";
            // Output PDF file path
            string outputPath = "output.pdf";

            // Load the diagram
            Diagram diagram = new Diagram(inputPath);

            // Create a background page (if not already present)
            Page backgroundPage = new Page();
            backgroundPage.Background = BOOL.True;
            diagram.Pages.Add(backgroundPage);

            // Use the dimensions of the first foreground page to size the background rectangle
            Page referencePage = diagram.Pages[0];
            double pageWidth = referencePage.PageSheet.PageProps.PageWidth.Value;
            double pageHeight = referencePage.PageSheet.PageProps.PageHeight.Value;

            // Add a rectangle shape that covers the entire page
            // AddShape(pinX, pinY, width, height, masterName) where masterName "Rectangle" is a built‑in master
            long rectShapeId = backgroundPage.AddShape(pageWidth / 2, pageHeight / 2, pageWidth, pageHeight, "Rectangle");
            Shape rectShape = backgroundPage.Shapes.GetShape(rectShapeId);

            // Set solid fill pattern and desired background color (e.g., light blue)
            rectShape.Fill.FillPattern.Value = 1;               // Solid fill
            rectShape.Fill.FillForegnd.Value = "#ADD8E6";       // Hex color code

            // Remove outline stroke
            rectShape.Line.LinePattern.Value = 0;               // No line

            // Send the rectangle to the back so other shapes appear above it
            rectShape.SendToBack();

            // Make the background shape non‑selectable
            rectShape.Protection.LockSelect.Value = BOOL.True;

            // Assign the background page to every foreground page
            foreach (Page page in diagram.Pages)
            {
                // Skip the background page itself
                if (page.Background == BOOL.True)
                    continue;

                page.BackPage = backgroundPage;
            }

            // Configure PDF save options (optional: set default font)
            PdfSaveOptions pdfOptions = new PdfSaveOptions();
            pdfOptions.DefaultFont = "Arial";

            // Save the diagram as PDF
            diagram.Save(outputPath, pdfOptions);

            Console.WriteLine("Diagram exported to PDF with uniform background color.");

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
