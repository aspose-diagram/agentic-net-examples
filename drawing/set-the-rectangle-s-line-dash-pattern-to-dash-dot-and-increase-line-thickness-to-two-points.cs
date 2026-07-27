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

            // Create a new empty diagram
            Diagram diagram = new Diagram();

            // Get the active page
            Page page = diagram.ActivePage;

            // Draw a rectangle at position (2,2) with width 4 and height 2 inches
            // DrawRectangle returns the shape ID (long)
            long rectId = page.DrawRectangle(2.0, 2.0, 4.0, 2.0);

            // Retrieve the shape object using the ID
            Shape rectShape = page.Shapes.GetShape(rectId);

            // Set line dash pattern to dash‑dot
            // The enum value for dash‑dot is DashDot (if available)
            rectShape.Line.LinePattern.Value = LinePatternValue.DashDot;

            // Increase line thickness to two points (2/72 inches)
            rectShape.Line.LineWeight.Value = 2.0 / 72.0;

            // Save the diagram to a VSDX file
            diagram.Save("RectangleModified.vsdx", SaveFileFormat.Vsdx);

        }
        catch (System.NullReferenceException ex)
        {
            Console.Error.WriteLine($"[NullReferenceException] {ex.Message}");
        }
    }
}
