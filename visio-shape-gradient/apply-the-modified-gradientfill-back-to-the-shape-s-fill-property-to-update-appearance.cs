using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Validate command‑line arguments.
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <inputVisioPath> <outputVisioPath>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

        // Guard: ensure the input file exists.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the Visio diagram.
            Diagram diagram = new Diagram(inputPath);

            // Retrieve the first page (index 0). Adjust as needed.
            if (diagram.Pages.Count == 0)
            {
                Console.Error.WriteLine("The diagram contains no pages.");
                return;
            }
            Page page = diagram.Pages[0];

            // Retrieve a shape by its ID (example uses ID = 1).
            // Ensure the shape exists before proceeding.
            Shape shape = page.Shapes.GetShape(1);
            if (shape == null)
            {
                Console.Error.WriteLine("Shape with ID 1 not found on the first page.");
                return;
            }

            // -----------------------------------------------------------------
            // Begin gradient fill modification.
            // -----------------------------------------------------------------

            // Set the fill pattern to gradient (value 25 corresponds to gradient fill).
            shape.Fill.FillPattern.Value = 25;

            // Enable the gradient.
            shape.Fill.GradientFill.GradientEnabled.Value = BOOL.True;

            // Set gradient direction (0 = horizontal, 1 = vertical, etc.).
            shape.Fill.GradientFill.GradientDir.Value = 0;

            // Set gradient angle (in degrees).
            shape.Fill.GradientFill.GradientAngle.Value = 45;

            // Clear any existing gradient stops.
            shape.Fill.GradientFill.GradientStops.Clear();

            // Add a start stop at position 0 with red color.
            shape.Fill.GradientFill.GradientStops.Add(
                new DoubleValue(0, MeasureConst.NUM),
                new ColorValue("#FF0000", MeasureConst.Undefined));

            // Add an end stop at position 1 with blue color.
            shape.Fill.GradientFill.GradientStops.Add(
                new DoubleValue(1, MeasureConst.NUM),
                new ColorValue("#0000FF", MeasureConst.Undefined));

            // -----------------------------------------------------------------
            // Gradient modifications are applied directly to the shape's Fill.
            // No additional assignment back to the Fill property is required.
            // -----------------------------------------------------------------

            // Save the modified diagram to the specified output path in VSDX format.
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Diagram saved successfully to: {outputPath}");
        }
        catch (Exception ex)
        {
            // Write any Aspose or I/O errors to the error console.
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}