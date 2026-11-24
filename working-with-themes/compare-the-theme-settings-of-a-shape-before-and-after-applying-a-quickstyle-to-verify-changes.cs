using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Input diagram path (first argument or default)
        string inputPath = args.Length > 0 ? args[0] : "Themes1.vsdx";

        // Guard: ensure the input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
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

            if (diagram.Pages[0].Shapes.Count == 0)
            {
                Console.Error.WriteLine("The first page contains no shapes.");
                return;
            }

            // Retrieve the first shape on the first page
            Shape shape = diagram.Pages[0].Shapes[0];

            // Capture theme‑related visual properties before applying quickstyle
            string beforeFill = shape.Fill?.FillForegnd?.Value?.ToString() ?? "null";
            string beforeLine = shape.Line?.LineColor?.Value?.ToString() ?? "null";

            Console.WriteLine("Before applying quickstyle:");
            Console.WriteLine($"  FillForegnd: {beforeFill}");
            Console.WriteLine($"  LineColor:   {beforeLine}");

            // Apply a preset theme, variant, and quickstyle to the shape
            // Note: These properties are write‑only; we cannot read them back.
            shape.PresetTheme = PresetThemeValue.Bubble;
            shape.PresetThemeVariant = PresetThemeVariantValue.Variant3;
            shape.PresetThemeQuickStyle = PresetQuickStyleValue.VariantStyle2;

            // Capture the same visual properties after applying quickstyle
            string afterFill = shape.Fill?.FillForegnd?.Value?.ToString() ?? "null";
            string afterLine = shape.Line?.LineColor?.Value?.ToString() ?? "null";

            Console.WriteLine("After applying quickstyle:");
            Console.WriteLine($"  FillForegnd: {afterFill}");
            Console.WriteLine($"  LineColor:   {afterLine}");

            // Verify that the visual properties have changed
            bool fillChanged = !string.Equals(beforeFill, afterFill, StringComparison.Ordinal);
            bool lineChanged = !string.Equals(beforeLine, afterLine, StringComparison.Ordinal);

            if (fillChanged || lineChanged)
            {
                Console.WriteLine("Theme changes detected:");
                if (fillChanged) Console.WriteLine("  Fill color changed.");
                if (lineChanged) Console.WriteLine("  Line color changed.");
            }
            else
            {
                // If no change is detected, throw an exception to indicate failure
                throw new Exception("No theme-related visual changes were detected after applying the quickstyle.");
            }

            // Optional: save the modified diagram to verify the changes visually
            string outputPath = "Themes1_Modified.vsdx";
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Modified diagram saved to: {outputPath}");
        }
        catch (Exception ex)
        {
            // Write any errors to the error stream
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}