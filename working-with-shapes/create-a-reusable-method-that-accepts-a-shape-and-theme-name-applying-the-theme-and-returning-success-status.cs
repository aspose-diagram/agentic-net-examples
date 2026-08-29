using System;
using System.IO;
using Aspose.Diagram;

public static class DiagramHelper
{
    /// <summary>
    /// Applies a preset theme to the specified shape.
    /// </summary>
    /// <param name="shape">The shape to which the theme will be applied.</param>
    /// <param name="themeName">Name of the theme (e.g., "Office", "Linear", "Zephyr").</param>
    /// <returns>True if the theme was applied successfully; otherwise false.</returns>
    public static bool ApplyTheme(Shape shape, string themeName)
    {
        // Validate inputs
        if (shape == null || string.IsNullOrWhiteSpace(themeName))
            return false;

        // Try to convert the theme name to the corresponding PresetThemeValue enum value
        if (Enum.TryParse<PresetThemeValue>(themeName, true, out var themeValue))
        {
            // Ensure the parsed value is a defined enum member and not NoTheme
            if (Enum.IsDefined(typeof(PresetThemeValue), themeValue) && themeValue != PresetThemeValue.NoTheme)
            {
                // Apply the theme using the PresetTheme property (rule)
                shape.PresetTheme = themeValue;
                return true;
            }
        }

        // Theme name could not be mapped to a valid enum value
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
