using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using Aspose.Diagram;

class Program
{
    // Path to the diagram file to be processed
    private const string InputDiagramPath = "input.vsdx";
    // Path where the modified diagram will be saved
    private const string OutputDiagramPath = "output.vsdx";
    // Path to the configuration file that maps page names to preset themes
    private const string ConfigPath = "pageThemeConfig.json";

    static void Main()
    {
        try
        {

            // Load the diagram (uses the provided load rule)
            Diagram diagram = new Diagram(InputDiagramPath);

            // Load the configuration mapping page names to theme names
            Dictionary<string, string> pageThemeMap = LoadConfiguration(ConfigPath);

            // Apply the preset theme to each page according to the configuration
            foreach (Page page in diagram.Pages)
            {
                if (pageThemeMap.TryGetValue(page.Name, out string themeName))
                {
                    // Convert the theme name string to the corresponding enum value
                    if (Enum.TryParse<PresetThemeValue>(themeName, ignoreCase: true, out var themeValue))
                    {
                        // Apply the preset theme to the page (uses the provided property)
                        page.PresetTheme = themeValue;
                    }
                    else
                    {
                        Console.WriteLine($"Warning: Theme '{themeName}' is not a valid PresetThemeValue.");
                    }
                }
            }

            // Save the modified diagram (uses the provided save rule)
            diagram.Save(OutputDiagramPath, SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }

    // Helper method to read the JSON configuration file
    private static Dictionary<string, string> LoadConfiguration(string path)
    {
        if (!File.Exists(path))
        {
            throw new FileNotFoundException($"Configuration file not found: {path}");
        }

        string json = File.ReadAllText(path);
        // Expected JSON format: { "PageName1": "Office", "PageName2": "Linear", ... }
        return JsonSerializer.Deserialize<Dictionary<string, string>>(json);
    }
}
