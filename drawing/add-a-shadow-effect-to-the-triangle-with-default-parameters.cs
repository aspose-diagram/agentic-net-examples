using System.IO;
using System;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        // Create a new diagram
        Diagram diagram = new Diagram();

        // Get the first page
        Page page = diagram.Pages[0];

        // Draw a triangle using a polyline (three points)
        page.DrawPolyline(new double[] { 1, 1, 3, 1, 2, 3 });

        // Retrieve the created shape (first shape on the page)
        Shape triangle = null;
        foreach (Shape s in page.Shapes)
        {
            triangle = s;
            break;
        }

        if (triangle == null)
        {
            throw new Exception("Failed to create triangle shape.");
        }

        // Apply default shadow effect
        triangle.Fill.ShapeShdwType.Value = ShapeShdwTypeValue.Simple;
        triangle.Fill.ShdwForegnd.Value = "#000000"; // black shadow
        triangle.Fill.ShdwForegndTrans.Value = 0.0;   // opaque
        triangle.Fill.ShapeShdwOffsetX.Value = 0.1;  // horizontal offset
        triangle.Fill.ShapeShdwOffsetY.Value = 0.1;  // vertical offset

        // Save the diagram
        diagram.Save("TriangleWithShadow.vsdx", SaveFileFormat.Vsdx);
    }
}
