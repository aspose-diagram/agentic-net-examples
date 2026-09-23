using System.IO;
using System;
using Aspose.Diagram;
using Aspose.Drawing;

class Program
{
    static void Main()
    {
        // Create a new empty diagram
        Diagram diagram = new Diagram();

        // Get the first page (a new diagram contains one default page)
        Page page = diagram.Pages[0];

        // Define pentagon vertices (clockwise) and close the shape by repeating the first point
        double[] pentagonPoints = new double[]
        {
            2.0, 1.0,   // Point 1
            4.0, 1.0,   // Point 2
            5.0, 3.0,   // Point 3
            3.0, 5.0,   // Point 4
            1.0, 3.0,   // Point 5
            2.0, 1.0    // Close polygon (repeat Point 1)
        };

        // Draw the pentagon using DrawPolyline (returns the shape ID as long)
        long shapeIdLong = page.DrawPolyline(pentagonPoints);

        // Retrieve the shape object (cast long to int if needed)
        Shape pentagon = page.Shapes.GetShape(shapeIdLong);

        // -----------------------------------------------------------------
        // Method 1: Use the shape's XForm width and height (already the bounding box)
        double widthFromXForm = pentagon.XForm.Width.Value;
        double heightFromXForm = pentagon.XForm.Height.Value;

        Console.WriteLine($"Bounding box from XForm: Width = {widthFromXForm}, Height = {heightFromXForm}");

        // -----------------------------------------------------------------
        // Method 2: Compute bounding box from geometry vertices
        // Assume the shape has at least one geometry section
        if (pentagon.Geoms.Count > 0)
        {
            // Get the first geometry (most shapes have a single geometry)
            Geom geom = (Geom)pentagon.Geoms[0];

            double minX = double.MaxValue;
            double maxX = double.MinValue;
            double minY = double.MaxValue;
            double maxY = double.MinValue;

            // Iterate all coordinate cells (MoveTo, LineTo, etc.)
            foreach (object coord in geom.CoordinateCol)
            {
                // All coordinate cells have X and Y properties of type DoubleValue
                if (coord is MoveTo moveTo)
                {
                    double x = moveTo.X.Value;
                    double y = moveTo.Y.Value;
                    if (x < minX) minX = x;
                    if (x > maxX) maxX = x;
                    if (y < minY) minY = y;
                    if (y > maxY) maxY = y;
                }
                else if (coord is LineTo lineTo)
                {
                    double x = lineTo.X.Value;
                    double y = lineTo.Y.Value;
                    if (x < minX) minX = x;
                    if (x > maxX) maxX = x;
                    if (y < minY) minY = y;
                    if (y > maxY) maxY = y;
                }
                // Other segment types (ArcTo, etc.) can be added similarly if needed
            }

            double computedWidth = maxX - minX;
            double computedHeight = maxY - minY;

            Console.WriteLine($"Bounding box from geometry: Width = {computedWidth}, Height = {computedHeight}");
        }
        else
        {
            Console.WriteLine("No geometry data found on the pentagon shape.");
        }
    }
}
