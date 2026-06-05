using System.IO;
using System;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        // Create a new empty diagram
        Diagram diagram = new Diagram();

        // Access the first page of the diagram
        Page page = diagram.Pages[0];

        // Add a rectangle shape at (2,2) with width 4 and height 2 inches
        long rectId = page.DrawRectangle(2.0, 2.0, 4.0, 2.0);
        Shape rect = page.Shapes.GetShape((int)rectId);

        // Apply a linear gradient fill from blue to green
        rect.Fill.FillPattern.Value = 25; // Gradient fill pattern
        rect.Fill.GradientFill.GradientEnabled.Value = BOOL.True;
        rect.Fill.GradientFill.GradientDir.Value = (int)GradientFillDir.Linear;
        rect.Fill.GradientFill.GradientStops.Clear();
        rect.Fill.GradientFill.GradientStops.Add(
            new DoubleValue(0, MeasureConst.NUM),
            new ColorValue("#0000FF", MeasureConst.Undefined));
        rect.Fill.GradientFill.GradientStops.Add(
            new DoubleValue(1, MeasureConst.NUM),
            new ColorValue("#00FF00", MeasureConst.Undefined));

        // Save the diagram to a VSDX file
        diagram.Save("GradientRectangle.vsdx", SaveFileFormat.Vsdx);
    }
}
