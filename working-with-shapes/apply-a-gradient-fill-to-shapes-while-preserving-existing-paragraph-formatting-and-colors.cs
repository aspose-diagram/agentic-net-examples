using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
    {
        static void Main(string[] args)
        {
            try
            {

                // Input and output file paths (adjust as needed)
                string inputPath = "input.vsdx";
                string outputPath = "output.vsdx";

                // Load the existing Visio diagram
                Diagram diagram = new Diagram(inputPath);

                // Iterate through all pages and shapes
                foreach (Page page in diagram.Pages)
                {
                    foreach (Shape shape in page.Shapes)
                    {
                        // Skip deleted shapes
                        if (shape.Del == BOOL.True)
                            continue;

                        // Preserve existing paragraph formatting and text colors by NOT modifying shape.Paras or shape.Chars

                        // Apply gradient fill
                        shape.Fill.FillPattern.Value = 25;                         // Set fill pattern to gradient
                        shape.Fill.GradientFill.GradientEnabled.Value = BOOL.True; // Enable gradient
                        shape.Fill.GradientFill.GradientDir.Value = 0;            // Direction (0 = left to right)

                        // Clear any existing gradient stops
                        shape.Fill.GradientFill.GradientStops.Clear();

                        // Add gradient stops (blue to green)
                        shape.Fill.GradientFill.GradientStops.Add(
                            new DoubleValue(0, MeasureConst.NUM),
                            new ColorValue("#0000FF", MeasureConst.Undefined));

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