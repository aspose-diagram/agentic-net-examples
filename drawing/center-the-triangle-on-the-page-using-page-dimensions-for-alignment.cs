using System.IO;
using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main()
    {
        // Create a new blank diagram
        using (Diagram diagram = new Diagram())
        {
            // Access the first (and only) page
            Page page = diagram.Pages[0];

            // Define triangle vertices (in inches)
            // Points: (0,0), (2,0), (1,1.732) and close back to (0,0)
            double[] trianglePoints = new double[]
            {
                0, 0,
                2, 0,
                1, 1.732,
                0, 0   // close the polygon
            };

            // Draw the triangle; returns the shape ID (long)
            long triangleId = page.DrawPolyline(trianglePoints);

            // Retrieve the shape object
            Shape triangle = page.Shapes.GetShape(triangleId);

            // Get page dimensions (in inches)
            double pageWidth = page.PageSheet.PageProps.PageWidth.Value;
            double pageHeight = page.PageSheet.PageProps.PageHeight.Value;

            // Get shape dimensions (in inches)
            double shapeWidth = triangle.XForm.Width.Value;
            double shapeHeight = triangle.XForm.Height.Value;

            // Center the triangle on the page by setting its PinX and PinY to page center
            // For shapes drawn via DrawPolyline, PinX/Y represent the shape's center.
            triangle.XForm.PinX.Value = pageWidth / 2.0;
            triangle.XForm.PinY.Value = pageHeight / 2.0;

            // Save the diagram to a VSDX file
            diagram.Save("CenteredTriangle.vsdx", SaveFileFormat.Vsdx);
        }
    }
}
