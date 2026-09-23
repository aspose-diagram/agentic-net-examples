using System;
using Aspose.Diagram;

class Program
    {
        static void Main()
        {
            try
            {

                // Create a new diagram
                Diagram diagram = new Diagram();

                // Access the first page
                Page page = diagram.Pages[0];

                // Add a rectangle shape (pinX, pinY, width, height, master name, isCalculate)
                long shapeId = page.AddShape(2.0, 2.0, 2.0, 1.0, "Rectangle", false);
                Shape shape = page.Shapes.GetShape(shapeId);

                // Enable gradient fill
                shape.Fill.FillPattern.Value = 25; // Gradient fill pattern
                shape.Fill.GradientFill.GradientEnabled.Value = BOOL.True;
                shape.Fill.GradientFill.GradientDir.Value = 0; // Horizontal direction

                // Define gradient stops
                shape.Fill.GradientFill.GradientStops.Clear();
                shape.Fill.GradientFill.GradientStops.Add(
                    new DoubleValue(0, MeasureConst.NUM),
                    new ColorValue("#FF0000", MeasureConst.Undefined)); // Start color (red)
                shape.Fill.GradientFill.GradientStops.Add(
                    new DoubleValue(1, MeasureConst.NUM),
                    new ColorValue("#0000FF", MeasureConst.Undefined)); // End color (blue)

                // Set gradient angle to 45 degrees for diagonal effect
                shape.Fill.GradientFill.GradientAngle.Value = 45;

                // Save the diagram
                diagram.Save("GradientShape.vsdx", SaveFileFormat.Vsdx);

            }
            catch (Aspose.Diagram.DiagramException ex)
            {
                Console.Error.WriteLine($"[DiagramException] {ex.Message}");
            }
    }
    }