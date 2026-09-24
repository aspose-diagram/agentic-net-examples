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

            // Iterate through all pages
            foreach (Page page in diagram.Pages)
            {
                // Iterate through all shapes on the page
                foreach (Shape shape in page.Shapes)
                {
                    // Skip deleted shapes
                    if (shape.Del == BOOL.True)
                        continue;

                    // Read inherited fill properties
                    string inheritedForeColor = shape.InheritFill.FillForegnd.Value;
                    string inheritedBackColor = shape.InheritFill.FillBkgnd.Value;
                    int inheritedPattern = shape.InheritFill.FillPattern.Value;

                    // Example: output inherited values to console
                    Console.WriteLine($"Shape ID {shape.ID} - Inherited ForeColor: {inheritedForeColor}, BackColor: {inheritedBackColor}, Pattern: {inheritedPattern}");

                    // Customize the shape's fill based on inherited values
                    // Here we simply set a solid fill with a new foreground color
                    shape.Fill.FillPattern.Value = 1; // Solid fill
                    shape.Fill.FillForegnd.Value = "#00FF00"; // New foreground color (green)
                    shape.Fill.FillBkgnd.Value = "#FFFFFF"; // Optional background color (white)

                    // If the shape originally used a gradient, you could enable gradient fill instead
                    // shape.Fill.FillPattern.Value = 25; // Gradient pattern
                    // shape.Fill.GradientFill.GradientEnabled.Value = BOOL.True;
                    // shape.Fill.GradientFill.GradientDir.Value = 0;
                    // shape.Fill.GradientFill.GradientStops.Clear();
                    // shape.Fill.GradientFill.GradientStops.Add(new DoubleValue(0, MeasureConst.NUM), new ColorValue("#00FF00", MeasureConst.Undefined));
                    // shape.Fill.GradientFill.GradientStops.Add(new DoubleValue(1, MeasureConst.NUM), new ColorValue("#FFFFFF", MeasureConst.Undefined));
                }
            }

            // Save the modified diagram
            string outputPath = "output.vsdx";
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine("Diagram saved to " + outputPath);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
