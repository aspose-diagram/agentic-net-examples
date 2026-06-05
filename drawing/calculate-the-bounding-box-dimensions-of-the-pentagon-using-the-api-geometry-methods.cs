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

            // Define pentagon vertices (closed shape, first point repeated at end)
            // Coordinates are in inches
            double[] pentagonPoints = new double[]
            {
                5.0, 7.0,   // Top vertex
                6.9, 6.0,
                6.3, 8.5,
                3.7, 8.5,
                3.1, 6.0,
                5.0, 7.0    // Close the shape
            };

            // Draw the pentagon using DrawPolyline; returns the shape ID (long)
            long shapeId = page.DrawPolyline(pentagonPoints);

            // Retrieve the shape object (GetShape expects an int)
            Shape pentagon = page.Shapes.GetShape((int)shapeId);

            // Initialize bounding box extremes
            double minX = double.MaxValue;
            double maxX = double.MinValue;
            double minY = double.MaxValue;
            double maxY = double.MinValue;

            // The geometry is stored in the first Geom collection
            Geom geom = (Geom)pentagon.Geoms[0];

            // Iterate over all coordinate segments (MoveTo, LineTo, etc.)
            foreach (var segment in geom.CoordinateCol)
            {
                // Handle MoveTo segment
                if (segment is MoveTo move)
                {
                    UpdateBounds(move.X.Value, move.Y.Value, ref minX, ref maxX, ref minY, ref maxY);
                }
                // Handle LineTo segment
                else if (segment is LineTo line)
                {
                    UpdateBounds(line.X.Value, line.Y.Value, ref minX, ref maxX, ref minY, ref maxY);
                }
                // Additional segment types (ArcTo, SplineKnot, etc.) can be added here if needed
            }

            // Calculate width and height of the bounding box
            double boundingWidth = maxX - minX;
            double boundingHeight = maxY - minY;

            // Output the results
            Console.WriteLine($"Pentagon Bounding Box:");
            Console.WriteLine($"Min X: {minX}, Max X: {maxX}");
            Console.WriteLine($"Min Y: {minY}, Max Y: {maxY}");
            Console.WriteLine($"Width : {boundingWidth}");
            Console.WriteLine($"Height: {boundingHeight}");
        }

        // Helper method to update min/max values
        static void UpdateBounds(double x, double y,
                                 ref double minX, ref double maxX,
                                 ref double minY, ref double maxY)
        {
            if (x < minX) minX = x;
            if (x > maxX) maxX = x;
            if (y < minY) minY = y;
            if (y > maxY) maxY = y;
        }
    }