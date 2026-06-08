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

            // Load an existing Visio diagram (replace with your actual file path)
            Diagram diagram = new Diagram("input.vsdx");

            // Ensure there is at least one page and one shape
            if (diagram.Pages.Count == 0)
            {
                throw new Exception("The diagram contains no pages.");
            }

            Page page = diagram.Pages[0];

            if (page.Shapes.Count == 0)
            {
                throw new Exception("The first page contains no shapes.");
            }

            // Retrieve the first shape on the page (you can change the ID as needed)
            Shape shape = page.Shapes.GetShape(1);

            // Enable gradient fill
            shape.Fill.FillPattern.Value = 25; // Gradient fill pattern
            shape.Fill.GradientFill.GradientEnabled.Value = BOOL.True;
            shape.Fill.GradientFill.GradientDir.Value = (int)GradientFillDir.Linear;

            // Add a new gradient stop at position 0.25 with yellow color (RGB 255,255,0)
            // Position is expressed as a fraction (0 to 1) using MeasureConst.NUM
            // Color is specified as a hex string using ColorValue
            shape.Fill.GradientFill.GradientStops.Add(
                new DoubleValue(0.25, MeasureConst.NUM),
                new ColorValue("#FFFF00", MeasureConst.Undefined));

            // Save the modified diagram
            diagram.Save("output.vsdx", SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
