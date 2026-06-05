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

            // Position and size for the oval (ellipse)
            double pinX = 5.0;   // X coordinate of the center
            double pinY = 5.0;   // Y coordinate of the center
            double width = 4.0;  // Width of the oval (in inches)
            double height = 2.0; // Height of the oval (in inches)

            // Draw the oval on the active page; returns the shape ID
            long shapeId = diagram.ActivePage.DrawEllipse(pinX, pinY, width, height);

            // Retrieve the concrete Shape instance using the ID
            Shape oval = diagram.ActivePage.Shapes.GetShape((int)shapeId);

            // Optional: set a fill color (green in this example)
            oval.Fill.FillForegnd.Value = "#00FF00";

            // Set fill opacity to 70% (i.e., 30% transparency)
            // FillForegndTrans.Value expects a percentage (0 = opaque, 100 = fully transparent)
            oval.Fill.FillForegndTrans.Value = 30; // 30% transparent => 70% opaque

            // Save the diagram to a VSDX file
            diagram.Save("OvalOpacity.vsdx", SaveFileFormat.Vsdx);

        }
        catch (System.NullReferenceException ex)
        {
            Console.Error.WriteLine($"[NullReferenceException] {ex.Message}");
        }
    }
}
