using System.IO;
using System;
using Aspose.Diagram;

public static class DiagramThemeHelper
{
    /// <summary>
    /// Applies a preset theme to the specified shape.
    /// </summary>
    /// <param name="shape">The shape to which the theme will be applied.</param>
    /// <param name="themeName">The name of the theme (e.g., "Office", "Linear", "Zephyr"). Case‑insensitive.</param>
    /// <returns>True if the theme was successfully applied; otherwise false.</returns>
    public static bool ApplyThemeToShape(Shape shape, string themeName)
    {
        // Validate input
        if (shape == null || string.IsNullOrWhiteSpace(themeName))
            return false;

        // Try to convert the theme name to the corresponding enum value
        if (Enum.TryParse<PresetThemeValue>(themeName, true, out var themeValue))
        {
            // Apply the theme using the PresetTheme property
            shape.PresetTheme = themeValue;
            return true;
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
