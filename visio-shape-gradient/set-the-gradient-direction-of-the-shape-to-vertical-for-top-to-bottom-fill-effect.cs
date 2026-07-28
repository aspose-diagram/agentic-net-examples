using System;
using Aspose.Diagram;

class Program
    {
        static void Main()
        {
            try
            {

                // Create a new empty diagram
                Diagram diagram = new Diagram();

                // Add a rectangle shape to the first page
                // Parameters: PinX, PinY, Width, Height, Master name
                long shapeId = diagram.Pages[0].AddShape(2.0, 2.0, 2.0, 1.0, "Rectangle");

                // Retrieve the shape instance
                Shape shape = diagram.Pages[0].Shapes.GetShape((int)shapeId);

                // Enable gradient fill
                shape.Fill.FillPattern.Value = 25;                     // Gradient fill pattern
                shape.Fill.GradientFill.GradientEnabled.Value = BOOL.True;

                // Set gradient direction to vertical (top‑to‑bottom)
                // 0 = Horizontal, 1 = Vertical, 2 = Diagonal (TL‑BR), etc.
                shape.Fill.GradientFill.GradientDir.Value = 1;

                // Define gradient stops (optional, but demonstrates the effect)
                shape.Fill.GradientFill.GradientStops.Clear();
                // Top stop (position 0) – blue
                shape.Fill.GradientFill.GradientStops.Add(
                    new DoubleValue(0, MeasureConst.NUM),
                    new ColorValue("#0000FF", MeasureConst.Undefined));
                // Bottom stop (position 1) – green
                shape.Fill.GradientFill.GradientStops.Add(
                    new DoubleValue(1, MeasureConst.NUM),
                    new ColorValue("#00FF00", MeasureConst.Undefined));

                // Save the diagram to a VSDX file
                diagram.Save("GradientVertical.vsdx", SaveFileFormat.Vsdx);

            }
            catch (Aspose.Diagram.DiagramException ex)
            {
                Console.Error.WriteLine($"[DiagramException] {ex.Message}");
            }
    }
    }