using System.IO;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main()
    {
        try
        {

            // Load the target diagram using the provided constructor (load rule)
            Diagram diagram = new Diagram("target.vsdx");

            // Retrieve theme preferences from the database
            Dictionary<int, int> pageThemeMap = GetPageThemePreferences();

            // Apply the retrieved theme to each corresponding page
            foreach (Page page in diagram.Pages)
            {
                if (pageThemeMap.TryGetValue(page.ID, out int themeInt))
                {
                    // Cast the integer value to the PresetQuickStyleValue enum and assign
                    page.PresetThemeQuickStyle = (PresetQuickStyleValue)themeInt;
                }
            }

            // Save the updated diagram using the provided Save method (save rule)
            DiagramSaveOptions saveOptions = new DiagramSaveOptions(SaveFileFormat.Vdx);
            diagram.Save("target_updated.vdx", saveOptions);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }

    // Reads theme preferences from a database table and returns a mapping of Page ID to theme value
    static Dictionary<int, int> GetPageThemePreferences()
    {
        var map = new Dictionary<int, int>();

        // Replace with your actual connection string
        string connectionString = "Data Source=SERVER;Initial Catalog=Database;Integrated Security=True";

        // Expected table schema: PageId (int), ThemeValue (int) where ThemeValue matches PresetQuickStyleValue enum
        string query = "SELECT PageId, ThemeValue FROM PageThemePreferences";

        using (SqlConnection conn = new SqlConnection(connectionString))
        using (SqlCommand cmd = new SqlCommand(query, conn))
        {
            conn.Open();
            using (SqlDataReader reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    int pageId = reader.GetInt32(0);
                    int themeValue = reader.GetInt32(1);
                    map[pageId] = themeValue;
                }
            }
        }

        return map;
    }
}
