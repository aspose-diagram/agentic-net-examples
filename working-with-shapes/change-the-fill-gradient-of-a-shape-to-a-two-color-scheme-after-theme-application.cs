using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
    {
        static void Main()
        {
            try
            {

                // Load an existing Visio diagram
                string inputPath = "input.vsdx";
                Diagram diagram = new Diagram(inputPath);

                // Apply a preset theme to the first page (optional, demonstrates "after theme application")
                Page page = diagram.Pages[0];
                page.PresetTheme = PresetThemeValue.Bubble;
                page.PresetThemeVariant = PresetThemeVariantValue.Variant1;

                // Iterate through all shapes on the page and set a two‑color gradient fill
                foreach (Shape shape in page.Shapes)
                {
                    // Skip deleted shapes
                    if (shape.Del == BOOL.True)
                        continue;

                    // Enable gradient fill
                    shape.Fill.FillPattern.Value = 25; // Gradient fill pattern
                    shape.Fill.GradientFill.GradientEnabled.Value = BOOL.True;
                    shape.Fill.GradientFill.GradientDir.Value = 0; // Direction (0 = left‑to‑right)

                    // Clear any existing gradient stops
                    shape.Fill.GradientFill.GradientStops.Clear();

                    // Add first gradient stop (position 0, blue)
                    shape.Fill.GradientFill.GradientStops.Add(
                        new DoubleValue(0, MeasureConst.NUM),
                        new ColorValue("#0000FF", MeasureConst.Undefined));

                    // Add second gradient stop (position 1, green)
                    shape.Fill.GradientFill.GradientStops.Add(
                        new DoubleValue(1, MeasureConst.NUM),
                        new ColorValue("#00FF00", MeasureConst.Undefined));
                }

                // Save the modified diagram
                string outputPath = "output.vsdx";
                diagram.Save(outputPath, SaveFileFormat.Vsdx);

                Console.WriteLine("Gradient fill applied and diagram saved to " + outputPath);

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }