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
                // Replace "input.vsdx" with the path to your diagram file
                Diagram diagram = new Diagram("input.vsdx");

                // Access the first page (index 0)
                Page page = diagram.Pages[0];

                // Retrieve the shape you want to modify.
                // Replace the shape ID (e.g., 1) with the actual ID of your target shape.
                Shape shape = page.Shapes.GetShape(1);

                // Ensure the shape has a gradient fill enabled
                shape.Fill.FillPattern.Value = 25; // Enable gradient fill pattern
                shape.Fill.GradientFill.GradientEnabled.Value = BOOL.True;

                GradientFill gradientFill = shape.Fill.GradientFill;

                // Collect all gradient stops except the one at index 2
                List<GradientStop> keptStops = new List<GradientStop>();
                int currentIndex = 0;
                foreach (GradientStop stop in gradientFill.GradientStops)
                {
                    if (currentIndex != 2) // Skip the stop at index 2
                    {
                        keptStops.Add(stop);
                    }
                    currentIndex++;
                }

                // Clear existing stops and re-add the kept ones
                gradientFill.GradientStops.Clear();
                foreach (GradientStop stop in keptStops)
                {
                    // Re-add using the original position and color values
                    gradientFill.GradientStops.Add(stop.Position, stop.Color);
                }

                // Save the modified diagram
                // Replace "output.vsdx" with the desired output file path
                diagram.Save("output.vsdx", SaveFileFormat.Vsdx);

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }