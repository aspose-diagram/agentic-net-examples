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
            Console.Error.WriteLine("Usage: Program <input.vsdx> <output.vsdx>");
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

            // Ensure the diagram has at least one page.
            if (diagram.Pages.Count == 0)
            {
                Console.Error.WriteLine("The diagram contains no pages.");
                return;
            }

            // Get the first page.
            Page page = diagram.Pages[0];

            // Ensure the page has at least one shape.
            if (page.Shapes.Count == 0)
            {
                Console.Error.WriteLine("The first page contains no shapes.");
                return;
            }

            // Retrieve the first shape on the page.
            Shape shape = page.Shapes[0];

            // ------------------------------------------------------------
            // Apply a preset theme to the shape (theme applied before gradient).
            // ------------------------------------------------------------
            shape.PresetTheme = PresetThemeValue.Bubble;
            shape.PresetThemeVariant = PresetThemeVariantValue.Variant1;
            // Optional: quick style can also be set if desired.
            // shape.PresetThemeQuickStyle = PresetQuickStyleValue.VariantStyle2;

            // ------------------------------------------------------------
            // Configure a two‑color gradient fill.
            // ------------------------------------------------------------
            // Set the fill pattern to gradient (value 25 as per API).
            shape.Fill.FillPattern.Value = 25;

            // Enable gradient fill.
            shape.Fill.GradientFill.GradientEnabled.Value = BOOL.True;

            // Set gradient direction (0 = left‑to‑right, other values per API).
            shape.Fill.GradientFill.GradientDir.Value = 0;

            // Clear any existing gradient stops.
            shape.Fill.GradientFill.GradientStops.Clear();

            // Add first gradient stop (position 0, red color).
            shape.Fill.GradientFill.GradientStops.Add(
                new DoubleValue(0, MeasureConst.NUM),
                new ColorValue("#FF0000", MeasureConst.Undefined));

            // Add second gradient stop (position 1, green color).
            shape.Fill.GradientFill.GradientStops.Add(
                new DoubleValue(1, MeasureConst.NUM),
                new ColorValue("#00FF00", MeasureConst.Undefined));

            // ------------------------------------------------------------
            // Save the modified diagram.
            // ------------------------------------------------------------
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Diagram saved successfully to: {outputPath}");
        }
        catch (Exception ex)
        {
            // Write any Aspose or runtime errors to the error stream.
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}