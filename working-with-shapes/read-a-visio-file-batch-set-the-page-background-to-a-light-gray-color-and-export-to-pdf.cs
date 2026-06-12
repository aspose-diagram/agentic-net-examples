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

            // Load the Visio diagram
            Diagram diagram = new Diagram(inputPath);

            // Ensure there is at least one page to copy dimensions from
            if (diagram.Pages.Count == 0)
            {
                throw new Exception("The diagram contains no pages.");
            }

            // Use the first page to obtain width and height (in inches)
            Page referencePage = diagram.Pages[0];
            double pageWidth = referencePage.PageSheet.PageProps.PageWidth.Value;
            double pageHeight = referencePage.PageSheet.PageProps.PageHeight.Value;

            // Create a new background page
            Page backgroundPage = new Page();
            backgroundPage.Background = BOOL.True; // Mark as background page
            backgroundPage.Name = "Background";
            backgroundPage.PageSheet.PageProps.PageWidth.Value = pageWidth;
            backgroundPage.PageSheet.PageProps.PageHeight.Value = pageHeight;

            // Draw a rectangle that covers the entire page
            // PinX and PinY are the center of the rectangle
            double pinX = pageWidth / 2.0;
            double pinY = pageHeight / 2.0;
            long rectShapeId = backgroundPage.DrawRectangle(pinX, pinY, pageWidth, pageHeight);

            // Retrieve the rectangle shape and set its fill to light gray
            Shape rectShape = backgroundPage.Shapes.GetShape(rectShapeId);
            rectShape.Fill.FillPattern.Value = 1;               // Solid fill
            rectShape.Fill.FillForegnd.Value = "#D3D3D3";       // Light gray color
            rectShape.Line.LinePattern.Value = 0;              // No border
            backgroundPage.SendToBack(rectShapeId);            // Ensure it stays behind other content

            // Add the background page to the diagram
            diagram.Pages.Add(backgroundPage);

            // Assign the background page to all foreground pages
            foreach (Page page in diagram.Pages)
            {
                if (page != backgroundPage)
                {
                    page.BackPage = backgroundPage;
                }
            }

            // Configure PDF save options
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
