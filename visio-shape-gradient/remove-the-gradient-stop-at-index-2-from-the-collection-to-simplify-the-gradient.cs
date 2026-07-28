using System.IO;
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
            if (diagram.Pages.Count == 0)
            {
                Console.WriteLine("The diagram contains no pages.");
                return;
            }

            Page page = diagram.Pages[0];
            if (page.Shapes.Count == 0)
            {
                Console.WriteLine("The page contains no shapes.");
                return;
            }

            // Retrieve the first shape
            Shape shape = page.Shapes[0];

            // Ensure the shape has a gradient fill enabled
            shape.Fill.FillPattern.Value = 25; // Enable gradient fill pattern
            shape.Fill.GradientFill.GradientEnabled.Value = BOOL.True;

            // Access the gradient fill collection
            GradientFill gradientFill = shape.Fill.GradientFill;

            // Collect all gradient stops except the one at index 2
            var keptStops = new List<(double Position, string Color)>();
            int currentIndex = 0;
            foreach (GradientStop stop in gradientFill.GradientStops)
            {
                if (currentIndex != 2) // Skip the stop at index 2
                {
                    double pos = stop.Position.Value;
                    string col = stop.Color.Value;
                    keptStops.Add((pos, col));
                }
                currentIndex++;
            }

            // Clear existing stops and re-add the kept ones
            gradientFill.GradientStops.Clear();
            foreach (var item in keptStops)
            {
                gradientFill.GradientStops.Add(
                    new DoubleValue(item.Position, MeasureConst.NUM),
                    new ColorValue(item.Color, MeasureConst.Undefined));
            }

            // Save the modified diagram
            string outputPath = "output.vsdx";
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine("Gradient stop at index 2 removed and diagram saved to " + outputPath);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
