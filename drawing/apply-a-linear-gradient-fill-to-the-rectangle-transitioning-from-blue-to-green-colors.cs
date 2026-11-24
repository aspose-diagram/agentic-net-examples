using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Define input and output file paths
        string inputPath = "input.vsdx";
        if (!System.IO.File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }
        string outputPath = "output.vsdx";

        // Guard against missing input file
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the Visio diagram
            Diagram diagram = new Diagram(inputPath);

            // Get the first page (index 0)
            Page page = diagram.Pages[0];

            // Draw a rectangle shape on the page
            // Parameters: pinX, pinY, width, height (all in inches)
            double pinX = 2.0;
            double pinY = 2.0;
            double width = 4.0;
            double height = 2.0;
            long shapeId = page.DrawRectangle(pinX, pinY, width, height);

            // Retrieve the created shape (GetShape expects an int)
            Shape shape = page.Shapes.GetShape((int)shapeId);

            // -----------------------------------------------------------------
            // Apply a linear gradient fill: blue (#0000FF) to green (#00FF00)
            // -----------------------------------------------------------------

            // Set fill pattern to gradient (value 25 as per Aspose guidelines)
            shape.Fill.FillPattern.Value = 25;

            // Enable gradient fill
            shape.Fill.GradientFill.GradientEnabled.Value = BOOL.True;

            // Set gradient direction (0 = left-to-right, adjust as needed)
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

            // Save the modified diagram to the output file in VSDX format
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
        }
        catch (Exception ex)
        {
            // Write any Aspose or IO errors to the error console
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}