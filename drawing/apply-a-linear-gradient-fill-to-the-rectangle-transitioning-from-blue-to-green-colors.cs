using System;
using Aspose.Diagram;

class Program
    {
        static void Main()
        {
            // Create a new empty diagram
            Diagram diagram = new Diagram();

            // Use the first page (a new diagram contains one default page)
            Page page = diagram.Pages[0];

            // Draw a rectangle shape (pinX, pinY, width, height)
            // This method returns the shape ID (long)
            long rectId = page.DrawRectangle(2.0, 2.0, 4.0, 3.0);

            // Retrieve the shape object using the ID
            Shape rectShape = page.Shapes.GetShape((int)rectId);

            // Apply a linear gradient fill (blue to green)
            // Set fill pattern to gradient (value 25)
            rectShape.Fill.FillPattern.Value = 25;

            // Enable gradient fill
            rectShape.Fill.GradientFill.GradientEnabled.Value = BOOL.True;

            // Set gradient direction (0 = left to right)
            rectShape.Fill.GradientFill.GradientDir.Value = 0;

            // Clear any existing gradient stops
            rectShape.Fill.GradientFill.GradientStops.Clear();

            // Add gradient stop at position 0 (blue)
            rectShape.Fill.GradientFill.GradientStops.Add(
                new DoubleValue(0, MeasureConst.NUM),
                new ColorValue("#0000FF", MeasureConst.Undefined));

            // Add gradient stop at position 1 (green)
            rectShape.Fill.GradientFill.GradientStops.Add(
                new DoubleValue(1, MeasureConst.NUM),
                new ColorValue("#00FF00", MeasureConst.Undefined));

            // Save the diagram to a VSDX file
            diagram.Save("GradientRectangle.vsdx", SaveFileFormat.Vsdx);
        }
    }