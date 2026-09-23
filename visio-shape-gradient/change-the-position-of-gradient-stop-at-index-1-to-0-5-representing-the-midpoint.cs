using System;
using System.Collections.Generic;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
    {
        static void Main()
        {
            try
            {

                // Load an existing Visio diagram
                Diagram diagram = new Diagram("input.vsdx");

                // Access the first page
                Page page = diagram.Pages[0];

                // Retrieve the first shape on the page (adjust as needed)
                Shape shape = null;
                foreach (Shape s in page.Shapes)
                {
                    shape = s;
                    break;
                }

                if (shape == null)
                {
                    throw new Exception("No shape found on the first page.");
                }

                // Ensure the shape has a gradient fill
                shape.Fill.FillPattern.Value = 25;                         // Gradient fill pattern
                shape.Fill.GradientFill.GradientEnabled.Value = BOOL.True; // Enable gradient
                shape.Fill.GradientFill.GradientDir.Value = 0;             // Direction (optional)

                // Capture existing gradient stops
                List<(double Position, string Color)> stops = new List<(double, string)>();
                foreach (GradientStop stop in shape.Fill.GradientFill.GradientStops)
                {
                    double pos = stop.Position.Value;
                    string col = stop.Color.Value;
                    stops.Add((pos, col));
                }

                // Verify there is a stop at index 1
                if (stops.Count <= 1)
                {
                    throw new Exception("The shape does not have a gradient stop at index 1.");
                }

                // Change the position of the stop at index 1 to 0.5 (midpoint)
                var modifiedStop = stops[1];
                modifiedStop.Position = 0.5;
                stops[1] = modifiedStop;

                // Reapply the gradient stops
                shape.Fill.GradientFill.GradientStops.Clear();
                foreach (var stopInfo in stops)
                {
                    shape.Fill.GradientFill.GradientStops.Add(
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