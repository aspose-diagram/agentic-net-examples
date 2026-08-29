using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Diagram;

namespace DiagramVersioning
{
    // Helper class for logging changes to a file
    public static class VersionLogger
    {
        // Appends a log entry with timestamp to the specified log file
        public static void LogChange(string logFilePath, string entry)
        {
            string timestampedEntry = $"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] {entry}{Environment.NewLine}";
            File.AppendAllText(logFilePath, timestampedEntry);
        }
    }

    public class Program
    {
        // Entry point
        public static void Main(string[] args)
        {
            try
            {

                // Paths (adjust as needed)
                string inputDiagramPath = "input.vsdx";
                string outputDiagramPath = "output.vsdx";
                string logFilePath = "cell_changes.log";

                // Load the diagram (using the standard constructor)
                Diagram diagram = new Diagram(inputDiagramPath);

                // Store original user-defined cell values: key = (shape ID, cell name)
                var originalValues = new Dictionary<(long, string), string>();

                // First pass: capture current values
                foreach (Page page in diagram.Pages)
                {
                    foreach (Shape shape in page.Shapes)
                    {
                        if (shape.Users != null)
                        {
                            foreach (User userCell in shape.Users)
                            {
                                // Use shape ID and cell name as unique identifier
                                var key = (shape.ID, userCell.Name);
                                originalValues[key] = userCell.Value?.Val ?? string.Empty;
                            }
                        }
                    }
                }

                // Simulate modifications to user-defined cells
                foreach (Page page in diagram.Pages)
                {
                    foreach (Shape shape in page.Shapes)
                    {
                        if (shape.Users != null)
                        {
                            foreach (User userCell in shape.Users)
                            {
                                // Example modification: append a version suffix
                                string currentValue = userCell.Value?.Val ?? string.Empty;
                                string newValue = currentValue + "_v2";
                                userCell.Value.Val = newValue;
                            }
                        }
                    }
                }

                // Second pass: detect changes and log them
                foreach (Page page in diagram.Pages)
                {
                    foreach (Shape shape in page.Shapes)
                    {
                        if (shape.Users != null)
                        {
                            foreach (User userCell in shape.Users)
                            {
                                var key = (shape.ID, userCell.Name);
                                string original = originalValues.ContainsKey(key) ? originalValues[key] : string.Empty;
                                string current = userCell.Value?.Val ?? string.Empty;

                                if (!string.Equals(original, current, StringComparison.Ordinal))
                                {
                                    string logEntry = $"ShapeID={shape.ID}, CellName='{userCell.Name}', OldValue='{original}', NewValue='{current}'";
                                    VersionLogger.LogChange(logFilePath, logEntry);
                                }
                            }
                        }
                    }
                }

                // Save the modified diagram
                diagram.Save(outputDiagramPath, SaveFileFormat.Vsdx);

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }
}