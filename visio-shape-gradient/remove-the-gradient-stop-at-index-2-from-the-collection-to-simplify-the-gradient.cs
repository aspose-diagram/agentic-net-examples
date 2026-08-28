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
                string inputPath = "input.vsdx"; // TODO: replace with actual file path
                Diagram diagram = new Diagram(inputPath);

                // Iterate through all pages and shapes
                foreach (Page page in diagram.Pages)
                {
                    foreach (Shape shape in page.Shapes)
                    {
                        // Ensure the shape has a gradient fill enabled
                        if (shape.Fill.GradientFill.GradientEnabled.Value == BOOL.True)
                        {
                            var gradientFill = shape.Fill.GradientFill;
                            var stops = gradientFill.GradientStops;

                            // Collect stops except the one at index 2
                            List<GradientStop> keptStops = new List<GradientStop>();
                            int currentIndex = 0;
                            foreach (GradientStop stop in stops)
                            {
                                if (currentIndex != 2)
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
                    }
                }

                // Save the modified diagram
                string outputPath = "output.vsdx"; // TODO: replace with desired output path
                diagram.Save(outputPath, SaveFileFormat.Vsdx);

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }