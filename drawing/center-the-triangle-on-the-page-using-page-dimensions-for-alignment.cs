using System.IO;
using System;
using Aspose.Diagram;

public class Program
{
    public static void Main()
    {
        // Create a new empty diagram
        using (Diagram diagram = new Diagram())
        {
            // Access the first (default) page
            Page page = diagram.Pages[0];

            // Define triangle vertices (in inches)
            double[] trianglePoints = new double[] { 0, 0, 2, 0, 1, 2, 0, 0 };

            // Draw the triangle; returns the shape ID (long)
            long triangleId = page.DrawPolyline(trianglePoints);

            // Retrieve the shape object using the ID
            Shape triangle = page.Shapes.GetShape(triangleId);

            // Get page dimensions (in inches)
            double pageWidth = page.PageSheet.PageProps.PageWidth.Value;
            double pageHeight = page.PageSheet.PageProps.PageHeight.Value;

            // Center the triangle on the page
            triangle.XForm.PinX.Value = pageWidth / 2.0;
            triangle.XForm.PinY.Value = pageHeight / 2.0;

            // Save the diagram
            diagram.Save("CenteredTriangle.vsdx", SaveFileFormat.Vsdx);
        }
    }
}
