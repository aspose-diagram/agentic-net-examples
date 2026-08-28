using System.IO;
using System;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        // Create a new empty diagram
        Diagram diagram = new Diagram();

        // Get the first (default) page
        Page page = diagram.Pages[0];

        // Define pentagon vertices (regular pentagon centered at (5,5) with radius 2)
        double centerX = 5.0;
        double centerY = 5.0;
        double radius = 2.0;
        double angleOffset = Math.PI / 2; // start at top

        // Calculate the five points
        double[] points = new double[12]; // 5 points + repeat first point to close shape (6*2)
        for (int i = 0; i < 5; i++)
        {
            double angle = angleOffset + i * 2 * Math.PI / 5;
            double x = centerX + radius * Math.Cos(angle);
            double y = centerY + radius * Math.Sin(angle);
            points[i * 2] = x;
            points[i * 2 + 1] = y;
        }
        // Close the polygon by repeating the first point
        points[10] = points[0];
        points[11] = points[1];

        // Draw the pentagon using DrawPolyline.
        // The overload requires the first two points (x1,y1,x2,y2) and then the remaining points array.
        // We'll pass the first two vertices separately and the rest (including the closing point) in the array.
        double x1 = points[0];
        double y1 = points[1];
        double x2 = points[2];
        double y2 = points[3];
        // Remaining points start from index 4 (third vertex) to the end.
        double[] remaining = new double[points.Length - 4];
        Array.Copy(points, 4, remaining, 0, remaining.Length);

        long shapeIdLong = page.DrawPolyline(x1, y1, x2, y2, remaining);
        int shapeId = (int)shapeIdLong;

        // Retrieve the shape object
        Shape pentagon = page.Shapes.GetShape(shapeId);

        // Get bounding box dimensions
        double width = pentagon.XForm.Width.Value;
        double height = pentagon.XForm.Height.Value;

        // Output the results
        Console.WriteLine($"Pentagon Bounding Box Width: {width} inches");
        Console.WriteLine($"Pentagon Bounding Box Height: {height} inches");
    }
}
