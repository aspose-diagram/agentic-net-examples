using System.IO;
using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main()
    {
        // Create a new diagram (lifecycle create rule)
        Diagram diagram = new Diagram();

        // Use the first page (a diagram always contains at least one page)
        Page page = diagram.Pages[0];

        // Define the five vertices of a regular pentagon.
        // The points are expressed relative to the shape's width and height.
        double radius = 1.0;               // half of the shape's width/height
        double[] pentagonPoints = new double[10];
        for (int i = 0; i < 5; i++)
        {
            // Start at the top vertex and step around the circle.
            double angle = Math.PI / 2 + i * 2 * Math.PI / 5;
            pentagonPoints[2 * i] = radius * Math.Cos(angle);     // X coordinate
            pentagonPoints[2 * i + 1] = radius * Math.Sin(angle); // Y coordinate
        }

        // Draw the pentagon using the DrawPolyline rule.
        // Width and height are set to 2 (diameter of the circumscribed circle).
        double pinX = 5.0;   // X position of the shape's pin on the page
        double pinY = 5.0;   // Y position of the shape's pin on the page
        double shapeWidth = 2.0;
        double shapeHeight = 2.0;
        long shapeId = page.DrawPolyline(pinX, pinY, shapeWidth, shapeHeight, pentagonPoints);

        // Retrieve the created shape.
        Shape pentagon = page.Shapes.GetShape(shapeId);

        // Uniformly double the dimensions while preserving proportions.
        pentagon.XForm.Width.Value *= 2;
        pentagon.XForm.Height.Value *= 2;

        // Save the diagram (lifecycle save rule).
        diagram.Save("ScaledPentagon.vsdx", SaveFileFormat.Vsdx);
    }
}
