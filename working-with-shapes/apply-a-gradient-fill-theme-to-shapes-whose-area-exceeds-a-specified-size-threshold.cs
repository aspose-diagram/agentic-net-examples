using System.IO;
using System;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        try
        {

            // Input Visio file path
            string inputPath = "input.vsdx";
            // Output Visio file path
            string outputPath = "output.vsdx";

            // Area threshold in square inches (example: 4 sq in)
            double areaThreshold = 4.0;

            // Load the diagram
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Iterate through all pages
                foreach (Page page in diagram.Pages)
                {
                    // Iterate through all shapes on the page
                    foreach (Shape shape in page.Shapes)
                    {
                        // Skip shapes that are marked as deleted
                        if (shape.Del == BOOL.True)
                            continue;

                        // Retrieve shape dimensions
                        double width = shape.XForm.Width.Value;
                        double height = shape.XForm.Height.Value;

                        // Calculate shape area
                        double area = width * height;

                        // Apply gradient fill if area exceeds the threshold
                        if (area > areaThreshold)
                        {
                            // Set fill pattern to gradient (value 25)
                            shape.Fill.FillPattern.Value = 25;

                            // Enable gradient fill
                            shape.Fill.GradientFill.GradientEnabled.Value = BOOL.True;

                            // Set gradient direction (0 = left to right)
                            shape.Fill.GradientFill.GradientDir.Value = 0;

                            // Clear any existing gradient stops
                            shape.Fill.GradientFill.GradientStops.Clear();

                            // Add gradient stop at start (position 0) with blue color
                            shape.Fill.GradientFill.GradientStops.Add(
                                new DoubleValue(0, MeasureConst.NUM),
                                new ColorValue("#0000FF", MeasureConst.Undefined));

                            // Add gradient stop at end (position 1) with green color
                            shape.Fill.GradientFill.GradientStops.Add(
                                new DoubleValue(1, MeasureConst.NUM),
                                new ColorValue("#00FF00", MeasureConst.Undefined));
                        }
                    }
                }

                // Save the modified diagram
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
            }

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
