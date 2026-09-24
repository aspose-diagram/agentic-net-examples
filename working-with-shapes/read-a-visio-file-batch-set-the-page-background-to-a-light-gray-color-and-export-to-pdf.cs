using System.IO;
using System;
using System.Collections.Generic;
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

            // Load the Visio diagram
            Diagram diagram = new Diagram(inputPath);

            // Determine the current maximum page ID to assign unique IDs to new pages
            int maxPageId = 0;
            foreach (Page p in diagram.Pages)
            {
                if (p.ID > maxPageId)
                    maxPageId = p.ID;
            }

            // Process each page to add a light gray background
            List<Page> pages = new List<Page>();
            foreach (Page p in diagram.Pages)
                pages.Add(p); // copy to list to avoid modification during iteration

            foreach (Page page in pages)
            {
                // Create a new background page
                Page bgPage = new Page();
                bgPage.ID = ++maxPageId;
                bgPage.Name = $"Background_{page.ID}";
                bgPage.Background = BOOL.True;

                // Add the background page to the diagram
                diagram.Pages.Add(bgPage);

                // Retrieve page dimensions (in inches)
                double pageWidth = page.PageSheet.PageProps.PageWidth.Value;
                double pageHeight = page.PageSheet.PageProps.PageHeight.Value;

                // Center coordinates for the rectangle shape
                double pinX = pageWidth / 2.0;
                double pinY = pageHeight / 2.0;

                // Add a rectangle shape that spans the entire page
                long shapeId = bgPage.AddShape(pinX, pinY, pageWidth, pageHeight, "Rectangle", false);
                Shape bgShape = bgPage.Shapes.GetShape(shapeId);

                // Set solid fill with light gray color (#D3D3D3)
                bgShape.Fill.FillPattern.Value = 1;               // Solid fill
                bgShape.Fill.FillForegnd.Value = "#D3D3D3";       // Light gray

                // Remove any outline
                bgShape.Line.LinePattern.Value = 0;               // No line

                // Send the shape to the back so it appears behind other content
                bgShape.SendToBack();

                // Make the background shape non‑selectable
                bgShape.Protection.LockSelect.Value = BOOL.True;

                // Associate the background page with the foreground page
                page.BackPage = bgPage;
            }

            // Configure PDF save options (optional: set default font)
            PdfSaveOptions pdfOptions = new PdfSaveOptions();
            pdfOptions.DefaultFont = "Arial";

            // Save the diagram as PDF
            diagram.Save(outputPath, pdfOptions);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
