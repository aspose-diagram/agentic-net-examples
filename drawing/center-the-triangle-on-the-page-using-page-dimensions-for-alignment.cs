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
            // Access the first (default) page
            Page page = diagram.Pages[0];

            // Define triangle points (base width 2 inches, height ~1.732 inches)
            // Points are defined in a flat double array: x1, y1, x2, y2, x3, y3, x1, y1 (close polygon)
            double[] trianglePoints = new double[]
            {
                0.0, 0.0,      // Point A
                2.0, 0.0,      // Point B
                1.0, 1.732,    // Point C (apex)
                0.0, 0.0       // Close back to Point A
            };

            // Draw the triangle; returns the shape ID (long)
            long triangleId = page.DrawPolyline(trianglePoints);

            // Retrieve the shape object using the ID (GetShape expects an int)
            Shape triangle = page.Shapes.GetShape((int)triangleId);

            // Get page dimensions (in inches)
            double pageWidth = page.PageSheet.PageProps.PageWidth.Value;
            double pageHeight = page.PageSheet.PageProps.PageHeight.Value;

            // Center the triangle by setting its PinX and PinY to the page center
            triangle.XForm.PinX.Value = pageWidth / 2.0;
            triangle.XForm.PinY.Value = pageHeight / 2.0;

            // Save the diagram to a VSDX file
            diagram.Save("CenteredTriangle.vsdx", SaveFileFormat.Vsdx);
        }
    }
}
