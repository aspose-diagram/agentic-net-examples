using System.IO;
using System;
using System.Data;
using Aspose.Diagram;

using Aspose.Diagram.Saving; // Required for SaveFileFormat enum

class Program
{
    static void Main()
    {
        try
        {

            // ----- Simulated database table with theme preferences -----
            DataTable themeTable = new DataTable();
            themeTable.Columns.Add("PageName", typeof(string));
            themeTable.Columns.Add("PresetTheme", typeof(string));
            themeTable.Columns.Add("PresetThemeVariant", typeof(string));

            // Sample data – in a real scenario this would come from a DB query
            themeTable.Rows.Add("Page-1", "Bubble", "Variant1");
            themeTable.Rows.Add("Page-2", "Bubble", "Variant2");
            themeTable.Rows.Add("Summary", "Bubble", "Variant3");

            // ----- Load the Visio diagram -----
            string inputPath = "input.vsdx"; // replace with actual file path
            Diagram diagram = new Diagram(inputPath);

            // ----- Apply theme preferences to each page -----
            foreach (DataRow row in themeTable.Rows)
            {
                string pageName = row["PageName"] as string;
                string themeName = row["PresetTheme"] as string;
                string variantName = row["PresetThemeVariant"] as string;

                // Retrieve the page by its name; skip if not found
                Page page = diagram.Pages.GetPage(pageName);
                if (page == null)
                {
                    Console.WriteLine($"Page \"{pageName}\" not found in diagram.");
                    continue;
                }

                // Parse enum values from strings (case‑insensitive)
                if (Enum.TryParse<PresetThemeValue>(themeName, true, out var themeEnum) &&
                    Enum.TryParse<PresetThemeVariantValue>(variantName, true, out var variantEnum))
                {
                    // Apply the theme and variant to the page
                    page.PresetTheme = themeEnum;
                    page.PresetThemeVariant = variantEnum;
                    Console.WriteLine($"Applied theme {themeEnum} ({variantEnum}) to page \"{pageName}\".");
                }
                else
                {
                    Console.WriteLine($"Invalid theme or variant for page \"{pageName}\".");
                }
            }

            // ----- Save the updated diagram -----
            string outputPath = "output.vsdx"; // replace with desired output path
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Diagram saved to \"{outputPath}\".");

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
