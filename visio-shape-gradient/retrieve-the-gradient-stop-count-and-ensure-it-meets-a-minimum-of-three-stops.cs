using System.IO;
using System;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        try
        {

            // Paths to input and output Visio files
            string inputPath = "input.vsdx";
            string outputPath = "output.vsdx";

            // Load the diagram
            Diagram diagram = new Diagram(inputPath);

            // Access the first page and the first shape (ID = 1)
            Page page = diagram.Pages[0];
            Shape shape = page.Shapes.GetShape(1);

            if (shape != null && shape.Fill != null && shape.Fill.GradientFill != null)
            {
                var gradientFill = shape.Fill.GradientFill;

                // Retrieve current gradient stop count
                int stopCount = gradientFill.GradientStops.Count;
                Console.WriteLine($"Current gradient stop count: {stopCount}");

                // Ensure at least three stops
                if (stopCount < 3)
                {
                    // Clear existing stops before adding new ones
                    gradientFill.GradientStops.Clear();

                    // Add three default gradient stops
                    gradientFill.GradientStops.Add(
                        new DoubleValue(0, MeasureConst.NUM),
                        new ColorValue("#FF0000", MeasureConst.Undefined)); // Red at position 0

                    gradientFill.GradientStops.Add(
                        new DoubleValue(0.5, MeasureConst.NUM),
                        new ColorValue("#00FF00", MeasureConst.Undefined)); // Green at position 0.5

                    gradientFill.GradientStops.Add(
                        new DoubleValue(1, MeasureConst.NUM),
                        new ColorValue("#0000FF", MeasureConst.Undefined)); // Blue at position 1

                    Console.WriteLine("Added default gradient stops to ensure a minimum of three stops.");
                }
            }
            else
            {
                Console.WriteLine("The specified shape does not have a gradient fill.");
            }

            // Save the modified diagram
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Diagram saved to: {outputPath}");

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
