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

            // Ensure the diagram has at least five pages.
            if (diagram.Pages.Count < 5)
            {
                Console.Error.WriteLine("The diagram does not contain a fifth page.");
                return;
            }

            // ------------------------------------------------------------
            // Create a new stylesheet with a gradient fill.
            // ------------------------------------------------------------
            StyleSheet gradientStyle = new StyleSheet();

            // Assign a unique ID (Visio IDs start at 1).
            gradientStyle.ID = diagram.StyleSheets.Count + 1;
            gradientStyle.Name = "GradientStyle";

            // Set the fill pattern to gradient (value 25).
            gradientStyle.Fill.FillPattern.Value = 25;

            // Enable the gradient fill.
            gradientStyle.Fill.GradientFill.GradientEnabled.Value = BOOL.True;

            // Set gradient direction (0 = left‑to‑right, other values are possible).
            gradientStyle.Fill.GradientFill.GradientDir.Value = 0;

            // Clear any existing gradient stops and add two stops (red to blue).
            gradientStyle.Fill.GradientFill.GradientStops.Clear();
            gradientStyle.Fill.GradientFill.GradientStops.Add(
                new DoubleValue(0, MeasureConst.NUM),
                new ColorValue("#FF0000", MeasureConst.Undefined));
            gradientStyle.Fill.GradientFill.GradientStops.Add(
                new DoubleValue(1, MeasureConst.NUM),
                new ColorValue("#0000FF", MeasureConst.Undefined));

            // Add the stylesheet to the diagram's collection.
            diagram.StyleSheets.Add(gradientStyle);

            // ------------------------------------------------------------
            // Apply the stylesheet to page five (index 4).
            // ------------------------------------------------------------
            Page pageFive = diagram.Pages[4];

            // Apply the same style ID for text, line, and fill.
            pageFive.ApplyStyle(gradientStyle.ID, gradientStyle.ID, gradientStyle.ID);

            // ------------------------------------------------------------
            // Save the modified diagram.
            // ------------------------------------------------------------
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Gradient stylesheet applied to page five and saved to: {outputPath}");
        }
        catch (Exception ex)
        {
            // Write any Aspose or IO errors to the error stream.
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}