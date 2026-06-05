using System.IO;
using System;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        // Create a new empty diagram
        Diagram diagram = new Diagram();

        // Add a foreground page (the page where shapes will be placed)
        Page foregroundPage = new Page();
        diagram.Pages.Add(foregroundPage);

        // Add a background page that will hold the background color shape
        Page backgroundPage = new Page();
        backgroundPage.Background = BOOL.True; // mark as background page
        diagram.Pages.Add(backgroundPage);

        // Ensure background page has the same dimensions as the foreground page
        double pageWidth = foregroundPage.PageSheet.PageProps.PageWidth.Value;
        double pageHeight = foregroundPage.PageSheet.PageProps.PageHeight.Value;
        backgroundPage.PageSheet.PageProps.PageWidth.Value = pageWidth;
        backgroundPage.PageSheet.PageProps.PageHeight.Value = pageHeight;

        // Draw a rectangle that covers the entire background page
        // DrawRectangle(pinX, pinY, width, height) – pinX/Y are the lower‑left corner
        long bgShapeId = backgroundPage.DrawRectangle(0, 0, pageWidth, pageHeight);
        Shape bgShape = backgroundPage.Shapes.GetShape(bgShapeId);

        // Set the rectangle fill to a light gray color (#D3D3D3) and solid fill pattern
        bgShape.Fill.FillPattern.Value = 1;               // solid fill
        bgShape.Fill.FillForegnd.Value = "#D3D3D3";       // light gray

        // Remove any outline
        bgShape.Line.LinePattern.Value = 0;               // no line

        // Send the rectangle to the back so other shapes appear above it
        bgShape.SendToBack();

        // Make the background shape non‑selectable
        bgShape.Protection.LockSelect.Value = BOOL.True;

        // Link the foreground page to the background page
        foregroundPage.BackPage = backgroundPage;

        // -------------------------------------------------
        // Add regular shapes after the background is set
        // Example: a simple rectangle in the centre of the page
        double rectWidth = 2.0;
        double rectHeight = 1.0;
        double rectPinX = pageWidth / 2;
        double rectPinY = pageHeight / 2;
        long shapeId = foregroundPage.DrawRectangle(rectPinX, rectPinY, rectWidth, rectHeight);
        Shape shape = foregroundPage.Shapes.GetShape(shapeId);
        shape.Fill.FillForegnd.Value = "#FF0000"; // red fill for demonstration
        // -------------------------------------------------

        // Save the diagram to a VSDX file
        diagram.Save("output.vsdx", SaveFileFormat.Vsdx);
    }
}
