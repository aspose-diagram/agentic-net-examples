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

        // Guard against missing input file.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the Visio diagram.
            Diagram diagram = new Diagram(inputPath);

            // Retrieve the first page (or any specific page as needed).
            // Here we use the first page in the collection.
            if (diagram.Pages.Count == 0)
            {
                Console.Error.WriteLine("The diagram contains no pages.");
                return;
            }

            Page page = diagram.Pages[0];

            // Retrieve a shape by its ID. Adjust the ID as required.
            // For demonstration we use shape ID 1.
            Shape shape = page.Shapes.GetShape(1);
            if (shape == null)
            {
                Console.Error.WriteLine("Shape with ID 1 not found on the page.");
                return;
            }

            // ------------------------------------------------------------
            // Begin gradient fill modification.
            // ------------------------------------------------------------

            // Ensure the shape uses a gradient fill pattern.
            shape.Fill.FillPattern.Value = 25; // 25 = gradient fill.

            // Enable the gradient.
            shape.Fill.GradientFill.GradientEnabled.Value = BOOL.True;

            // Set gradient direction (0 = left‑to‑right, adjust as needed).
            shape.Fill.GradientFill.GradientDir.Value = 0;

            // Clear any existing gradient stops.
            shape.Fill.GradientFill.GradientStops.Clear();

            // Add a new gradient stop at position 0 (start) with red color.
            shape.Fill.GradientFill.GradientStops.Add(
                new DoubleValue(0, MeasureConst.NUM),
                new ColorValue("#FF0000", MeasureConst.Undefined));

            // Add a second gradient stop at position 1 (end) with blue color.
            shape.Fill.GradientFill.GradientStops.Add(
                new DoubleValue(1, MeasureConst.NUM),
                new ColorValue("#0000FF", MeasureConst.Undefined));

            // ------------------------------------------------------------
            // Gradient modifications are now applied directly to the shape.
            // ------------------------------------------------------------

            // Save the modified diagram to the specified output path.
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Diagram saved successfully to: {outputPath}");
        }
        catch (Exception ex)
        {
            // Write any Aspose or I/O errors to the error stream.
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}