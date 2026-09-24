using System.IO;
using System;
using Aspose.Diagram;

public static class ThemeHelper
{
    /// <summary>
    /// Applies a preset theme to the specified shape.
    /// </summary>
    /// <param name="shape">The shape to which the theme will be applied.</param>
    /// <param name="themeName">Name of the preset theme (e.g., "Bubble").</param>
    /// <returns>True if the theme was applied successfully; otherwise, false.</returns>
    public static bool ApplyThemeToShape(Shape shape, string themeName)
    {
        // Validate inputs
        if (shape == null || string.IsNullOrWhiteSpace(themeName))
            return false;

        // Attempt to convert the string to the corresponding PresetThemeValue enum member
        if (Enum.TryParse<PresetThemeValue>(themeName, true, out var presetTheme))
        {
            // Apply the theme to the shape
            shape.PresetTheme = presetTheme;

            // Optionally set a default variant (can be adjusted as needed)
            shape.PresetThemeVariant = PresetThemeVariantValue.Variant1;

            return true;
        }

        // Theme name does not match any known PresetThemeValue
        return false;
    }
}

class Program
{
    static void Main(string[] args)
    {
        // See classes above
    }
}
