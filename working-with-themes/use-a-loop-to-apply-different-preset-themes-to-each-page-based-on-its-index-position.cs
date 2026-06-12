using System.IO;
using System;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        try
        {

            // Load an existing Visio diagram
            Diagram diagram = new Diagram("input.vsdx");

            // Define a list of preset themes to apply
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
                PresetThemeValue.Slice,
                PresetThemeValue.Ion,
                PresetThemeValue.Retrospect,
                PresetThemeValue.Organic,
                PresetThemeValue.Bubble,
                PresetThemeValue.Clouds,
                PresetThemeValue.Gemstone,
                PresetThemeValue.Lines,
                PresetThemeValue.Facet,
                PresetThemeValue.Prominence,
                PresetThemeValue.Smoke,
                PresetThemeValue.Radiance,
                PresetThemeValue.Shade,
                PresetThemeValue.Pencil,
                PresetThemeValue.Pen,
                PresetThemeValue.Marker,
                PresetThemeValue.WhiteBoard
            };

            // Apply a theme to each page based on its index
            for (int i = 0; i < diagram.Pages.Count; i++)
            {
                Page page = diagram.Pages[i];
                // Cycle through the theme array if there are more pages than themes
                PresetThemeValue selectedTheme = themes[i % themes.Length];
                page.PresetTheme = selectedTheme;
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
