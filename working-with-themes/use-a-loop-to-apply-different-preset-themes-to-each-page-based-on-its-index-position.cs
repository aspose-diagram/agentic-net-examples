using System.IO;
using Aspose.Diagram;
using System;

class Program
{
    static void Main()
    {
        try
        {

            // Load an existing Visio diagram
            Diagram diagram = new Diagram("input.vsdx");

            // Preset themes to apply (add or remove as needed)
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

            // Apply a theme to each page based on its index
            for (int i = 0; i < diagram.Pages.Count; i++)
            {
                Page page = diagram.Pages[i];

                // Select a theme cyclically from the array
                PresetThemeValue selectedTheme = themes[i % themes.Length];
                page.PresetTheme = selectedTheme;

                // Optionally set a theme variant (Variant1‑Variant4) based on index
                page.PresetThemeVariant = (PresetThemeVariantValue)(i % 4);
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
