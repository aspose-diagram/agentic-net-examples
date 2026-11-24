using System;
using System.IO;
using System.Data;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Validate command‑line arguments.
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <inputDiagramPath> <outputDiagramPath>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

        // Guard against missing input file.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Ensure the output directory exists.
        string outputDir = Path.GetDirectoryName(outputPath);
        if (!string.IsNullOrEmpty(outputDir) && !Directory.Exists(outputDir))
        {
            try
            {
                Directory.CreateDirectory(outputDir);
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Failed to create output directory: {ex.Message}");
                return;
            }
        }

        // -----------------------------------------------------------------
        // Simulate reading theme preferences from a database.
        // In a real scenario you would use ADO.NET with a proper provider,
        // e.g. SqlConnection, OleDbConnection, etc.
        // -----------------------------------------------------------------
        DataTable themePreferences = new DataTable();
        themePreferences.Columns.Add("PageName", typeof(string));
        themePreferences.Columns.Add("Theme", typeof(string));

        // Sample data – replace with actual DB query results.
        themePreferences.Rows.Add("Page-1", "Bubble");
        themePreferences.Rows.Add("Page-2", "Clouds");
        // Add more rows as needed.

        // -----------------------------------------------------------------
        // Load the diagram, apply themes, and save the result.
        // -----------------------------------------------------------------
        try
        {
            using (Diagram diagram = new Diagram(inputPath))
            {
                foreach (DataRow row in themePreferences.Rows)
                {
                    string pageName = row["PageName"] as string;
                    string themeName = row["Theme"] as string;

                    if (string.IsNullOrEmpty(pageName) || string.IsNullOrEmpty(themeName))
                    {
                        Console.Error.WriteLine("Invalid preference row – missing PageName or Theme.");
                        continue;
                    }

                    // Retrieve the page by name.
                    Page page = diagram.Pages.GetPage(pageName);
                    if (page == null)
                    {
                        Console.Error.WriteLine($"Page not found: {pageName}");
                        continue;
                    }

                    // Map the theme name to the PresetThemeValue enum.
                    if (!Enum.TryParse<PresetThemeValue>(themeName, ignoreCase: true, out PresetThemeValue presetTheme))
                    {
                        Console.Error.WriteLine($"Unsupported theme '{themeName}' for page '{pageName}'.");
                        continue;
                    }

                    // Apply the preset theme to the page.
                    page.PresetTheme = presetTheme;

                    // Optionally, you could also set a variant if required:
                    // page.PresetThemeVariant = PresetThemeVariantValue.Variant1;
                }

                // Save the modified diagram.
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
                Console.WriteLine($"Diagram saved successfully to: {outputPath}");
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}