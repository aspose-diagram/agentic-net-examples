using System.IO;
using System;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        try
        {

            // Paths to the source and destination Visio files
            string inputPath = "input.vsdx";
            string outputPath = "output.vsdx";

            // Load an existing diagram
            Diagram diagram = new Diagram(inputPath);

            // Work with the first page
            Page page = diagram.Pages[0];

            // Add a rectangle shape (you can replace this with an existing shape ID)
            long shapeId = page.AddShape(2.0, 2.0, "Rectangle");
            Shape shape = page.Shapes.GetShape((int)shapeId);

            // Enable gradient fill and set it to horizontal (0 degrees)
            shape.Fill.FillPattern.Value = 25;                         // Gradient fill pattern
            shape.Fill.GradientFill.GradientEnabled.Value = BOOL.True; // Turn on gradient
            shape.Fill.GradientFill.GradientAngle.Value = 0;           // 0° = horizontal

            // Optional: define gradient stops (e.g., red to green)
            shape.Fill.GradientFill.GradientStops.Clear();
            shape.Fill.GradientFill.GradientStops.Add(
                new DoubleValue(0, MeasureConst.NUM),
                new ColorValue("#FF0000", MeasureConst.Undefined));
            shape.Fill.GradientFill.GradientStops.Add(
                new DoubleValue(1, MeasureConst.NUM),
                new ColorValue("#00FF00", MeasureConst.Undefined));

            // Save the modified diagram
            diagram.Save(outputPath, SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
