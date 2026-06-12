using System.IO;
using System;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        try
        {

            // Input and output file paths (adjust as needed)
            string inputPath = "input.vsdx";
            string outputPath = "output.vsdx";

            // Area threshold in square inches
            double areaThreshold = 2.0;

            // Load the Visio diagram
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Iterate through all pages
                foreach (Page page in diagram.Pages)
                {
                    // Iterate through all shapes on the page
                    foreach (Shape shape in page.Shapes)
                    {
                        // Skip deleted shapes
                        if (shape.Del == BOOL.True)
                            continue;

                        // Calculate shape area (width * height)
                        double width = shape.XForm.Width.Value;
                        double height = shape.XForm.Height.Value;
                        double area = width * height;

                        // Apply gradient fill if area exceeds the threshold
                        if (area > areaThreshold)
                        {
                            // Set fill pattern to gradient (value 25)
                            shape.Fill.FillPattern.Value = 25;

                            // Enable gradient fill
                            shape.Fill.GradientFill.GradientEnabled.Value = BOOL.True;

                            // Set gradient direction (0 = horizontal, adjust as needed)
                            shape.Fill.GradientFill.GradientDir.Value = 0;

                            // Clear any existing gradient stops
                            shape.Fill.GradientFill.GradientStops.Clear();

                            // Add gradient stop at position 0 (start) with blue color
                            shape.Fill.GradientFill.GradientStops.Add(
                                new DoubleValue(0, MeasureConst.NUM),
                                new ColorValue("#0000FF", MeasureConst.Undefined));

                            // Add gradient stop at position 1 (end) with green color
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
