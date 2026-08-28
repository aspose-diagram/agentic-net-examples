using System.IO;
using System;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        // Create a new empty diagram (contains a default page)
        using (Diagram diagram = new Diagram())
        {
            // Get the first (default) page
            Page page = diagram.Pages[0];

            // Convert 10 pixels to inches (assuming 96 DPI)
            double marginInches = 10.0 / 96.0;

            // Define rectangle size (in inches)
            double rectWidth = 2.0;   // example width
            double rectHeight = 1.0;  // example height

            // Calculate the PinX and PinY so that the rectangle's left/top edges
            // are positioned at the page's top‑left corner with the margin
            double pinX = marginInches + rectWidth / 2.0;
            double pageHeight = page.PageSheet.PageProps.PageHeight.Value;
            double pinY = pageHeight - marginInches - rectHeight / 2.0;

            // Draw the rectangle on the page
            long rectShapeId = page.DrawRectangle(pinX, pinY, rectWidth, rectHeight);

            // (Optional) Retrieve the shape to modify its appearance
            Shape rectShape = page.Shapes.GetShape((int)rectShapeId);
            rectShape.Line.LineColor.Value = "#FF0000";   // red border
            rectShape.Fill.FillForegnd.Value = "#00FF00"; // green fill

            // Save the diagram to a VSDX file
            diagram.Save("AlignedRectangle.vsdx", SaveFileFormat.Vsdx);
        }
    }
}
