using System.IO;
using System;
using Aspose.Diagram;

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

            // Retrieve a shape (for example, the first shape on the page)
            // Ensure the shape collection is not empty
            if (page.Shapes.Count == 0)
            {
                Console.WriteLine("No shapes found on the page.");
                return;
            }

            // Get the shape by its ID (Shapes collection uses long IDs)
            long shapeId = page.Shapes[0].ID;
            Shape shape = page.Shapes.GetShape(shapeId);

            // Enable gradient fill
            shape.Fill.FillPattern.Value = 25;                     // Gradient fill pattern
            shape.Fill.GradientFill.GradientEnabled.Value = BOOL.True;

            // Set gradient angle to 0 degrees for a horizontal fill
            shape.Fill.GradientFill.GradientAngle.Value = 0;

            // (Optional) Define gradient stops if needed
            // Clear existing stops
            shape.Fill.GradientFill.GradientStops.Clear();
            // Add a start (left) stop – blue
            shape.Fill.GradientFill.GradientStops.Add(
                new DoubleValue(0, MeasureConst.NUM),
                new ColorValue("#0000FF", MeasureConst.Undefined));
            // Add an end (right) stop – green
            shape.Fill.GradientFill.GradientStops.Add(
                new DoubleValue(1, MeasureConst.NUM),
                new ColorValue("#00FF00", MeasureConst.Undefined));

            // Save the modified diagram
            string outputPath = "output.vsdx";
            diagram.Save(outputPath, SaveFileFormat.Vsdx);

            Console.WriteLine($"Diagram saved to '{outputPath}' with gradient angle set to 0°.");

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
