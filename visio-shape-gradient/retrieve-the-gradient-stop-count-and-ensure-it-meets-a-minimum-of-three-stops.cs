using System.IO;
using System;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        try
        {

            // Load an existing Visio diagram
            string inputPath = "input.vsdx";
            Diagram diagram = new Diagram(inputPath);

            // Access the first shape on the first page (shape IDs start at 1)
            Shape shape = diagram.Pages[0].Shapes.GetShape(1);

            // Ensure the shape is set up for gradient fill
            shape.Fill.FillPattern.Value = 25; // gradient fill pattern
            shape.Fill.GradientFill.GradientEnabled.Value = BOOL.True;
            shape.Fill.GradientFill.GradientDir.Value = (int)GradientFillDir.Linear;

            // Retrieve the current number of gradient stops
            int stopCount = shape.Fill.GradientFill.GradientStops.Count;
            Console.WriteLine($"Current gradient stop count: {stopCount}");

            // Ensure there are at least three gradient stops
            if (stopCount < 3)
            {
                // Clear any existing stops
                shape.Fill.GradientFill.GradientStops.Clear();

                // Add three stops: start (red), middle (green), end (blue)
                shape.Fill.GradientFill.GradientStops.Add(
                    new DoubleValue(0, MeasureConst.NUM),
                    new ColorValue("#FF0000", MeasureConst.Undefined));

                shape.Fill.GradientFill.GradientStops.Add(
                    new DoubleValue(0.5, MeasureConst.NUM),
                    new ColorValue("#00FF00", MeasureConst.Undefined));

                shape.Fill.GradientFill.GradientStops.Add(
                    new DoubleValue(1, MeasureConst.NUM),
                    new ColorValue("#0000FF", MeasureConst.Undefined));

                Console.WriteLine("Added missing gradient stops to meet the minimum of three.");
            }

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
