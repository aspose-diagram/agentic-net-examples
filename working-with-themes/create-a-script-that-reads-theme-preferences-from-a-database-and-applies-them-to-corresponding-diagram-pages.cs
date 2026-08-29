using System;
using System.IO;
using System.Data;
using System.Data.SqlClient;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Expect three arguments: input diagram path, output diagram path, and DB connection string.
        if (args.Length < 3)
        {
            Console.Error.WriteLine("Usage: <program> <inputDiagramPath> <outputDiagramPath> <connectionString>");
            return;
        }

        string inputPath = args[0];
        // Guard: ensure the input diagram file exists.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        string outputPath = args[1];
        string connectionString = args[2];

        Diagram diagram = null;

        try
        {
            // Load the Visio diagram from the specified file.
            diagram = new Diagram(inputPath);
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error loading diagram: {ex.Message}");
            return;
        }

        // Open a SQL connection to read theme preferences.
        using (SqlConnection conn = new SqlConnection(connectionString))
        {
            try
            {
                conn.Open();
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Database connection failed: {ex.Message}");
                return;
            }

            // Query expects a table named PageThemes with columns PageName (string) and ThemeName (string).
            const string query = "SELECT PageName, ThemeName FROM PageThemes";

            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                try
                {
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            // Retrieve page name and desired theme from the current row.
                            string pageName = reader["PageName"] as string;
                            string themeName = reader["ThemeName"] as string;

                            if (string.IsNullOrWhiteSpace(pageName) || string.IsNullOrWhiteSpace(themeName))
                                continue; // Skip incomplete rows.

                            // Attempt to locate the page by its name (case‑sensitive match).
                            Page page = diagram.Pages.GetPage(pageName);
                            if (page == null)
                            {
                                Console.Error.WriteLine($"Page not found: {pageName}");
                                continue;
                            }

                            // Parse the theme name into the PresetThemeValue enum.
                            if (Enum.TryParse<PresetThemeValue>(themeName, ignoreCase: true, out var themeEnum))
                            {
                                // Apply the theme to the page.
                                page.PresetTheme = themeEnum;
                            }
                            else
                            {
                                Console.Error.WriteLine($"Invalid theme '{themeName}' for page '{pageName}'.");
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.Error.WriteLine($"Error reading theme data: {ex.Message}");
                    return;
                }
            }
        }

        try
        {
            // Save the modified diagram to the output path using VSDX format.
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error saving diagram: {ex.Message}");
        }
    }
}