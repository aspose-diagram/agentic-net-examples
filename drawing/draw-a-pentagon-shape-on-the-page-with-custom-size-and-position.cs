using System.IO;
using System;
using Aspose.Diagram;
using Aspose.Drawing;

class Program
{
    static void Main()
    {
        // Create a new blank diagram
        using (Diagram diagram = new Diagram())
        {
            // Get the first (default) page
            Page page = diagram.Pages[0];

            // Define pentagon parameters
            double centerX = 5.0;   // inches
            double centerY = 5.0;   // inches
            double radius = 2.0;    // inches
            int vertexCount = 5;

            // Calculate the vertices of the pentagon
            PointF[] vertices = new PointF[vertexCount];
            double startAngle = -Math.PI / 2; // start at the top
            double angleStep = 2 * Math.PI / vertexCount;

            for (int i = 0; i < vertexCount; i++)
            {
                double angle = startAngle + i * angleStep;
                float x = (float)(centerX + radius * Math.Cos(angle));
                float y = (float)(centerY + radius * Math.Sin(angle));
                vertices[i] = new PointF(x, y);
            }

            // Draw the pentagon using DrawPolyline.
            // The method requires the first two points as separate parameters,
            // and the remaining points as an array.
            // To close the shape, repeat the first point at the end of the array.
            PointF[] remainingPoints = new PointF[vertexCount - 1];
            for (int i = 1; i < vertexCount; i++)
                remainingPoints[i - 1] = vertices[i];
            // Append the first point again to close the polygon
            Array.Resize(ref remainingPoints, remainingPoints.Length + 1);
            remainingPoints[remainingPoints.Length - 1] = vertices[0];

            long shapeId = page.DrawPolyline(
                vertices[0].X, vertices[0].Y,
                vertices[1].X, vertices[1].Y,
                remainingPoints);

            // Retrieve the shape object to apply formatting
            Shape pentagon = page.Shapes.GetShape((int)shapeId);

            // Set line color (black) and fill color (gold)
            pentagon.Line.LineColor.Value = "#000000";
            pentagon.Fill.FillForegnd.Value = "#FFCC00";

            // Save the diagram to a VSDX file
            diagram.Save("Pentagon.vsdx", SaveFileFormat.Vsdx);
        }
    }
}
