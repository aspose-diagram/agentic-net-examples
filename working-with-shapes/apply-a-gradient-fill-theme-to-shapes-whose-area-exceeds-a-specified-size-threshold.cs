using System.IO;
using System;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        try
        {

            // Input and output file paths
            const string inputPath = "input.vsdx";
            const string outputPath = "output.vsdx";

            // Area threshold in square inches (adjust as needed)
            double areaThreshold = 2.0;

            // Load the Visio diagram
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Iterate over all pages in the diagram
                foreach (Page page in diagram.Pages)
                {
                    // Iterate over all shapes on the current page
                    foreach (Shape shape in page.Shapes)
                    {
                        // Skip shapes that are marked as deleted
                        if (shape.Del == BOOL.True)
                            continue;

                        // Retrieve shape dimensions
                        double width = shape.XForm.Width.Value;
                        double height = shape.XForm.Height.Value;

                        // Compute shape area
                        double area = width * height;

                        // Apply gradient fill if the area exceeds the threshold
                        if (area > areaThreshold)
                        {
                            // Set fill pattern to gradient (value 25)
                            shape.Fill.FillPattern.Value = 25;

                            // Enable gradient fill
                            shape.Fill.GradientFill.GradientEnabled.Value = BOOL.True;

                            // Set gradient direction (0 = left to right)
                            shape.Fill.GradientFill.GradientDir.Value = 0;

                            // Remove any existing gradient stops
                            shape.Fill.GradientFill.GradientStops.Clear();

                            // Add gradient stops (blue at start, green at end)
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

            Console.WriteLine("Gradient fill applied to qualifying shapes.");

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
