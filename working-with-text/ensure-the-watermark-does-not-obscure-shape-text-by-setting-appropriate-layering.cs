using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
    {
        static void Main()
        {
            // Create a new empty Visio diagram
            Diagram diagram = new Diagram();

            // Get the first (default) page
            Page page = diagram.Pages[0];

            // -------------------------------------------------
            // Add a sample shape (rectangle) with some text
            // -------------------------------------------------
            // Draw a rectangle at (2,2) with width=4 inches and height=2 inches
            long rectId = page.DrawRectangle(2.0, 2.0, 4.0, 2.0);
            // Retrieve the shape object
            Shape rectShape = page.Shapes.GetShape(rectId);
            // Add visible text to the rectangle
            rectShape.Text.Value.Clear();
            rectShape.Text.Value.Add(new Txt("Sample Shape Text"));
            // Optionally bring the rectangle to front to ensure visibility
            rectShape.BringToFront();

            // -------------------------------------------------
            // Add a watermark that covers the whole page
            // -------------------------------------------------
            // Retrieve page dimensions (in inches)
            double pageWidth = page.PageSheet.PageProps.PageWidth.Value;
            double pageHeight = page.PageSheet.PageProps.PageHeight.Value;
            // Center position for the watermark
            double centerX = pageWidth / 2.0;
            double centerY = pageHeight / 2.0;
            // Add the watermark text shape (full page size)
            Shape watermark = page.AddText(
                centerX,               // pinX (center of rotation)
                centerY,               // pinY
                pageWidth,             // width (covers whole page)
                pageHeight,            // height
                "CONFIDENTIAL",        // watermark text
                "Arial",               // font name
                "#CCCCCC",             // light gray color
                0.25);                 // font size in inches (≈18 pt)

            // Send the watermark to the back so it does not obscure other shapes
            watermark.SendToBack();

            // -------------------------------------------------
            // Save the diagram to a VSDX file
            // -------------------------------------------------
            diagram.Save("WatermarkedDiagram.vsdx", SaveFileFormat.Vsdx);
        }
    }