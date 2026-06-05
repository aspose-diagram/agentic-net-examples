using System.IO;
using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main()
    {
        // Create a new empty diagram
        using (Diagram diagram = new Diagram())
        {
            // Access the first page (a new diagram contains a default page)
            Page page = diagram.Pages[0];

            // Define triangle vertices (X, Y) in inches
            // Points: (2,2), (4,2), (3,4)
            double[] trianglePoints = new double[] { 2, 2, 4, 2, 3, 4, 2, 2 };

            // Draw the triangle as a polyline; the method returns the shape ID (long)
            long triangleId = page.DrawPolyline(trianglePoints);

            // Retrieve the shape object using the returned ID
            Shape triangle = page.Shapes.GetShape(triangleId);

            // Uniformly scale the triangle by a factor of 1.5
            // Multiply the width and height of the shape's XForm cell values
            triangle.XForm.Width.Value *= 1.5;
            triangle.XForm.Height.Value *= 1.5;

            // Optionally, you could also adjust the PinX/PinY if you want to keep the shape centered.
            // In this example we keep the center (PinX/PinY) unchanged.

            // Save the diagram to a VSDX file
            diagram.Save("ScaledTriangle.vsdx", SaveFileFormat.Vsdx);
        }
    }
}
