using System;
using Aspose.Diagram;

class Program
    {
        static void Main()
        {
            // Create a new empty diagram
            Diagram diagram = new Diagram();

            // Add a new page to the diagram
            diagram.Pages.Add(new Page());
            Page page = diagram.Pages[0];

            // Draw a rectangle shape on the page
            // Parameters: pinX, pinY, width, height
            long rectId = page.DrawRectangle(2.0, 2.0, 4.0, 2.0);
            Shape rectShape = page.Shapes.GetShape(rectId);

            // Apply gradient fill to the rectangle
            // Set fill pattern to gradient (value 25)
            rectShape.Fill.FillPattern.Value = 25;

            // Enable gradient fill
            rectShape.Fill.GradientFill.GradientEnabled.Value = BOOL.True;

            // Set gradient direction (0 = horizontal)
            rectShape.Fill.GradientFill.GradientDir.Value = 0;

            // Clear any existing gradient stops
            rectShape.Fill.GradientFill.GradientStops.Clear();

            // Add gradient stop at position 0 (red)
            rectShape.Fill.GradientFill.GradientStops.Add(
                new DoubleValue(0, MeasureConst.NUM),
                new ColorValue("#FF0000", MeasureConst.Undefined));

            // Add gradient stop at position 1 (green)
            rectShape.Fill.GradientFill.GradientStops.Add(
                new DoubleValue(1, MeasureConst.NUM),
                new ColorValue("#00FF00", MeasureConst.Undefined));

            // Save the diagram to a VSDX file
            diagram.Save("GradientRectangle.vsdx", SaveFileFormat.Vsdx);
        }
    }