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

            // Retrieve the first shape on the page
            Shape targetShape = null;
            foreach (Shape shp in page.Shapes)
            {
                targetShape = shp;
                break;
            }

            if (targetShape == null)
            {
                Console.WriteLine("No shapes found on the first page.");
                return;
            }

            // Apply a gradient fill to the shape
            // Set fill pattern to Gradient (value 25)
            targetShape.Fill.FillPattern.Value = 25;

            // Enable gradient fill
            targetShape.Fill.GradientFill.GradientEnabled.Value = BOOL.True;

            // Set gradient direction (0 = left‑to‑right, for example)
            targetShape.Fill.GradientFill.GradientDir.Value = 0;

            // Clear any existing gradient stops
            targetShape.Fill.GradientFill.GradientStops.Clear();

            // Add new gradient stops (position, color)
            targetShape.Fill.GradientFill.GradientStops.Add(
                new DoubleValue(0, MeasureConst.NUM),
                new ColorValue("#0000FF", MeasureConst.Undefined)); // Blue at start

            targetShape.Fill.GradientFill.GradientStops.Add(
                new DoubleValue(1, MeasureConst.NUM),
                new ColorValue("#00FF00", MeasureConst.Undefined)); // Green at end

            // Save the modified diagram
            string outputPath = "output.vsdx";
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Diagram saved to '{outputPath}'.");

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
