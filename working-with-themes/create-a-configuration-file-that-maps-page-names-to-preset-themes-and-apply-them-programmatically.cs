using System;
using System.IO;
using System.Collections.Generic;
using System.Text.Json;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

namespace DiagramThemeApplier
{
    // Represents a mapping between a page name and its desired theme.
    public class PageThemeMapping
    {
        public string PageName { get; set; }
        public string Theme { get; set; }
        public string Variant { get; set; }
    }

    class Program
    {
        static void Main()
        {
            try
            {

                // Paths to the diagram file and the JSON configuration file.
                string diagramPath = "input.vsdx";
                string configPath = "pageThemes.json";

                // Load the diagram.
                Diagram diagram = new Diagram(diagramPath);

                // Read and deserialize the configuration file.
                if (!File.Exists(configPath))
                    throw new FileNotFoundException($"Configuration file not found: {configPath}");

                string json = File.ReadAllText(configPath);
                List<PageThemeMapping> mappings = JsonSerializer.Deserialize<List<PageThemeMapping>>(json);

                // Apply the preset themes to the corresponding pages.
                foreach (PageThemeMapping mapping in mappings)
                {
                    // Retrieve the page by name; GetPage returns null if not found.
                    Page page = diagram.Pages.GetPage(mapping.PageName);
                    if (page == null)
                    {
                        Console.WriteLine($"Page \"{mapping.PageName}\" not found in diagram.");
                        continue;
                    }

                    // Parse the theme enum value.
                    if (Enum.TryParse<PresetThemeValue>(mapping.Theme, out var themeEnum))
                    {
                        page.PresetTheme = themeEnum;
                    }
                    else
                    {
                        Console.WriteLine($"Invalid theme \"{mapping.Theme}\" for page \"{mapping.PageName}\".");
                        continue;
                    }

                    // Parse the variant enum value if provided.
                    if (!string.IsNullOrWhiteSpace(mapping.Variant) &&
                        Enum.TryParse<PresetThemeVariantValue>(mapping.Variant, out var variantEnum))
                    {
                        page.PresetThemeVariant = variantEnum;
                    }
                }

                // Save the updated diagram.
                string outputPath = "output.vsdx";
                diagram.Save(outputPath, SaveFileFormat.Vsdx);

                Console.WriteLine($"Diagram saved with applied themes to \"{outputPath}\".");

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }
}