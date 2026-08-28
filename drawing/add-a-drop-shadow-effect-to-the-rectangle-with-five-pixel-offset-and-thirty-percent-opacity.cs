using System.IO;
using System;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        try
        {

            // Create a new diagram
            Diagram diagram = new Diagram();

            // Use the active page
            Page page = diagram.ActivePage;

            // Add a rectangle shape (pin at 2,2 inches, width 2 inches, height 1 inch)
            long rectId = page.AddShape(2.0, 2.0, 2.0, 1.0, "Rectangle");

            // Retrieve the shape object (GetShape expects an int)
            Shape rectShape = page.Shapes.GetShape((int)rectId);

            // Enable a simple drop shadow
            rectShape.Fill.ShapeShdwType.Value = ShapeShdwTypeValue.Simple;

            // Set shadow color to black
            rectShape.Fill.ShdwForegnd.Value = "#000000";

            // Set shadow transparency to 0.7 (30% opacity)
            rectShape.Fill.ShdwForegndTrans.Value = 0.7;

            // Approximate 5‑pixel offset (5 px ≈ 5/96 in ≈ 0.052 in)
            rectShape.Fill.ShapeShdwOffsetX.Value = 0.052;
            rectShape.Fill.ShapeShdwOffsetY.Value = 0.052;

            // Save the diagram
            diagram.Save("RectangleWithShadow.vsdx", SaveFileFormat.Vsdx);

        }
        catch (System.NullReferenceException ex)
        {
            Console.Error.WriteLine($"[NullReferenceException] {ex.Message}");
        }
    }
}
