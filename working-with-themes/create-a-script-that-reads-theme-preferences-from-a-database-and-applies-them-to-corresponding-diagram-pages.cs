using System;
using System.IO;
using System.Data;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Input and output file paths (can be passed as command‑line arguments)
        string inputPath = args.Length > 0 ? args[0] : "Themes1.vsdx";
        string outputPath = args.Length > 1 ? args[1] : "Themes1_Updated.vsdx";

        // Guard against missing input file
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Simulate reading theme preferences from a database
        DataTable themePreferences = GetThemePreferences();

        try
        {
            // Load the diagram inside a using block as required
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Iterate over each preference row and apply the theme to the matching page
                foreach (DataRow row in themePreferences.Rows)
                {
                    string pageName = row["PageName"] as string;
                    string themeName = row["PresetTheme"] as string;
                    string variantName = row["PresetThemeVariant"] as string;

                    // Retrieve the page by name; GetPage returns null if not found
                    Page page = diagram.Pages.GetPage(pageName);
                    if (page == null)
                    {
                        Console.Error.WriteLine($"Page not found: {pageName}");
                        continue;
                    }

                    // Parse the PresetTheme enum value
                    if (!Enum.TryParse<PresetThemeValue>(themeName, true, out PresetThemeValue themeEnum))
                    {
                        Console.Error.WriteLine($"Invalid PresetTheme value: {themeName}");
                        continue;
                    }

                    // Parse the PresetThemeVariant enum value
                    if (!Enum.TryParse<PresetThemeVariantValue>(variantName, true, out PresetThemeVariantValue variantEnum))
                    {
                        Console.Error.WriteLine($"Invalid PresetThemeVariant value: {variantName}");
                        continue;
                    }

                    // Apply the theme and variant to the page (write‑only properties)
                    page.PresetTheme = themeEnum;
                    page.PresetThemeVariant = variantEnum;
                }

                // Save the modified diagram to the output path
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }

    // Simulated method that would normally retrieve data from a database
    static DataTable GetThemePreferences()
    {
        DataTable dt = new DataTable();
        dt.Columns.Add("PageName", typeof(string));
        dt.Columns.Add("PresetTheme", typeof(string));
        dt.Columns.Add("PresetThemeVariant", typeof(string));

        // Example rows – replace with real data retrieval logic
        dt.Rows.Add("Page-1", "Bubble", "Variant1");
        dt.Rows.Add("Page-2", "Bubble", "Variant2");

        return dt;
    }
}