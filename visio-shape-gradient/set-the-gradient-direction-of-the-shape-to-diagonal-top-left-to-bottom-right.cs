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

            // Load an existing Visio diagram (or create a new one)
            string inputPath = "input.vsdx";
            Diagram diagram = new Diagram(inputPath);

            // Add a rectangle shape to the active page
            // AddShape returns the shape ID (long)
            long rectId = diagram.ActivePage.AddShape(2.0, 2.0, "Rectangle");

            // Retrieve the Shape object using the ID
            Shape rectShape = diagram.ActivePage.Shapes.GetShape((int)rectId);

            // Enable gradient fill
            rectShape.Fill.FillPattern.Value = 25; // 25 = gradient fill pattern
            rectShape.Fill.GradientFill.GradientEnabled.Value = BOOL.True;

            // Set gradient direction to diagonal (top‑left to bottom‑right)
            // Direction values: 0 = left‑right, 1 = top‑bottom, 2 = diagonal TL‑BR, 3 = diagonal BL‑TR
            rectShape.Fill.GradientFill.GradientDir.Value = 2;

            // Clear any existing gradient stops
            rectShape.Fill.GradientFill.GradientStops.Clear();

            // Add gradient stop at start (position 0) – blue
            rectShape.Fill.GradientFill.GradientStops.Add(
                new DoubleValue(0.0, MeasureConst.NUM),
                new ColorValue("#0000FF", MeasureConst.Undefined));

            // Add gradient stop at end (position 1) – green
            rectShape.Fill.GradientFill.GradientStops.Add(
                new DoubleValue(1.0, MeasureConst.NUM),
                new ColorValue("#00FF00", MeasureConst.Undefined));

            // Save the modified diagram
            string outputPath = "output.vsdx";
            diagram.Save(outputPath, SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
