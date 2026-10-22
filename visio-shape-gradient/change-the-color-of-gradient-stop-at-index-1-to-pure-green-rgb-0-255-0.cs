using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Expect input and output file paths as command‑line arguments.
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <inputVisioFile> <outputVisioFile>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

        // Guard against missing input file.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file does not exist: {inputPath}");
            return;
        }

        try
        {
            // Load the Visio diagram.
            Diagram diagram = new Diagram(inputPath);

            // Get the first page (index 0). Adjust as needed for specific pages.
            if (diagram.Pages.Count == 0)
            {
                Console.Error.WriteLine("The diagram contains no pages.");
                return;
            }
            Page page = diagram.Pages[0];

            // Get shape with ID 1 on the page. Adjust the ID if a different shape is required.
            Shape shape = page.Shapes.GetShape(1);
            if (shape == null)
            {
                Console.Error.WriteLine("Shape with ID 1 was not found on the first page.");
                return;
            }

            // Ensure gradient fill is enabled.
            shape.Fill.GradientFill.GradientEnabled.Value = BOOL.True;

            // Retrieve existing gradient stops.
            var gradientStops = shape.Fill.GradientFill.GradientStops;
            if (gradientStops.Count == 0)
            {
                Console.Error.WriteLine("The shape does not have any gradient stops.");
                return;
            }

            // Store positions and colors of existing stops.
            var positions = new double[gradientStops.Count];
            var colors = new string[gradientStops.Count];

            for (int i = 0; i < gradientStops.Count; i++)
            {
                GradientStop stop = gradientStops[i];
                // Position is a DoubleValue; its numeric value is stored in .Value.
                positions[i] = stop.Position.Value;
                // Color is a ColorValue; its hex string is stored in .Value.
                colors[i] = stop.Color.Value;
            }

            // Modify the color of the stop at index 1 (second stop) to pure green.
            if (gradientStops.Count > 1)
            {
                colors[1] = "#00FF00"; // RGB(0,255,0)
            }
            else
            {
                Console.Error.WriteLine("The shape has fewer than two gradient stops; cannot modify index 1.");
                return;
            }

            // Clear existing stops.
            gradientStops.Clear();

            // Re‑add stops with the updated color for index 1.
            for (int i = 0; i < positions.Length; i++)
            {
                // Use MeasureConst.NUM for position units and MeasureConst.Undefined for color units.
                gradientStops.Add(
                    new DoubleValue(positions[i], MeasureConst.NUM),
                    new ColorValue(colors[i], MeasureConst.Undefined));
            }

            // Save the modified diagram to the specified output path in VSDX format.
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Diagram saved successfully to: {outputPath}");
        }
        catch (Exception ex)
        {
            // Write any Aspose or I/O errors to the error stream.
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}