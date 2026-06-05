using System.IO;
using System;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        try
        {

            // Create a new empty diagram
            Diagram diagram = new Diagram();

            // Access the active page where shapes will be added
            Page page = diagram.ActivePage;

            // Draw a rectangle (pinX, pinY, width, height) in inches
            // Example position: center at (2,2) with width 4 and height 2 inches
            long shapeId = page.DrawRectangle(2.0, 2.0, 4.0, 2.0);

            // Retrieve the shape object using the returned ID
            Shape rect = page.Shapes.GetShape((int)shapeId);

            // Set the line dash pattern to dash‑dot
            rect.Line.LinePattern.Value = LinePatternValue.DashDot;

            // Increase line thickness to two points (2 points = 2/72 inches)
            rect.Line.LineWeight.Value = 2.0 / 72.0;

            // Save the diagram to a VSDX file
            diagram.Save("RectangleDiagram.vsdx", SaveFileFormat.Vsdx);

        }
        catch (System.NullReferenceException ex)
        {
            Console.Error.WriteLine($"[NullReferenceException] {ex.Message}");
        }
    }
}
