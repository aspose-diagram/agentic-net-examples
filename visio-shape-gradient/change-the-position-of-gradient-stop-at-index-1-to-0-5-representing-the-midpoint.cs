using System;
using System.Collections.Generic;
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

                // Get the first page
                Page page = diagram.Pages[0];

                // Find the first shape that has a gradient fill
                Shape targetShape = null;
                foreach (Shape shape in page.Shapes)
                {
                    // Ensure the shape has a gradient fill enabled
                    if (shape.Fill != null && shape.Fill.GradientFill != null && shape.Fill.GradientFill.GradientEnabled.Value == BOOL.True)
                    {
                        targetShape = shape;
                        break;
                    }
                }

                if (targetShape == null)
                {
                    Console.WriteLine("No shape with an enabled gradient fill was found.");
                    return;
                }

                // Access the gradient fill collection
                var gradientFill = targetShape.Fill.GradientFill;
                var existingStops = new List<(double position, string color)>();
                int index = 0;

                // Preserve existing stops, modifying the one at index 1
                foreach (GradientStop stop in gradientFill.GradientStops)
                {
                    double pos = stop.Position.Value;
                    string col = stop.Color.Value;

                    if (index == 1)
                    {
                        // Change position to midpoint (0.5)
                        pos = 0.5;
                    }

                    existingStops.Add((pos, col));
                    index++;
                }

                // Clear current stops and re-add them with the updated position
                gradientFill.GradientStops.Clear();
                foreach (var (pos, col) in existingStops)
                {
                    gradientFill.GradientStops.Add(
                        new DoubleValue(pos, MeasureConst.NUM),
                        new ColorValue(col, MeasureConst.Undefined));
                }

                // Save the modified diagram
                string outputPath = "output.vsdx";
                diagram.Save(outputPath, SaveFileFormat.Vsdx);

                Console.WriteLine($"Gradient stop at index 1 updated and diagram saved to '{outputPath}'.");

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }