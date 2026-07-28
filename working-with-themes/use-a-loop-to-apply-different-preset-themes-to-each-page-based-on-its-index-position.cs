using System.IO;
using System;
using Aspose.Diagram;

class ApplyPresetThemes
{
    static void Main()
    {
        try
        {

            // Load an existing Visio diagram
            Diagram diagram = new Diagram("input.vsdx");

            // Define a list of preset themes to apply cyclically
            PresetThemeValue[] themes = new PresetThemeValue[]
            {
                PresetThemeValue.Office,
                PresetThemeValue.Linear,
                PresetThemeValue.Zephyr,
                PresetThemeValue.Integral,
                PresetThemeValue.Simple,
                PresetThemeValue.Whisp,
                PresetThemeValue.Daybreak,
                PresetThemeValue.Parallel,
                PresetThemeValue.Sequence,
                PresetThemeValue.Slice
            };

            // Loop through each page and assign a theme based on its index
            for (int i = 0; i < diagram.Pages.Count; i++)
            {
                Page page = diagram.Pages[i];

                // Select a theme from the array using modulo to wrap around
                PresetThemeValue selectedTheme = themes[i % themes.Length];

                // Apply the selected preset theme to the page
                page.PresetTheme = selectedTheme;

                // Optionally, also set a theme variant and quick style for richer styling
                page.PresetThemeVariant = (PresetThemeVariantValue)(i % 4); // Variant1‑4
                page.PresetThemeQuickStyle = (PresetQuickStyleValue)(100 + (i % 4)); // VariantStyle1‑4
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
