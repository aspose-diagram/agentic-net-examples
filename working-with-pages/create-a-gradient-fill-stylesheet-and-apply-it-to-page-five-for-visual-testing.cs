using System.IO;
using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main()
    {
        // Create a new empty diagram
        using (Diagram diagram = new Diagram())
        {
            // Ensure the diagram has at least 5 pages
            while (diagram.Pages.Count < 5)
            {
                diagram.Pages.Add(new Page());
            }

            // Get the fifth page (index 4)
            Page page = diagram.Pages[4];

            // Draw a rectangle shape on the page
            // Parameters: pinX, pinY (center), width, height (in inches)
            long rectId = page.DrawRectangle(5.0, 5.0, 2.0, 1.0);

            // Retrieve the shape object using the returned ID
            Shape rectShape = page.Shapes.GetShape((int)rectId);

            // Apply a gradient fill to the rectangle
            // Set fill pattern to gradient (value 25)
            rectShape.Fill.FillPattern.Value = 25;

            // Enable gradient fill
            rectShape.Fill.GradientFill.GradientEnabled.Value = BOOL.True;

            // Set gradient direction (0 = horizontal, 1 = vertical, etc.)
            rectShape.Fill.GradientFill.GradientDir.Value = 0;

            // Clear any existing gradient stops
            rectShape.Fill.GradientFill.GradientStops.Clear();

            // Add gradient stop at position 0 (start) with red color
            rectShape.Fill.GradientFill.GradientStops.Add(
                new DoubleValue(0, MeasureConst.NUM),
                new ColorValue("#FF0000", MeasureConst.Undefined));

            // Add gradient stop at position 1 (end) with green color
            rectShape.Fill.GradientFill.GradientStops.Add(
                new DoubleValue(1, MeasureConst.NUM),
                new ColorValue("#00FF00", MeasureConst.Undefined));

            // Save the diagram to a VSDX file for visual testing
            diagram.Save("GradientTest.vsdx", SaveFileFormat.Vsdx);
        }
    }
}
