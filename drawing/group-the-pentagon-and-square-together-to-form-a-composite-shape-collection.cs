using System;
using Aspose.Diagram;

class Program
    {
        static void Main()
        {
            // Create a new empty diagram
            Diagram diagram = new Diagram();

            // Use the first (default) page
            Page page = diagram.Pages[0];

            // -------------------------------------------------
            // 1. Add a pentagon using DrawPolyline.
            //    The points are defined as a flat double array:
            //    (x1, y1, x2, y2, ..., x5, y5, x1, y1) – the first point
            //    is repeated to close the polygon.
            // -------------------------------------------------
            double[] pentagonPoints = new double[]
            {
                2.0, 2.0,   // Point 1
                3.5, 1.0,   // Point 2
                5.0, 2.0,   // Point 3
                4.5, 4.0,   // Point 4
                2.5, 4.0,   // Point 5
                2.0, 2.0    // Close polygon
            };
            long pentagonId = page.DrawPolyline(pentagonPoints);
            Shape pentagonShape = page.Shapes.GetShape(pentagonId);

            // -------------------------------------------------
            // 2. Add a square using DrawRectangle.
            //    Parameters: pinX, pinY (center), width, height.
            // -------------------------------------------------
            double squareCenterX = 4.0;
            double squareCenterY = 6.0;
            double squareSize = 2.0; // width = height
            long squareId = page.DrawRectangle(squareCenterX, squareCenterY, squareSize, squareSize);
            Shape squareShape = page.Shapes.GetShape(squareId);

            // -------------------------------------------------
            // 3. Group the pentagon and square together.
            //    The Group method takes an array of Shape objects
            //    and returns the newly created group shape.
            // -------------------------------------------------
            Shape groupShape = page.Shapes.Group(new Shape[] { pentagonShape, squareShape });
            groupShape.Name = "PentagonSquareGroup";

            // -------------------------------------------------
            // 4. Save the diagram to a VSDX file.
            // -------------------------------------------------
            diagram.Save("GroupedShapes.vsdx", SaveFileFormat.Vsdx);
        }
    }