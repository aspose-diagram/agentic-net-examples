using System.IO;
using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main()
    {
        // Create an empty diagram
        Diagram diagram = new Diagram();

        // Add a foreground page (the visible page)
        Page foregroundPage = new Page();
        diagram.Pages.Add(foregroundPage);

        // Add a background page that will hold the background color
        Page backgroundPage = new Page();
        backgroundPage.Background = BOOL.True; // mark as a background page
        diagram.Pages.Add(backgroundPage);

        // Retrieve the dimensions of the foreground page
        double pageWidth = foregroundPage.PageSheet.PageProps.PageWidth.Value;
        double pageHeight = foregroundPage.PageSheet.PageProps.PageHeight.Value;

        // Draw a rectangle that covers the entire page on the background page
        // PinX and PinY are the centre of the shape
        long rectShapeId = backgroundPage.DrawRectangle(pageWidth / 2, pageHeight / 2, pageWidth, pageHeight);
        Shape backgroundShape = backgroundPage.Shapes.GetShape((int)rectShapeId);

        // Set the rectangle to a solid light‑gray fill and remove its border
        backgroundShape.Fill.FillPattern.Value = 1;            // solid fill
        backgroundShape.Fill.FillForegnd.Value = "#D3D3D3";    // light gray color
        backgroundShape.Line.LinePattern.Value = 0;          // no line

        // Assign the background page to every foreground page in the diagram
        foreach (Page page in diagram.Pages)
        {
            if (page.Background == BOOL.False)
            {
                page.BackPage = backgroundPage;
            }
        }

        // Save the diagram (any supported format, e.g., VSDX)
        diagram.Save("DiagramWithGrayBackground.vsdx", SaveFileFormat.Vsdx);
    }
}
