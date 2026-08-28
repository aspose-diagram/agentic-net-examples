using System.IO;
using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main()
    {
        try
        {

            // Create a new empty Visio diagram
            Diagram diagram = new Diagram();

            // Get the active page where we will draw the shape
            Page page = diagram.ActivePage;

            // Define the points of a diamond (top, right, bottom, left, back to top)
            // The flat double array represents: x1, y1, x2, y2, x3, y3, ...
            double[] diamondPoints = new double[]
            {
                5.0, 2.0,   // Top
                7.0, 4.0,   // Right
                5.0, 6.0,   // Bottom
                3.0, 4.0,   // Left
                5.0, 2.0    // Close back to Top
            };

            // Draw the diamond as a polyline; this returns the shape ID (long)
            long shapeId = page.DrawPolyline(diamondPoints);

            // Retrieve the shape object using the returned ID
            Shape diamond = page.Shapes.GetShape(shapeId);

            // Set a thick border (line weight) and black line color
            diamond.Line.LineWeight.Value = 0.05; // thickness in inches
            diamond.Line.LineColor.Value = "#000000";

            // Export the diagram (containing the diamond) to SVG format
            SVGSaveOptions svgOptions = new SVGSaveOptions();
            diagram.Save("diamond.svg", svgOptions);

        }
        catch (System.NullReferenceException ex)
        {
            Console.Error.WriteLine($"[NullReferenceException] {ex.Message}");
        }
    }
}
