using System;
using System.IO;
using System.Text;
using Aspose.Diagram;

class Program
    {
        static void Main(string[] args)
        {
            // Directory containing Visio diagram files (VSD, VSDX, VDX, etc.)
            string diagramsFolder = @"C:\Diagrams";

            // Output CSV file path
            string csvReportPath = @"C:\Reports\VbaModuleCounts.csv";

            // Prepare CSV content with header
            var csvBuilder = new StringBuilder();
            csvBuilder.AppendLine("DiagramFile,ModuleCount");

            // Get all Visio files in the folder (including subfolders)
            string[] diagramFiles = Directory.GetFiles(diagramsFolder, "*.*", SearchOption.AllDirectories);
            foreach (string filePath in diagramFiles)
            {
                // Filter supported Visio extensions
                string extension = Path.GetExtension(filePath).ToLowerInvariant();
                if (extension != ".vsd" && extension != ".vsdx" && extension != ".vdx" && extension != ".vsdm")
                    continue;

                try
                {
                    // Load the diagram
                    Diagram diagram = new Diagram(filePath);

                    // Count VBA modules; VbaProject may be null if no VBA present
                    int moduleCount = diagram.VbaProject?.Modules?.Count ?? 0;

                    // Append result to CSV
                    string line = $"{EscapeCsv(filePath)},{moduleCount}";
                    csvBuilder.AppendLine(line);
                }
                catch (Exception ex)
                {
                    // In case of load failure, log the error and continue
                    Console.WriteLine($"Error processing '{filePath}': {ex.Message}");
                }
            }

            // Ensure the output directory exists
            Directory.CreateDirectory(Path.GetDirectoryName(csvReportPath));

            // Write CSV to file
            File.WriteAllText(csvReportPath, csvBuilder.ToString(), Encoding.UTF8);

            Console.WriteLine($"VBA module count report generated at: {csvReportPath}");
        }

        // Helper to escape CSV fields that may contain commas or quotes
        private static string EscapeCsv(string field)
        {
            if (field.Contains(",") || field.Contains("\"") || field.Contains("\n"))
            {
                string escaped = field.Replace("\"", "\"\"");
                return $"\"{escaped}\"";
            }
            return field;
        }
    }