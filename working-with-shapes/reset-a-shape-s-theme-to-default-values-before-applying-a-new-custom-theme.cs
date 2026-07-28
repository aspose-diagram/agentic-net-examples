using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
    {
        static void Main()
        {
            try
            {

                // Path to the source Visio file
                string inputPath = "input.vsdx";
                // Path for the output Visio file
                string outputPath = "output.vsdx";

                // Load the diagram
                Diagram diagram = new Diagram(inputPath);

                // Choose the page (first page in this example)
                Page page = diagram.Pages[0];

                // Identify the shape to reset (using shape ID 1 as an example)
                long targetShapeId = 1;
                Shape shape = page.Shapes.GetShape(targetShapeId);
                if (shape == null)
                    throw new Exception($"Shape with ID {targetShapeId} not found.");

                // ---------- Reset theme to default values ----------
                // Apply a known default preset theme (Bubble) and its default variant/quickstyle.
                // This effectively clears any previously applied custom theme settings.
                shape.PresetTheme = PresetThemeValue.Bubble;
                shape.PresetThemeVariant = PresetThemeVariantValue.Variant1;
                shape.PresetThemeQuickStyle = PresetQuickStyleValue.VariantStyle1;

                // ---------- Apply a new custom theme ----------
                // Example: use style matrix 3 and color matrix 4.
                shape.SetPresetThemeStyleMatrics(PresetStyleMatricsValue.Style3, PresetColorMatricsValue.Color4);

                // Save the modified diagram
                diagram.Save(outputPath, SaveFileFormat.Vsdx);

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }