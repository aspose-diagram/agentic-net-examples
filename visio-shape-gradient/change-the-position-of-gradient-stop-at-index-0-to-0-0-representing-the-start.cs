using System.IO;
using System;
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

            // Access the first page
            Page page = diagram.Pages[0];

            // Ensure there is at least one shape on the page
            if (page.Shapes.Count == 0)
            {
                Console.WriteLine("No shapes found on the page.");
                return;
            }

            // Retrieve the first shape (using GetShape as recommended)
            Shape shape = page.Shapes.GetShape(page.Shapes[0].ID);

            // Ensure the shape has a gradient fill enabled
            shape.Fill.FillPattern.Value = 25; // Gradient fill pattern
            shape.Fill.GradientFill.GradientEnabled.Value = BOOL.True;

            // Access the gradient fill and its stops collection
            var gradientFill = shape.Fill.GradientFill;
            var stopsCollection = gradientFill.GradientStops;

            // Store existing gradient stops
            var existingStops = new System.Collections.Generic.List<GradientStop>();
            foreach (GradientStop stop in stopsCollection)
            {
                existingStops.Add(stop);
            }

            // Clear all existing stops
            stopsCollection.Clear();

            // Re‑add stops, setting the position of the first stop to 0.0
            for (int i = 0; i < existingStops.Count; i++)
            {
                GradientStop oldStop = existingStops[i];
                double newPosition = (i == 0) ? 0.0 : oldStop.Position.Value;

                gradientFill.GradientStops.Add(
                    new DoubleValue(newPosition, MeasureConst.NUM),
                    new ColorValue(oldStop.Color.Value, MeasureConst.Undefined));
            }

            // Save the modified diagram
            string outputPath = "output.vsdx";
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine("Gradient stop at index 0 updated to position 0.0 and diagram saved.");

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
