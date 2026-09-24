using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
    {
        // Path to the input Visio file
        private const string InputFilePath = "input.vsdx";
        // Path to the output Visio file after theme application
        private const string OutputFilePath = "output.vsdx";
        // Path to the log file where theme application events are recorded
        private const string LogFilePath = "theme_application_log.txt";

        static void Main()
        {
            try
            {

                // Ensure the log file exists and is empty at start
                File.WriteAllText(LogFilePath, string.Empty);

                // Load the diagram from the specified file
                Diagram diagram = new Diagram(InputFilePath);

                // Iterate through all pages and shapes to apply a theme
                foreach (Page page in diagram.Pages)
                {
                    foreach (Shape shape in page.Shapes)
                    {
                        // Apply a preset theme to the shape
                        shape.PresetTheme = PresetThemeValue.Bubble;
                        shape.PresetThemeVariant = PresetThemeVariantValue.Variant1;

                        // Log the theme application event
                        LogThemeApplication(shape.ID);
                    }
                }

                // Save the modified diagram using the appropriate SaveFileFormat
                diagram.Save(OutputFilePath, SaveFileFormat.Vsdx);

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }

        /// <summary>
        /// Writes a log entry with the current timestamp and the shape identifier.
        /// </summary>
        /// <param name="shapeId">The unique identifier of the shape.</param>
        private static void LogThemeApplication(long shapeId)
        {
            string timestamp = DateTime.Now.ToString("o"); // ISO 8601 format
            string logEntry = $"{timestamp} - Theme applied to shape ID: {shapeId}";
            // Append the log entry to the log file
            File.AppendAllText(LogFilePath, logEntry + Environment.NewLine);
        }
    }