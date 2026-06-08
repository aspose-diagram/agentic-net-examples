using System.IO;
using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main()
    {
        try
        {

            // Load an existing Visio diagram
            Diagram diagram = new Diagram("input.vsdx");

            // Iterate through all pages and shapes
            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    // Ensure the shape uses a gradient fill
                    shape.Fill.FillPattern.Value = 25; // Gradient fill pattern
                    shape.Fill.GradientFill.GradientEnabled.Value = BOOL.True;

                    // Remove any existing gradient stops
                    shape.Fill.GradientFill.GradientStops.Clear();

                    // Add uniform gray stops at start (0) and end (1)
                    shape.Fill.GradientFill.GradientStops.Add(
                        new DoubleValue(0, MeasureConst.NUM),
                        new ColorValue("#808080", MeasureConst.Undefined));

                    shape.Fill.GradientFill.GradientStops.Add(
                        new DoubleValue(1, MeasureConst.NUM),
                        new ColorValue("#808080", MeasureConst.Undefined));
                }
            }

            // Save the modified diagram
            diagram.Save("output.vsdx", SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
