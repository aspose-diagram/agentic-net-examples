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

            // Access the first page and retrieve a shape by its ID (example ID = 1)
            Page page = diagram.Pages[0];
            Shape shape = page.Shapes.GetShape(1);

            // Ensure the shape has a gradient fill enabled
            shape.Fill.FillPattern.Value = 25; // Gradient fill pattern
            shape.Fill.GradientFill.GradientEnabled.Value = BOOL.True;

            // Retrieve the current gradient stops collection
            GradientFill gradientFill = shape.Fill.GradientFill;
            int stopCount = gradientFill.GradientStops.Count;
            Console.WriteLine($"Current gradient stop count: {stopCount}");

            // If there are fewer than three stops, replace them with three default stops
            if (stopCount < 3)
            {
                // Clear existing stops
                gradientFill.GradientStops.Clear();

                // Add three gradient stops at positions 0, 0.5, and 1 with sample colors
                gradientFill.GradientStops.Add(
                    new DoubleValue(0, MeasureConst.NUM),
                    new ColorValue("#FF0000", MeasureConst.Undefined)); // Red at start

                gradientFill.GradientStops.Add(
                    new DoubleValue(0.5, MeasureConst.NUM),
                    new ColorValue("#00FF00", MeasureConst.Undefined)); // Green at middle

                gradientFill.GradientStops.Add(
                    new DoubleValue(1, MeasureConst.NUM),
                    new ColorValue("#0000FF", MeasureConst.Undefined)); // Blue at end

                Console.WriteLine("Added three gradient stops to meet the minimum requirement.");
            }

            // Verify the final count
            Console.WriteLine($"Final gradient stop count: {gradientFill.GradientStops.Count}");

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
