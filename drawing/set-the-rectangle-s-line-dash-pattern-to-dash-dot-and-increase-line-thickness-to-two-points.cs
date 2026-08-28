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

            // Get the active page where the shape will be added
            Page page = diagram.ActivePage;

            // Draw a rectangle (pinX, pinY, width, height)
            // Example position and size; adjust as needed
            long rectId = page.DrawRectangle(2.0, 2.0, 4.0, 3.0);

            // Retrieve the shape object using the returned ID
            Shape rect = page.Shapes.GetShape((int)rectId);

            // Set line dash pattern to dash‑dot
            rect.Line.LinePattern.Value = LinePatternValue.DashDot;

            // Increase line thickness to two points (2/72 inches ≈ 0.0277778)
            rect.Line.LineWeight.Value = 0.0277778;

            // Save the diagram to a VSDX file
            diagram.Save("RectangleDemo.vsdx", SaveFileFormat.Vsdx);

        }
        catch (System.NullReferenceException ex)
        {
            Console.Error.WriteLine($"[NullReferenceException] {ex.Message}");
        }
    }
}
