using System;
using System.IO;
using Aspose.Diagram;

class ThemeLogger
{
    static void Main()
    {
        try
        {

            // Load an existing Visio diagram (replace with your file path)
            Diagram diagram = new Diagram(@"input.vsdx");

            // Path to the log file
            string logFilePath = @"theme_log.txt";

            // Open the log file for appending
            using (StreamWriter logWriter = new StreamWriter(logFilePath, true))
            {
                // Iterate through all pages and shapes
                foreach (Page page in diagram.Pages)
                {
                    foreach (Shape shape in page.Shapes)
                    {
                        // Apply a preset theme to the shape (choose any theme you need)
                        shape.PresetTheme = PresetThemeValue.Office;

                        // Log the event: timestamp (ISO 8601) and shape identifier
                        string logEntry = $"{DateTime.UtcNow:O}\tShapeID:{shape.ID}";
                        logWriter.WriteLine(logEntry);
                    }
                }
            }

            // Save the modified diagram (replace with your desired output path)
            diagram.Save(@"output.vsdx", SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
