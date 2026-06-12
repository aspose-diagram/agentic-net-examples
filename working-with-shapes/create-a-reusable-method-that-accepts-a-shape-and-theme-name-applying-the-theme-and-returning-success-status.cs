using System.IO;
using System;
using Aspose.Diagram;

public static class ShapeThemeHelper
{
    /// <summary>
    /// Applies a preset theme to the specified shape based on the theme name.
    /// Returns true if the theme was applied successfully; otherwise false.
    /// </summary>
    /// <param name="shape">The shape to which the theme will be applied.</param>
    /// <param name="themeName">The name of the preset theme (e.g., "Office", "Linear").</param>
    /// <returns>True if the theme was applied; false if the theme name is invalid or shape is null.</returns>
    public static bool ApplyPresetTheme(Shape shape, string themeName)
    {
        if (shape == null || string.IsNullOrWhiteSpace(themeName))
            return false;

        // Try to parse the theme name to the PresetThemeValue enum (ignore case)
        if (Enum.TryParse<PresetThemeValue>(themeName, ignoreCase: true, out var themeValue))
        {
            // Ensure the parsed value is defined in the enum
            if (Enum.IsDefined(typeof(PresetThemeValue), themeValue) && themeValue != PresetThemeValue.NoTheme)
            {
                shape.PresetTheme = themeValue;
                return true;
            }
        }

        // Theme name not recognized
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
