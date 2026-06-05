using System.IO;
using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main()
    {
        // Create a new empty diagram
        Diagram diagram = new Diagram();

        // Access the first (default) page
        Page page = diagram.Pages[0];

        // Define the diamond vertices.
        // Start at top (0,1), go to right (1,0), then bottom, left, and back to top.
        double[] points = new double[] { 0, -1, -1, 0, 0, 1 };

        // Draw the diamond shape; returns the shape ID.
        long shapeId = page.DrawPolyline(0, 1, 1, 0, points);

        // Retrieve the shape object using the returned ID.
        Shape diamond = page.Shapes.GetShape((int)shapeId);

        // Apply a thick border (line weight) and set its color.
        diamond.Line.LineWeight.Value = 0.05; // thickness in inches
        diamond.Line.LineColor.Value = "#000000";

        // Export the diagram (containing the diamond) to SVG.
        diagram.Save("diamond.svg", new SVGSaveOptions());
    }
}
