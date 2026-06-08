using System;
using System.Collections.Generic;
using Aspose.Diagram;

class Program
    {
        static void Main()
        {
            try
            {

                // Load the diagram from a file
                Diagram diagram = new Diagram("input.vsdx");

                // Access the first page
                Page page = diagram.Pages[0];

                // Retrieve the first shape on the page
                Shape shape = page.Shapes[0];

                // Ensure the shape has a gradient fill enabled
                shape.Fill.FillPattern.Value = 25;
                shape.Fill.GradientFill.GradientEnabled.Value = BOOL.True;

                // Get the gradient fill object
                GradientFill gradientFill = shape.Fill.GradientFill;

                // Store existing gradient stops, modifying the stop at index 1 to position 0.5
                var updatedStops = new List<(double Position, string Color)>();
                int currentIndex = 0;
                foreach (GradientStop stop in gradientFill.GradientStops)
                {
                    double position = stop.Position.Value;
                    string color = stop.Color.Value;

                    if (currentIndex == 1)
                    {
                        // Change position of the second stop (index 1) to the midpoint
                        position = 0.5;
                    }

                    updatedStops.Add((position, color));
                    currentIndex++;
                }

                // Clear existing stops and re-add the updated collection
                gradientFill.GradientStops.Clear();
                foreach (var stopInfo in updatedStops)
                {
                    gradientFill.GradientStops.Add(
                        new DoubleValue(stopInfo.Position, MeasureConst.NUM),
                        new ColorValue(stopInfo.Color, MeasureConst.Undefined));
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