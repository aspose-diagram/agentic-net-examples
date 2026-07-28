using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
    {
        static void Main()
        {
            try
            {

                // Create a new empty diagram
                Diagram diagram = new Diagram();

                // Get the first (default) page
                Page page = diagram.Pages[0];

                // Add a rectangle shape to the page
                long shapeId = page.AddShape(2.0, 2.0, "Rectangle");
                Shape shape = page.Shapes.GetShape((int)shapeId);

                // Enable gradient fill
                shape.Fill.FillPattern.Value = 25; // Gradient fill pattern
                shape.Fill.GradientFill.GradientEnabled.Value = BOOL.True;

                // Set gradient direction to horizontal (left‑to‑right)
                // 0 = left‑to‑right, 1 = top‑to‑bottom, etc.
                shape.Fill.GradientFill.GradientDir.Value = 0;

                // Clear any existing gradient stops
                shape.Fill.GradientFill.GradientStops.Clear();

                // Add gradient stops (red at start, green at end)
                shape.Fill.GradientFill.GradientStops.Add(
                    new DoubleValue(0, MeasureConst.NUM),
                    new ColorValue("#FF0000", MeasureConst.Undefined));

                shape.Fill.GradientFill.GradientStops.Add(
                    new DoubleValue(1, MeasureConst.NUM),
                    new ColorValue("#00FF00", MeasureConst.Undefined));

                // Save the diagram to a VSDX file
                diagram.Save("GradientShape.vsdx", SaveFileFormat.Vsdx);

            }
            catch (Aspose.Diagram.DiagramException ex)
            {
                Console.Error.WriteLine($"[DiagramException] {ex.Message}");
            }
    }
    }