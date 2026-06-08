using System;
using System.Collections.Generic;
using Aspose.Diagram;

class Program
    {
        static void Main()
        {
            try
            {

                // Input and output file paths
                string inputPath = "input.vsdx";
                string outputPath = "output.vsdx";

                // Load the diagram
                Diagram diagram = new Diagram(inputPath);

                // Assume we work with the first page
                if (diagram.Pages.Count == 0)
                {
                    Console.WriteLine("The diagram contains no pages.");
                    return;
                }

                var page = diagram.Pages[0];

                // Find the first shape that has a gradient fill enabled
                Shape targetShape = null;
                foreach (Shape shape in page.Shapes)
                {
                    if (shape.Fill != null &&
                        shape.Fill.GradientFill != null &&
                        shape.Fill.GradientFill.GradientEnabled != null &&
                        shape.Fill.GradientFill.GradientEnabled.Value == BOOL.True)
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

                // Retrieve existing gradient stops
                var gradientFill = targetShape.Fill.GradientFill;
                var originalStops = new List<(DoubleValue position, ColorValue color)>();

                foreach (GradientStop stop in gradientFill.GradientStops)
                {
                    originalStops.Add((stop.Position, stop.Color));
                }

                if (originalStops.Count == 0)
                {
                    Console.WriteLine("The selected shape has no gradient stops.");
                    return;
                }

                // Modify the position of the stop at index 0 to 0.0 (start)
                var modifiedStops = new List<(DoubleValue position, ColorValue color)>();
                for (int i = 0; i < originalStops.Count; i++)
                {
                    var (pos, col) = originalStops[i];
                    if (i == 0)
                    {
                        // Set position to 0.0 using MeasureConst.NUM
                        pos = new DoubleValue(0.0, MeasureConst.NUM);
                    }
                    modifiedStops.Add((pos, col));
                }

                // Clear existing stops and re-add the modified collection
                gradientFill.GradientStops.Clear();
                foreach (var (pos, col) in modifiedStops)
                {
                    gradientFill.GradientStops.Add(pos, col);
                }

                // Save the modified diagram
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
                Console.WriteLine($"Gradient stop updated and diagram saved to '{outputPath}'.");

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }