using System.IO;
using System;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        try
        {

            // Path to the source Visio file
            string inputPath = "input.vsdx";

            // Load the diagram
            Diagram diagram = new Diagram(inputPath);

            // Get the first page (assumes at least one page exists)
            Page page = diagram.Pages[0];

            // Find the first non‑deleted shape on the page
            Shape targetShape = null;
            foreach (Shape shp in page.Shapes)
            {
                if (shp.Del == BOOL.False)
                {
                    targetShape = shp;
                    break;
                }
            }

            if (targetShape == null)
            {
                throw new Exception("No usable shape found on the page.");
            }

            // -------------------------------------------------
            // Reset the shape's theme to default (no theme)
            // -------------------------------------------------
            // Since the theme properties are write‑only, we assign a neutral theme.
            // Using the Bubble theme with the first variant and quick style effectively clears any previous custom theme.
            targetShape.PresetTheme = PresetThemeValue.Bubble;
            targetShape.PresetThemeVariant = PresetThemeVariantValue.Variant1;
            targetShape.PresetThemeQuickStyle = PresetQuickStyleValue.VariantStyle1;

            // -------------------------------------------------
            // Apply a new custom theme to the shape
            // -------------------------------------------------
            // Example: apply style matrix 2 with color matrix 5.
            targetShape.SetPresetThemeStyleMatrics(PresetStyleMatricsValue.Style2, PresetColorMatricsValue.Color5);

            // Save the modified diagram
            string outputPath = "output.vsdx";
            diagram.Save(outputPath, SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
