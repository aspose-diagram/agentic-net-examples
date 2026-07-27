using System;
using Aspose.Diagram;

class Program
    {
        static void Main()
        {
            // Create a new empty diagram
            Diagram diagram = new Diagram();

            // Get the first page (there is always at least one page in a new diagram)
            Page page = diagram.Pages[0];

            // Define pentagon vertices (clockwise) – coordinates are in inches
            // The shape will be closed by repeating the first point at the end
            double[] pentagonPoints = new double[]
            {
                2.0, 2.0,   // Point 1
                4.0, 2.0,   // Point 2
                5.0, 4.0,   // Point 3
                3.0, 6.0,   // Point 4
                1.0, 4.0,   // Point 5
                2.0, 2.0    // Close polygon (repeat first point)
            };

            // Draw the pentagon using DrawPolyline (flat double array overload)
            long shapeIdLong = page.DrawPolyline(pentagonPoints);
            // Retrieve the shape object (cast long to int as required by GetShape)
            Shape pentagonShape = page.Shapes.GetShape((int)shapeIdLong);

            // Initialize bounding box extremes
            double minX = double.MaxValue;
            double maxX = double.MinValue;
            double minY = double.MaxValue;
            double maxY = double.MinValue;

            // Iterate through all geometry sections of the shape
            foreach (Aspose.Diagram.Geom geom in pentagonShape.Geoms)
            {
                // Iterate through each coordinate (MoveTo, LineTo, etc.)
                for (int i = 0; i < geom.CoordinateCol.Count; i++)
                {
                    // The collection stores untyped objects; retrieve as object then use dynamic
                    object coordObj = geom.CoordinateCol[i];
                    dynamic coord = coordObj; // Allows access to X and Y at runtime

                    double x = (double)coord.X.Value;
                    double y = (double)coord.Y.Value;

                    if (x < minX) minX = x;
                    if (x > maxX) maxX = x;
                    if (y < minY) minY = y;
                    if (y > maxY) maxY = y;
                }
            }

            // Calculate width and height of the bounding box
            double boundingWidth = maxX - minX;
            double boundingHeight = maxY - minY;

            // Output the results
            Console.WriteLine($"Bounding Box for the pentagon:");
            Console.WriteLine($"Min X: {minX}, Max X: {maxX}");
            Console.WriteLine($"Min Y: {minY}, Max Y: {maxY}");
            Console.WriteLine($"Width : {boundingWidth}");
            Console.WriteLine($"Height: {boundingHeight}");

            // (Optional) Save the diagram to verify the shape visually
            diagram.Save("Pentagon.vsdx", SaveFileFormat.Vsdx);
        }
    }