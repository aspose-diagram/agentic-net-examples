using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Validate command‑line arguments.
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: <program> <inputVisioPath> <outputVisioPath>");
            return;
        }

        // Input Visio file path.
        string inputPath = args[0];
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Output Visio file path.
        string outputPath = args[1];
        // No existence check for output – it will be created/overwritten.

        // Define the target theme matrix (preset theme + style/color matrices + colors).
        PresetThemeValue targetTheme = PresetThemeValue.Bubble;                     // Theme enum.
        PresetThemeVariantValue targetVariant = PresetThemeVariantValue.Variant1;   // Variant enum.
        PresetQuickStyleValue targetQuickStyle = PresetQuickStyleValue.VariantStyle1; // Quick‑style enum.
        PresetStyleMatricsValue targetStyleMatrics = PresetStyleMatricsValue.Style1; // Style matrix.
        PresetColorMatricsValue targetColorMatrics = PresetColorMatricsValue.Color1; // Color matrix.
        string targetFillColor = "#FF0000"; // Desired fill foreground color (red).
        string targetLineColor = "#0000FF"; // Desired line color (blue).

        try
        {
            // Load the diagram from the input file.
            Diagram diagram = new Diagram(inputPath);

            // Iterate through all pages.
            foreach (Page page in diagram.Pages)
            {
                // Iterate through all shapes on the current page.
                foreach (Shape shape in page.Shapes)
                {
                    // Skip shapes that are marked as deleted.
                    if (shape.Del == BOOL.True)
                        continue;

                    // Determine if the shape's current visual attributes differ from the target matrix.
                    bool fillDiffers = shape.Fill.FillForegnd.Value != targetFillColor;
                    bool lineDiffers = shape.Line.LineColor.Value != targetLineColor;
                    bool needsTheme = fillDiffers || lineDiffers;

                    if (needsTheme)
                    {
                        // Apply the preset theme and its variant/quick‑style.
                        shape.PresetTheme = targetTheme;
                        shape.PresetThemeVariant = targetVariant;
                        shape.PresetThemeQuickStyle = targetQuickStyle;

                        // Apply the style and color matrices.
                        shape.SetPresetThemeStyleMatrics(targetStyleMatrics, targetColorMatrics);

                        // Ensure the fill and line colors match the target matrix.
                        shape.Fill.FillForegnd.Value = targetFillColor;
                        shape.Line.LineColor.Value = targetLineColor;
                    }
                }
            }

            // Save the modified diagram to the output path using VSDX format.
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
        }
        catch (Exception ex)
        {
            // Log any Aspose or I/O errors.
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}