using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        // Define input file path
        string inputPath = "input.vsdx";
        // Guard to ensure the input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Define output file path
        string outputPath = "output.vsdx";

        try
        {
            // Load the existing Visio diagram
            Diagram diagram = new Diagram(inputPath);

            // Access the first page of the diagram
            Page page = diagram.Pages[0];

            // Ensure there is at least one shape on the page
            if (page.Shapes.Count == 0)
            {
                Console.WriteLine("No shapes found on the page.");
                return;
            }

            // Retrieve the first shape (or replace with a specific shape ID)
            Shape shape = page.Shapes.GetShape(page.Shapes[0].ID);

            // Enable gradient fill on the shape (required before manipulating gradient stops)
            shape.Fill.FillPattern.Value = 25;               // Gradient fill pattern
            shape.Fill.GradientFill.GradientEnabled.Value = BOOL.True;

            // Access the gradient stops collection
            var gradientStops = shape.Fill.GradientFill.GradientStops;

            // Verify that at least one gradient stop exists
            if (gradientStops.Count == 0)
            {
                Console.WriteLine("The shape does not contain any gradient stops.");
                return;
            }

            // Change the position of the gradient stop at index 0 to 0.0 (start of the gradient)
            GradientStop firstStop = gradientStops[0];
            // Assign a new DoubleValue (position 0, using MeasureConst.NUM unit)
            firstStop.Position = new DoubleValue(0, MeasureConst.NUM);

            // Save the modified diagram to the output path
            diagram.Save(outputPath, SaveFileFormat.Vsdx);

            Console.WriteLine("Gradient stop position updated and diagram saved successfully.");
        }
        catch (Exception ex)
        {
            // Write any errors that occur during processing to the error stream
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}