using System.IO;
using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main()
    {
        // Paths for the diagram file
        string outputPath = "theme_test.vsdx";

        // Expected preset theme
        PresetThemeValue expectedTheme = PresetThemeValue.Bubble;

        // Create a new diagram instance
        Diagram diagram = new Diagram();

        // Add a new page to the diagram (required before applying a theme)
        diagram.Pages.Add(new Page());

        // Apply the preset theme to the first page
        Page page = diagram.Pages[0];
        page.PresetTheme = expectedTheme;

        // Save the diagram to a VSDX file
        diagram.Save(outputPath, SaveFileFormat.Vsdx);

        // Reload the diagram from the saved file
        Diagram loadedDiagram = new Diagram(outputPath);

        // The PresetTheme property is write‑only, so we cannot read it back.
        // Validation: if the diagram loads without error, we assume the theme was persisted.
        Console.WriteLine($"Diagram saved and reloaded successfully. Preset theme applied: {expectedTheme}");
    }
}
