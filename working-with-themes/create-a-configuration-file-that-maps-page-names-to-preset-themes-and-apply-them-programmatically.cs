using System;
using System.Collections.Generic;
using System.IO;
using Aspose.Diagram;
using System.Text.Json;

// Define a class that represents the mapping from a page name to its theme settings.
public class PageThemeMapping
{
    public string PageName { get; set; }
    public PresetThemeValue Theme { get; set; }
    public PresetThemeVariantValue? Variant { get; set; }
    public PresetQuickStyleValue? QuickStyle { get; set; }
}

public class ThemeApplier
{
    // Path to the configuration file (JSON format).
    private const string ConfigFilePath = "PageThemeConfig.json";

    // Path to the source Visio diagram.
    private const string InputDiagramPath = "InputDiagram.vsdx";

    // Path where the modified diagram will be saved.
    private const string OutputDiagramPath = "OutputDiagram.vsdx";

    public static void Main()
    {
        try
        {

            // Load the configuration that maps page names to theme values.
            List<PageThemeMapping> mappings = LoadConfiguration(ConfigFilePath);

            // Load the Visio diagram using Aspose.Diagram (lifecycle rule: load).
            Diagram diagram = new Diagram(InputDiagramPath);

            // Apply the preset themes to the corresponding pages.
            foreach (Page page in diagram.Pages)
            {
                // Find a mapping entry that matches the current page name.
                PageThemeMapping mapping = mappings.Find(m => string.Equals(m.PageName, page.Name, StringComparison.OrdinalIgnoreCase));

                if (mapping != null)
                {
                    // Apply the main preset theme.
                    page.PresetTheme = mapping.Theme;

                    // Optionally apply a variant if it is specified.
                    if (mapping.Variant.HasValue)
                    {
                        page.PresetThemeVariant = mapping.Variant.Value;
                    }

                    // Optionally apply a quick style if it is specified.
                    if (mapping.QuickStyle.HasValue)
                    {
                        page.PresetThemeQuickStyle = mapping.QuickStyle.Value;
                    }
                }
            }

            // Save the modified diagram (lifecycle rule: save).
            diagram.Save(OutputDiagramPath, SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }

    // Helper method to read the JSON configuration file and deserialize it into a list of mappings.
    private static List<PageThemeMapping> LoadConfiguration(string path)
    {
        if (!File.Exists(path))
        {
            throw new FileNotFoundException($"Configuration file not found: {path}");
        }

        string json = File.ReadAllText(path);
        var options = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true,
            Converters = { new System.Text.Json.Serialization.JsonStringEnumConverter() }
        };
        return JsonSerializer.Deserialize<List<PageThemeMapping>>(json, options);
    }
}