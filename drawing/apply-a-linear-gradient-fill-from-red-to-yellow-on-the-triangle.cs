using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        try
        {
            // Create a new empty diagram
            Diagram diagram = new Diagram();

            // Get the first (default) page
            Page page = diagram.Pages[0];

            // Define triangle vertices (in inches)
            double x1 = 2.0, y1 = 2.0;
            double x2 = 4.0, y2 = 2.0;
            double x3 = 3.0, y3 = 4.0;

            // Draw the triangle using a closed polyline (repeat first point at the end)
            long triangleId = page.DrawPolyline(new double[]
            {
                x1, y1,
                x2, y2,
                x3, y3,
                x1, y1
            });

            // Retrieve the shape object (GetShape expects an int)
            Shape triangle = page.Shapes.GetShape((int)triangleId);

            // Apply a linear gradient fill from red to yellow
            // Set fill pattern to gradient (value 25)
            triangle.Fill.FillPattern.Value = 25;

            // Enable gradient fill
            triangle.Fill.GradientFill.GradientEnabled.Value = BOOL.True;

            // Set gradient direction to linear (cast enum to int for the cell value)
            triangle.Fill.GradientFill.GradientDir.Value = (int)GradientFillDir.Linear;

            // Clear any existing gradient stops
            triangle.Fill.GradientFill.GradientStops.Clear();

            // Add gradient stop at position 0 (red)
            triangle.Fill.GradientFill.GradientStops.Add(
                new DoubleValue(0, MeasureConst.NUM),
                new ColorValue("#FF0000", MeasureConst.Undefined));

            // Add gradient stop at position 1 (yellow)
            triangle.Fill.GradientFill.GradientStops.Add(
                new DoubleValue(1, MeasureConst.NUM),
                new ColorValue("#FFFF00", MeasureConst.Undefined));

            // Save the diagram to a VSDX file
            diagram.Save("Triangle.vsdx", SaveFileFormat.Vsdx);
        }
        catch (Exception ex)
        {
            // Write any errors to the error console
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}