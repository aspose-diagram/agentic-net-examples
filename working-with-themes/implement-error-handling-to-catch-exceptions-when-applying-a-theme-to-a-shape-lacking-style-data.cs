using System.IO;
using System;
using Aspose.Diagram;

class ApplyThemeWithErrorHandling
{
    static void Main()
    {
        try
        {

            // Load an existing Visio diagram
            Diagram diagram = new Diagram("input.vsdx");

            // Assume we work with the first page and first shape
            Page page = diagram.Pages[0];
            Shape shape = page.Shapes[0];

            try
            {
                // Attempt to apply a preset theme style matrix to the shape
                // This may throw if the shape lacks style data
                shape.SetPresetThemeStyleMatrics(
                    PresetStyleMatricsValue.Style1,   // style row
                    PresetColorMatricsValue.Color1   // color column
                );

                // Optionally, set quick style, theme, or variant directly
                // shape.PresetThemeQuickStyle = PresetQuickStyleValue.QuickStyle1;
                // shape.PresetTheme = PresetThemeValue.Theme1;
                // shape.PresetThemeVariant = PresetThemeVariantValue.Variant1;
            }
            catch (Exception ex)
            {
                // Handle the exception gracefully
                Console.WriteLine($"Error applying theme to shape ID {shape.ID}: {ex.Message}");
            }

            // Save the modified diagram
            diagram.Save("output.vsdx", SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
