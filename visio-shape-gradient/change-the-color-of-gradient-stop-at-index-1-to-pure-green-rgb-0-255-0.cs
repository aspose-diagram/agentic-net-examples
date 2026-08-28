using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Define input file path and verify existence
        string inputPath = "input.vsdx";
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        try
        {
            // Load the diagram from the specified file
            Diagram diagram = new Diagram(inputPath);

            // Retrieve shape with ID 1 from the active page
            Shape shape = diagram.ActivePage.Shapes.GetShape(1);

            // Enable gradient fill on the shape
            shape.Fill.FillPattern.Value = 25;               // Gradient fill pattern
            shape.Fill.GradientFill.GradientEnabled.Value = BOOL.True;
            shape.Fill.GradientFill.GradientDir.Value = 0;   // Optional direction

            // Access the gradient fill object for convenience
            var gradientFill = shape.Fill.GradientFill;

            // Prepare a list to hold updated stop data (position and color)
            var updatedStops = new List<(double Position, string ColorHex)>();
            int currentIndex = 0;

            // Iterate through existing gradient stops
            foreach (GradientStop stop in gradientFill.GradientStops)
            {
                double position = stop.Position.Value;   // Preserve original position
                string colorHex = stop.Color.Value;      // Preserve original color

                // If this is the stop at index 1, change its color to pure green
                if (currentIndex == 1)
                {
                    colorHex = "#00FF00";
                }

                // Store the (possibly) modified stop data
                updatedStops.Add((position, colorHex));
                currentIndex++;
            }

            // Remove all existing stops
            gradientFill.GradientStops.Clear();

            // Re‑add stops using the correct Add method (no direct GradientStop construction)
            foreach (var (Position, ColorHex) in updatedStops)
            {
                gradientFill.GradientStops.Add(
                    new DoubleValue(Position, MeasureConst.NUM),
                    new ColorValue(ColorHex, MeasureConst.Undefined));
            }

            // Save the modified diagram to a new file
            diagram.Save("output.vsdx", SaveFileFormat.Vsdx);
        }
        catch (Exception ex)
        {
            // Output any Aspose or I/O errors to the error stream
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}