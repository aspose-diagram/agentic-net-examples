using System;
using System.IO;
using Aspose.Diagram;

class ThemeApplicationLogger
{
    static void Main()
    {
        try
        {

            // Load an existing Visio diagram
            Diagram diagram = new Diagram("input.vsdx");

            // Path to the log file
            string logFilePath = "ThemeApplicationLog.txt";

            // Ensure the log file is empty before starting
            File.WriteAllText(logFilePath, string.Empty);

            // Open a StreamWriter for appending log entries
            using (StreamWriter logWriter = new StreamWriter(logFilePath, true))
            {
                // Iterate through all pages and shapes in the diagram
                foreach (Page page in diagram.Pages)
                {
                    foreach (Shape shape in page.Shapes)
                    {
                        // Example: Apply the Office preset theme to the shape
                        shape.PresetTheme = PresetThemeValue.Office;

                        // Log the theme application event
                        string logEntry = string.Format(
                            "[{0}] Shape ID {1} applied theme {2}",
                            DateTime.Now.ToString("o"), // ISO 8601 timestamp
                            shape.ID,
                            PresetThemeValue.Office);

                        logWriter.WriteLine(logEntry);
                    }
                }
            }

            // Save the modified diagram
            diagram.Save("output.vsdx", SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
