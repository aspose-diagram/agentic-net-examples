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

                // Assume we work with the first page and the first shape on that page
                Page page = diagram.Pages[0];
                // Replace 1 with the actual shape ID you want to modify
                Shape shape = page.Shapes.GetShape(1);

                // Access the gradient fill stops collection
                GradientFill gradientFill = shape.Fill.GradientFill;
                GradientStopCollection stops = gradientFill.GradientStops;

                // Store all stops except the one at index 3 (zero‑based)
                List<GradientStop> keptStops = new List<GradientStop>();
                int currentIndex = 0;
                foreach (GradientStop stop in stops)
                {
                    if (currentIndex != 3)
                    {
                        keptStops.Add(stop);
                    }
                    currentIndex++;
                }

                // Clear the existing stops and re‑add the kept ones
                stops.Clear();
                foreach (GradientStop stop in keptStops)
                {
                    // Add using the original position and color values
                    stops.Add(stop.Position, stop.Color);
                }

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