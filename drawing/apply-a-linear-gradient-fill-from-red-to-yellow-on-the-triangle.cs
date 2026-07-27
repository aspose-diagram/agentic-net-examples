using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
    {
        static void Main()
        {
            // Create a new diagram
            Diagram diagram = new Diagram();

            // Get the first (default) page
            Page page = diagram.Pages[0];

            // Define triangle points (X1,Y1, X2,Y2, X3,Y3, X1,Y1 to close)
            double[] trianglePoints = new double[] { 2, 2, 4, 2, 3, 4, 2, 2 };

            // Draw the triangle; returns the shape ID (long)
            long shapeId = page.DrawPolyline(trianglePoints);

            // Retrieve the shape object using the ID
            Shape shape = page.Shapes.GetShape((int)shapeId);

            // Apply gradient fill
            // 1. Set fill pattern to gradient (value 25)
            shape.Fill.FillPattern.Value = 25;

            // 2. Enable gradient fill
            shape.Fill.GradientFill.GradientEnabled.Value = BOOL.True;

            // 3. Set gradient direction to linear (0 degrees)
            shape.Fill.GradientFill.GradientDir.Value = 0;

            // 4. Clear any existing gradient stops
            shape.Fill.GradientFill.GradientStops.Clear();

            // 5. Add gradient stop at position 0 (red)
            shape.Fill.GradientFill.GradientStops.Add(
                new DoubleValue(0, MeasureConst.NUM),
                new ColorValue("#FF0000", MeasureConst.Undefined));

            // 6. Add gradient stop at position 1 (yellow)
            shape.Fill.GradientFill.GradientStops.Add(
                new DoubleValue(1, MeasureConst.NUM),
                new ColorValue("#FFFF00", MeasureConst.Undefined));

            // Save the diagram to a VSDX file
            diagram.Save("TriangleGradient.vsdx", SaveFileFormat.Vsdx);
        }
    }