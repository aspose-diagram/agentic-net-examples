using System;
using Aspose.Diagram;

class Program
    {
        static void Main()
        {
            // Create a new empty diagram
            using (Diagram diagram = new Diagram())
            {
                // Get the first (default) page
                Page page = diagram.Pages[0];

                // Define pentagon parameters
                double centerX = 5.0;   // inches
                double centerY = 5.0;   // inches
                double radius = 2.0;    // inches
                int sides = 5;

                // Build a flat double array with the pentagon vertices.
                // The first point is repeated at the end to close the shape.
                double[] points = new double[(sides + 1) * 2];
                for (int i = 0; i <= sides; i++)
                {
                    double angleDeg = i * 360.0 / sides;
                    double angleRad = Math.PI * angleDeg / 180.0;
                    double x = centerX + radius * Math.Cos(angleRad);
                    double y = centerY + radius * Math.Sin(angleRad);
                    points[i * 2] = x;
                    points[i * 2 + 1] = y;
                }

                // Draw the pentagon using the flat double array overload.
                // This returns the shape ID (long).
                long pentagonId = page.DrawPolyline(points);

                // Retrieve the shape object to apply formatting (optional).
                Shape pentagonShape = page.Shapes.GetShape((int)pentagonId);
                // Set a red fill color.
                pentagonShape.Fill.FillForegnd.Value = "#FF0000";
                // Set a black outline.
                pentagonShape.Line.LineColor.Value = "#000000";

                // Save the diagram to a VSDX file.
                diagram.Save("Pentagon.vsdx", SaveFileFormat.Vsdx);
            }
        }
    }