using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
    {
        static void Main()
        {
            try
            {

                // Input and output file paths
                string inputPath = "input.vsdx";
                string outputPath = "output.vsdx";

                // Load the diagram
                Diagram diagram = new Diagram(inputPath);

                // Iterate through all pages
                foreach (Page page in diagram.Pages)
                {
                    // Iterate through all shapes on the page
                    foreach (Shape shape in page.Shapes)
                    {
                        // Skip deleted shapes
                        if (shape.Del == BOOL.True)
                            continue;

                        // Preserve existing paragraph formatting by not modifying shape.Paras

                        // Apply gradient fill
                        // Set fill pattern to gradient (value 25)
                        shape.Fill.FillPattern.Value = 25;

                        // Enable gradient
                        shape.Fill.GradientFill.GradientEnabled.Value = BOOL.True;

                        // Set gradient direction (0 = left to right)
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

                // Save the modified diagram
                diagram.Save(outputPath, SaveFileFormat.Vsdx);

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }