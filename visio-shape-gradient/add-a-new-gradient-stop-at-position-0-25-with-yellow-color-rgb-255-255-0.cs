using System.IO;
using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main()
    {
        try
        {

            // Load an existing Visio diagram (replace with your file path)
            string inputPath = "input.vsdx";
            Diagram diagram = new Diagram(inputPath);

            // Get the first page
            Page page = diagram.Pages[0];

            // Retrieve the first shape on the page (replace with specific shape ID if needed)
            Shape shape = null;
            foreach (Shape s in page.Shapes)
            {
                shape = s;
                break;
            }

            if (shape == null)
            {
                Console.WriteLine("No shape found on the page.");
                return;
            }

            // Enable gradient fill
            shape.Fill.FillPattern.Value = 25; // Gradient fill pattern
            shape.Fill.GradientFill.GradientEnabled.Value = BOOL.True;
            shape.Fill.GradientFill.GradientDir.Value = 0; // Direction (0 = left to right)

            // Clear existing gradient stops
            shape.Fill.GradientFill.GradientStops.Clear();

            // Add a new gradient stop at position 0.25 with yellow color (RGB 255,255,0)
            // Position uses MeasureConst.NUM (0 to 1 range)
            // Color uses a hex string and MeasureConst.Undefined
            shape.Fill.GradientFill.GradientStops.Add(
                new DoubleValue(0.25, MeasureConst.NUM),
                new ColorValue("#FFFF00", MeasureConst.Undefined));

            // Optionally, add additional stops (e.g., start and end) to see the gradient effect
            shape.Fill.GradientFill.GradientStops.Add(
                new DoubleValue(0.0, MeasureConst.NUM),
                new ColorValue("#FF0000", MeasureConst.Undefined)); // Red at start
            shape.Fill.GradientFill.GradientStops.Add(
                new DoubleValue(1.0, MeasureConst.NUM),
                new ColorValue("#0000FF", MeasureConst.Undefined)); // Blue at end

            // Save the modified diagram
            string outputPath = "output.vsdx";
            diagram.Save(outputPath, SaveFileFormat.Vsdx);

            Console.WriteLine("Gradient stop added and diagram saved to " + outputPath);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
