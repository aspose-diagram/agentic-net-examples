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
            // Get the first (default) page
            Page page = diagram.Pages[0];

            // Define pentagon vertices (in inches) and close the shape by repeating the first point
            double[] pentagonPoints = new double[]
            {
                2.0, 2.0,   // Point 1
                4.0, 2.0,   // Point 2
                5.0, 4.0,   // Point 3
                3.0, 6.0,   // Point 4
                1.0, 4.0,   // Point 5
                2.0, 2.0    // Close back to Point 1
            };

            // Draw the pentagon shape on the page
            long shapeId = page.DrawPolyline(pentagonPoints);

            // Retrieve the shape object to apply formatting
            Shape pentagon = page.Shapes.GetShape(shapeId);

            // Set fill color (red) and solid fill pattern
            pentagon.Fill.FillForegnd.Value = "#FF0000";
            pentagon.Fill.FillPattern.Value = 1; // Solid fill

            // Set line color (blue) and line weight
            pentagon.Line.LineColor.Value = "#0000FF";
            pentagon.Line.LineWeight.Value = 0.02; // Thickness in inches

            // Save the diagram to a VSDX file
            diagram.Save("Pentagon.vsdx", SaveFileFormat.Vsdx);
        }
    }
}
