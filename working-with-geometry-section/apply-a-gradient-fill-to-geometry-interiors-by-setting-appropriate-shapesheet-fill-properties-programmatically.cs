using System;
using Aspose.Diagram;

class Program
    {
        static void Main(string[] args)
        {
            try
            {

                // Create a new empty diagram
                Diagram diagram = new Diagram();

                // Access the first (default) page
                Page page = diagram.Pages[0];

                // Add a rectangle shape to the page (pinX, pinY, width, height, master name)
                // The AddShape method returns the shape ID (long)
                long shapeId = page.AddShape(2.0, 2.0, 2.0, 1.0, "Rectangle");

                // Retrieve the shape instance using the returned ID
                Shape shape = page.Shapes.GetShape(shapeId);

                // Apply gradient fill
                // 1. Set fill pattern to gradient (value 25)
                shape.Fill.FillPattern.Value = 25;

                // 2. Enable gradient
                shape.Fill.GradientFill.GradientEnabled.Value = BOOL.True;

                // 3. Set gradient direction (0 = horizontal, 1 = vertical, etc.)
                shape.Fill.GradientFill.GradientDir.Value = 0;

                // 4. Clear any existing gradient stops
                shape.Fill.GradientFill.GradientStops.Clear();

                // 5. Add gradient stops (position, color)
                // Position is a DoubleValue (0 to 1) with MeasureConst.NUM unit
                // Color is a ColorValue with a hex string and MeasureConst.Undefined unit
                shape.Fill.GradientFill.GradientStops.Add(
                    new DoubleValue(0, MeasureConst.NUM),
                    new ColorValue("#0000FF", MeasureConst.Undefined)); // Blue at start

                shape.Fill.GradientFill.GradientStops.Add(
                    new DoubleValue(1, MeasureConst.NUM),
                    new ColorValue("#00FF00", MeasureConst.Undefined)); // Green at end

                // Save the diagram to a VSDX file
                diagram.Save("GradientShape.vsdx", SaveFileFormat.Vsdx);

                Console.WriteLine("Diagram saved with gradient fill applied.");

            }
            catch (Aspose.Diagram.DiagramException ex)
            {
                Console.Error.WriteLine($"[DiagramException] {ex.Message}");
            }
    }
    }