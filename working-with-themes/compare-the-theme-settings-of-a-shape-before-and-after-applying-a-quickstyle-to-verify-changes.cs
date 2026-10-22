using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Validate input arguments
        if (args.Length < 1)
        {
            Console.Error.WriteLine("Usage: Program <inputDiagramPath> [outputDiagramPath]");
            return;
        }

        string inputPath = args[0];
        string outputPath = args.Length > 1 ? args[1] : "output_modified.vsdx";

        // Guard against missing input file
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file does not exist: {inputPath}");
            return;
        }

        try
        {
            // Load the diagram
            Diagram diagram = new Diagram(inputPath);

            // Ensure there is at least one page and one shape
            if (diagram.Pages.Count == 0)
            {
                Console.Error.WriteLine("The diagram contains no pages.");
                return;
            }

            Page page = diagram.Pages[0];
            if (page.Shapes.Count == 0)
            {
                Console.Error.WriteLine("The first page contains no shapes.");
                return;
            }

            // Retrieve the first shape
            Shape shape = page.Shapes[0];

            // Capture theme‑related visual properties before applying quickstyle
            string fillForegndBefore = shape.Fill?.FillForegnd?.Value ?? string.Empty;
            double fillForegndTransBefore = shape.Fill?.FillForegndTrans?.Value ?? double.NaN;
            string lineColorBefore = shape.Line?.LineColor?.Value ?? string.Empty;
            double lineWeightBefore = shape.Line?.LineWeight?.Value ?? double.NaN;

            // Apply a preset theme, variant, and quickstyle (write‑only properties)
            shape.PresetTheme = PresetThemeValue.Bubble;
            shape.PresetThemeVariant = PresetThemeVariantValue.Variant3;
            shape.PresetThemeQuickStyle = PresetQuickStyleValue.VariantStyle2;

            // Capture the same visual properties after applying quickstyle
            string fillForegndAfter = shape.Fill?.FillForegnd?.Value ?? string.Empty;
            double fillForegndTransAfter = shape.Fill?.FillForegndTrans?.Value ?? double.NaN;
            string lineColorAfter = shape.Line?.LineColor?.Value ?? string.Empty;
            double lineWeightAfter = shape.Line?.LineWeight?.Value ?? double.NaN;

            // Compare and report differences
            bool anyChange = false;

            if (fillForegndBefore != fillForegndAfter)
            {
                anyChange = true;
                Console.WriteLine($"FillForegnd changed from '{fillForegndBefore}' to '{fillForegndAfter}'.");
            }

            if (!double.IsNaN(fillForegndTransBefore) && !double.IsNaN(fillForegndTransAfter) &&
                fillForegndTransBefore != fillForegndTransAfter)
            {
                anyChange = true;
                Console.WriteLine($"FillForegndTrans changed from {fillForegndTransBefore} to {fillForegndTransAfter}.");
            }

            if (lineColorBefore != lineColorAfter)
            {
                anyChange = true;
                Console.WriteLine($"LineColor changed from '{lineColorBefore}' to '{lineColorAfter}'.");
            }

            if (!double.IsNaN(lineWeightBefore) && !double.IsNaN(lineWeightAfter) &&
                lineWeightBefore != lineWeightAfter)
            {
                anyChange = true;
                Console.WriteLine($"LineWeight changed from {lineWeightBefore} to {lineWeightAfter}.");
            }

            if (!anyChange)
            {
                Console.WriteLine("No visual theme changes detected after applying the quickstyle.");
            }

            // Save the modified diagram
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Modified diagram saved to: {outputPath}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}