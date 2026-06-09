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

            // Load an existing Visio diagram
            string inputPath = "input.vsdx";
            Diagram diagram = new Diagram(inputPath);

            // Apply gradient fill to every non‑deleted shape on each page
            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    // Skip shapes that are marked as deleted
                    if (shape.Del == BOOL.True)
                        continue;

                    // Set the fill pattern to gradient (value 25)
                    shape.Fill.FillPattern.Value = 25;

                    // Enable gradient fill
                    shape.Fill.GradientFill.GradientEnabled.Value = BOOL.True;

                    // Set gradient direction (0 = left‑to‑right)
                    shape.Fill.GradientFill.GradientDir.Value = 0;

                    // Clear any existing gradient stops
                    shape.Fill.GradientFill.GradientStops.Clear();

                    // Add gradient stops: blue at start, green at end
                    shape.Fill.GradientFill.GradientStops.Add(
                        new DoubleValue(0, MeasureConst.NUM),
                        new ColorValue("#0000FF", MeasureConst.Undefined));

                    shape.Fill.GradientFill.GradientStops.Add(
                        new DoubleValue(1, MeasureConst.NUM),
                        new ColorValue("#00FF00", MeasureConst.Undefined));
                }
            }

            // Save the modified diagram
            string outputPath = "output.vsdx";
            diagram.Save(outputPath, SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
