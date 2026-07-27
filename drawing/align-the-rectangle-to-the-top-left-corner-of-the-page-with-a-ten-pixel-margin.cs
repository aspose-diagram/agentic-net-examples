using System.IO;
using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class AlignRectangleExample
{
    static void Main()
    {
        // Create a new blank diagram
        Diagram diagram = new Diagram();

        // Access the first (default) page
        Page page = diagram.Pages[0];

        // Define rectangle dimensions
        double rectWidth = 100.0;   // width in points (or pixels depending on DPI)
        double rectHeight = 50.0;   // height in points

        // Define margin from the top‑left corner
        double margin = 10.0;

        // Calculate the pin (center) coordinates so that the rectangle's
        // top‑left corner aligns with the page's top‑left corner plus margin.
        // PinX = left margin + half of width
        // PinY = top margin + half of height
        double pinX = margin + rectWidth / 2.0;
        double pinY = margin + rectHeight / 2.0;

        // Draw the rectangle on the page
        long shapeId = page.DrawRectangle(pinX, pinY, rectWidth, rectHeight);

        // (Optional) Retrieve the shape to modify further if needed
        // Shape rectShape = page.Shapes.GetShape(shapeId);

        // Save the diagram to a VSDX file
        diagram.Save("AlignedRectangle.vsdx", SaveFileFormat.Vsdx);
    }
}
