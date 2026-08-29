using System;
using System.IO;
using Aspose.Diagram;

class ThemeApplicationLogger
{
    static void Main()
    {
        try
        {

            // Paths for the source diagram and the log file
            string diagramPath = "input.vsdx";
            string logPath = "ThemeApplicationLog.txt";

            // Load the Visio diagram
            Diagram diagram = new Diagram(diagramPath);

            // Ensure the log file is empty before starting
            File.WriteAllText(logPath, string.Empty);

            // Iterate through all pages and shapes in the diagram
            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    // Apply a preset theme to the shape (example: Office theme)
                    shape.PresetTheme = PresetThemeValue.Office;

                    // Build log entry with timestamp and shape identifier (ID)
                    string logEntry = $"{DateTime.Now:yyyy-MM-dd HH:mm:ss}\tPage:{page.ID}\tShapeID:{shape.ID}\tTheme:Office";

                    // Append the log entry to the text file
                    File.AppendAllText(logPath, logEntry + Environment.NewLine);
                }
            }

            // Save the modified diagram (optional, if you want to keep changes)
            diagram.Save("output.vsdx", SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
