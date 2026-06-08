using System.IO;
using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

public class Program
{
    public static void Main()
    {
        try
        {

            // Load an existing Visio diagram
            Diagram diagram = new Diagram("input.vsdx");

            // Get the first page
            Page page = diagram.Pages[0];

            // Retrieve the first shape on the page
            Shape shape = page.Shapes[0];

            // Set fill pattern to gradient (value 25)
            shape.Fill.FillPattern.Value = 25;

            // Enable gradient fill
            shape.Fill.GradientFill.GradientEnabled.Value = BOOL.True;

            // Set gradient direction to diagonal (top‑left to bottom‑right)
            // Using RectangleFromTopLeft which represents a diagonal rectangle gradient
            shape.Fill.GradientFill.GradientDir.Value = (int)GradientFillDir.RectangleFromTopLeft;

            // Clear any existing gradient stops
            shape.Fill.GradientFill.GradientStops.Clear();

            // Add gradient stop at start (position 0) with red color
            shape.Fill.GradientFill.GradientStops.Add(
                new DoubleValue(0, MeasureConst.NUM),
                new ColorValue("#FF0000", MeasureConst.Undefined));

            // Add gradient stop at end (position 1) with green color
            shape.Fill.GradientFill.GradientStops.Add(
                new DoubleValue(1, MeasureConst.NUM),
                new ColorValue("#00FF00", MeasureConst.Undefined));

            // Save the modified diagram
            diagram.Save("output.vsdx", SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
