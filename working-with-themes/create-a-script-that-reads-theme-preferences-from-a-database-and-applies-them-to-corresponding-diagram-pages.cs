using System.IO;
using System;
using System.Data;
using System.Data.SqlClient;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class ApplyThemeFromDatabase
{
    static void Main()
    {
        try
        {

            // Path to the diagram that will receive the themes
            string targetDiagramPath = @"C:\Diagrams\TargetDiagram.vsdx";

            // Load the target diagram (uses the provided load rule)
            Diagram targetDiagram = new Diagram(targetDiagramPath);

            // Connection string to the database that stores theme preferences
            string connectionString = @"Data Source=SERVER;Initial Catalog=ThemeDB;Integrated Security=True";

            // Query that returns page name (or index) and the desired theme name
            string query = @"SELECT PageName, ThemeName FROM PageThemePreferences";

            using (SqlConnection conn = new SqlConnection(connectionString))
            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                conn.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        string pageName = reader.GetString(0);
                        string themeName = reader.GetString(1);

                        // Find the page in the diagram by name
                        Page page = null;
                        foreach (Page p in targetDiagram.Pages)
                        {
                            if (p.Name == pageName)
                            {
                                page = p;
                                break;
                            }
                        }

                        if (page == null)
                        {
                            Console.WriteLine($"Page '{pageName}' not found in diagram.");
                            continue;
                        }

                        // Map the theme name from the database to a PresetThemeValue enum
                        PresetThemeValue themeValue = MapThemeNameToEnum(themeName);

                        // Load a temporary diagram that contains the desired theme.
                        // Each theme is stored as a separate Visio file in a Themes folder.
                        string themeDiagramPath = $@"C:\Themes\{themeName}.vsdx";
                        Diagram themeDiagram = new Diagram(themeDiagramPath);

                        // Apply the theme to the target diagram (uses the provided CopyTheme rule)
                        targetDiagram.CopyTheme(themeDiagram);

                        // Optionally, set the page's quick style if needed
                        // page.PresetThemeQuickStyle = (PresetQuickStyleValue)themeValue; // Uncomment if quick style enum is available
                    }
                }
            }

            // Save the modified diagram (uses the provided save rule)
            string outputPath = @"C:\Diagrams\TargetDiagram_Themed.vsdx";
            targetDiagram.Save(outputPath, SaveFileFormat.Vsdx);

            // Clean up
            targetDiagram.Dispose();

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }

    // Helper method to convert a theme name string to the corresponding PresetThemeValue enum.
    private static PresetThemeValue MapThemeNameToEnum(string themeName)
    {
        return themeName switch
        {
            "Office" => PresetThemeValue.Office,
            "Linear" => PresetThemeValue.Linear,
            "Zephyr" => PresetThemeValue.Zephyr,
            "Integral" => PresetThemeValue.Integral,
            "Simple" => PresetThemeValue.Simple,
            "Whisp" => PresetThemeValue.Whisp,
            "Daybreak" => PresetThemeValue.Daybreak,
            "Parallel" => PresetThemeValue.Parallel,
            "Sequence" => PresetThemeValue.Sequence,
            "Slice" => PresetThemeValue.Slice,
            "Ion" => PresetThemeValue.Ion,
            "Retrospect" => PresetThemeValue.Retrospect,
            "Organic" => PresetThemeValue.Organic,
            "Bubble" => PresetThemeValue.Bubble,
            "Clouds" => PresetThemeValue.Clouds,
            "Gemstone" => PresetThemeValue.Gemstone,
            "Lines" => PresetThemeValue.Lines,
            "Facet" => PresetThemeValue.Facet,
            "Prominence" => PresetThemeValue.Prominence,
            "Smoke" => PresetThemeValue.Smoke,
            "Radiance" => PresetThemeValue.Radiance,
            "Shade" => PresetThemeValue.Shade,
            "Pencil" => PresetThemeValue.Pencil,
            "Pen" => PresetThemeValue.Pen,
            "Marker" => PresetThemeValue.Marker,
            "WhiteBoard" => PresetThemeValue.WhiteBoard,
            _ => PresetThemeValue.NoTheme
        };
    }
}
