using System;
using Aspose.Diagram;

class Program
    {
        static void Main()
        {
            try
            {

                // Load an existing Visio diagram
                Diagram diagram = new Diagram("input.vsdx");

                // Access the first page and a shape (adjust indices as needed)
                var page = diagram.Pages[0];
                var shape = page.Shapes.GetShape(1);

                // Enable gradient fill on the shape
                shape.Fill.FillPattern.Value = 25; // Gradient fill pattern
                shape.Fill.GradientFill.GradientEnabled.Value = BOOL.True;
                shape.Fill.GradientFill.GradientDir.Value = 0; // Direction (0 = left to right)

                // Clear any existing gradient stops
                shape.Fill.GradientFill.GradientStops.Clear();

                // Add a gradient stop at position 0 with pure red color (RGB 255,0,0)
                shape.Fill.GradientFill.GradientStops.Add(
                    new DoubleValue(0, MeasureConst.NUM),
                    new ColorValue("#FF0000", MeasureConst.Undefined));

                // (Optional) Add a second stop to complete the gradient
                shape.Fill.GradientFill.GradientStops.Add(
                    new DoubleValue(1, MeasureConst.NUM),
                    new ColorValue("#FFFFFF", MeasureConst.Undefined));

                // Save the modified diagram
                diagram.Save("output.vsdx", SaveFileFormat.Vsdx);

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }