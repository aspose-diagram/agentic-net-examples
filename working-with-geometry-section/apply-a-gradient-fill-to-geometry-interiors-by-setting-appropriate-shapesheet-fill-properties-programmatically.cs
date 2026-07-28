using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
    {
        static void Main(string[] args)
        {
            // Create a new empty diagram
            Diagram diagram = new Diagram();

            // Get the first (default) page
            Page page = diagram.Pages[0];

            // Draw a rectangle shape on the page
            // Parameters: pinX, pinY, width, height (all in inches)
            long shapeIdLong = page.DrawRectangle(2.0, 2.0, 4.0, 3.0);

            // Retrieve the shape using its ID (cast to int as required by GetShape)
            Shape shape = page.Shapes.GetShape((int)shapeIdLong);

            // Apply gradient fill to the shape
            // Set fill pattern to gradient (value 25)
            shape.Fill.FillPattern.Value = 25;

            // Enable gradient fill
            shape.Fill.GradientFill.GradientEnabled.Value = BOOL.True;

            // Set gradient direction (0 = left-to-right, other values for different directions)
            shape.Fill.GradientFill.GradientDir.Value = 0;

            // Clear any existing gradient stops
            shape.Fill.GradientFill.GradientStops.Clear();

            // Add gradient stop at position 0 (start) with blue color
            shape.Fill.GradientFill.GradientStops.Add(
                new DoubleValue(0, MeasureConst.NUM),
                new ColorValue("#0000FF", MeasureConst.Undefined));

            // Add gradient stop at position 1 (end) with green color
            shape.Fill.GradientFill.GradientStops.Add(
                new DoubleValue(1, MeasureConst.NUM),
                new ColorValue("#00FF00", MeasureConst.Undefined));

            // Save the diagram to a VSDX file
            diagram.Save("GradientDiagram.vsdx", SaveFileFormat.Vsdx);

            Console.WriteLine("Diagram saved with gradient fill applied.");
        }
    }