using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
    {
        static void Main()
        {
            try
            {

                // Load an existing Visio diagram from file
                string inputPath = "input.vsdx";
                Diagram diagram = new Diagram(inputPath);

                // Access the first page in the diagram
                Page page = diagram.Pages[0];

                // Find the first shape on the page (skip deleted shapes)
                Shape targetShape = null;
                foreach (Aspose.Diagram.Shape shape in page.Shapes)
                {
                    if (shape.Del == BOOL.False)
                    {
                        targetShape = shape;
                        break;
                    }
                }

                if (targetShape == null)
                {
                    Console.WriteLine("No usable shape found on the first page.");
                    return;
                }

                // ----- Theme customization via ShapeSheet-like properties -----
                // Apply a preset theme to the shape
                targetShape.PresetTheme = PresetThemeValue.Bubble;

                // Choose a variant of the preset theme
                targetShape.PresetThemeVariant = PresetThemeVariantValue.Variant2;

                // Apply a quick style from the preset theme
                targetShape.PresetThemeQuickStyle = PresetQuickStyleValue.VariantStyle3;

                // Directly modify fill color (theme-related visual property)
                // Using a hex color string; this overrides the theme fill color for this shape
                targetShape.Fill.FillForegnd.Value = "#FF5733";

                // Directly modify line color (another theme-related visual property)
                targetShape.Line.LineColor.Value = "#2E86C1";

                // Save the modified diagram to a new file in VSDX format
                string outputPath = "output.vsdx";
                diagram.Save(outputPath, SaveFileFormat.Vsdx);

                Console.WriteLine("Diagram saved with customized theme settings to: " + outputPath);

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }