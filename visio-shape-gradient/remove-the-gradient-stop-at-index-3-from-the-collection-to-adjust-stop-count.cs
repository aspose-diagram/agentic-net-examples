using System;
using System.Collections.Generic;
using Aspose.Diagram;

class Program
    {
        static void Main()
        {
            try
            {

                // Load the Visio diagram
                Diagram diagram = new Diagram("input.vsdx");

                // Assume we work on the first page
                var page = diagram.Pages[0];

                // Iterate through all shapes on the page
                foreach (Shape shape in page.Shapes)
                {
                    // Access the gradient fill of the shape
                    var gradientFill = shape.Fill?.GradientFill;
                    if (gradientFill == null)
                        continue;

                    // Ensure there are enough gradient stops to remove index 3
                    if (gradientFill.GradientStops == null || gradientFill.GradientStops.Count <= 3)
                        continue;

                    // Collect gradient stops except the one at index 3
                    List<GradientStop> keptStops = new List<GradientStop>();
                    int currentIndex = 0;
                    foreach (GradientStop stop in gradientFill.GradientStops)
                    {
                        if (currentIndex != 3)
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