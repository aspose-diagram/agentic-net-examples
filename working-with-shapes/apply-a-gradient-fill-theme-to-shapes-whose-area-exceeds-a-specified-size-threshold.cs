using System.IO;
using System;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        try
        {

            // Input and output Visio files
            string inputPath = "input.vsdx";
            string outputPath = "output.vsdx";

            // Load the diagram
            Diagram diagram = new Diagram(inputPath);

            // Area threshold in square inches (adjust as needed)
            double areaThreshold = 4.0;

            // Iterate through all pages and shapes
            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    // Skip shapes that are marked for deletion
                    if (shape.Del == BOOL.True)
                        continue;

                    // Compute shape area (width * height)
                    double width = shape.XForm.Width.Value;
                    double height = shape.XForm.Height.Value;
                    double area = width * height;

                    // Apply gradient fill if area exceeds the threshold
                    if (area > areaThreshold)
                    {
                        // Set fill pattern to gradient
                        shape.Fill.FillPattern.Value = 25; // Gradient pattern identifier
                        shape.Fill.GradientFill.GradientEnabled.Value = BOOL.True;
                        shape.Fill.GradientFill.GradientDir.Value = 0; // 0 = left‑to‑right

                        // Remove any existing gradient stops
                        shape.Fill.GradientFill.GradientStops.Clear();

                        // Add gradient stops (example: blue to green)
                        shape.Fill.GradientFill.GradientStops.Add(
                            new DoubleValue(0, MeasureConst.NUM),
                            new ColorValue("#0000FF", MeasureConst.Undefined));

                        shape.Fill.GradientFill.GradientStops.Add(
                            new DoubleValue(1, MeasureConst.NUM),
                            new ColorValue("#00FF00", MeasureConst.Undefined));
                    }
                }
            }

            // Save the modified diagram
            diagram.Save(outputPath, SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
