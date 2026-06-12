using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
    {
        static void Main(string[] args)
        {
            try
            {

                // Load an existing Visio diagram
                string inputPath = "input.vsdx";
                Diagram diagram = new Diagram(inputPath);

                // Access the first page and the first shape on that page
                Page page = diagram.Pages[0];
                Shape shape = page.Shapes[0];

                // Capture theme‑related inherited formatting before applying a quickstyle
                string beforeFillColor = shape.InheritFill.FillForegnd.Value;
                string beforeLineColor = shape.InheritLine.LineColor.Value;

                Console.WriteLine("Before applying quickstyle:");
                Console.WriteLine($"  Inherited Fill Foreground Color: {beforeFillColor}");
                Console.WriteLine($"  Inherited Line Color: {beforeLineColor}");

                // Apply a preset theme, variant, and quickstyle to the shape
                shape.PresetTheme = PresetThemeValue.Bubble;
                shape.PresetThemeVariant = PresetThemeVariantValue.Variant1;
                shape.PresetThemeQuickStyle = PresetQuickStyleValue.VariantStyle2;

                // Capture the inherited formatting after the quickstyle is applied
                string afterFillColor = shape.InheritFill.FillForegnd.Value;
                string afterLineColor = shape.InheritLine.LineColor.Value;

                Console.WriteLine("\nAfter applying quickstyle:");
                Console.WriteLine($"  Inherited Fill Foreground Color: {afterFillColor}");
                Console.WriteLine($"  Inherited Line Color: {afterLineColor}");

                // Verify that the theme changes have affected the shape's appearance
                bool fillChanged = !string.Equals(beforeFillColor, afterFillColor, StringComparison.OrdinalIgnoreCase);
                bool lineChanged = !string.Equals(beforeLineColor, afterLineColor, StringComparison.OrdinalIgnoreCase);

                if (fillChanged || lineChanged)
                {
                    Console.WriteLine("\nTheme quickstyle applied successfully. Changes detected:");
                    if (fillChanged)
                        Console.WriteLine($"  Fill color changed from {beforeFillColor} to {afterFillColor}");
                    if (lineChanged)
                        Console.WriteLine($"  Line color changed from {beforeLineColor} to {afterLineColor}");
                }
                else
                {
                    throw new Exception("Theme quickstyle did not produce any visible changes on the shape.");
                }

                // Optionally save the modified diagram to verify the result visually
                string outputPath = "output.vsdx";
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
                Console.WriteLine($"\nModified diagram saved to: {outputPath}");

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }