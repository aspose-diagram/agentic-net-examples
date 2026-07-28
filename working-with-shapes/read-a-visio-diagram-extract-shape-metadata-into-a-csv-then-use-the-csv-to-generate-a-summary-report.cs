using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Input Visio file, CSV output, and report output can be passed as arguments.
        string visioPath = args.Length > 0 ? args[0] : "input.vsdx";
        // Guard to ensure the input Visio file exists.
        if (!File.Exists(visioPath))
        {
            Console.Error.WriteLine($"File not found: {visioPath}");
            return;
        }
        string csvPath = args.Length > 1 ? args[1] : "shapes.csv";
        string reportPath = args.Length > 2 ? args[2] : "report.txt";

        try
        {
            // Load the Visio diagram.
            Diagram diagram = new Diagram(visioPath);

            // Extract shape metadata and write to CSV.
            ExtractMetadataToCsv(diagram, csvPath);

            // Generate a summary report from the CSV.
            GenerateReportFromCsv(csvPath, reportPath);
        }
        catch (Exception ex)
        {
            // Log any errors that occur during processing.
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }

    private static void ExtractMetadataToCsv(Diagram diagram, string csvFilePath)
    {
        try
        {
            // Ensure the directory for the CSV exists.
            string directory = Path.GetDirectoryName(csvFilePath);
            if (!string.IsNullOrEmpty(directory) && !Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }

            using (StreamWriter writer = new StreamWriter(csvFilePath, false))
            {
                // CSV header.
                writer.WriteLine("ShapeID,Name,NameU,Master,Text");

                // Iterate through all pages.
                foreach (Page page in diagram.Pages)
                {
                    // Iterate through all shapes on the page.
                    foreach (Shape shape in page.Shapes)
                    {
                        // Shape ID.
                        long shapeId = shape.ID;

                        // Shape name and universal name.
                        string name = shape.Name ?? string.Empty;
                        string nameU = shape.NameU ?? string.Empty;

                        // Master name (if the shape is based on a master).
                        string masterName = string.Empty;
                        if (shape.Master != null)
                        {
                            masterName = shape.Master.Name ?? string.Empty;
                        }

                        // Plain text of the shape (concatenated).
                        string text = string.Empty;
                        if (shape.Text != null && shape.Text.Value != null)
                        {
                            // Use the Text property to get plain text.
                            text = shape.Text.Value.Text ?? string.Empty;
                        }

                        // Replace line breaks and commas to keep CSV well‑formed.
                        text = text.Replace("\r", " ").Replace("\n", " ").Replace(",", " ");

                        // Write CSV line.
                        writer.WriteLine($"{shapeId},{EscapeCsv(name)},{EscapeCsv(nameU)},{EscapeCsv(masterName)},{EscapeCsv(text)}");
                    }
                }
            }

            Console.WriteLine($"Metadata extracted to CSV: {csvFilePath}");
        }
        catch (Exception ex)
        {
            // Log any errors that occur while extracting metadata.
            Console.Error.WriteLine($"Error extracting metadata: {ex.Message}");
        }
    }

    private static string EscapeCsv(string value)
    {
        if (value == null)
            return string.Empty;

        // Enclose in double quotes if the value contains a comma or quote.
        if (value.Contains(",") || value.Contains("\""))
        {
            string escaped = value.Replace("\"", "\"\"");
            return $"\"{escaped}\"";
        }
        return value;
    }

    private static void GenerateReportFromCsv(string csvFilePath, string reportFilePath)
    {
        if (!File.Exists(csvFilePath))
        {
            Console.WriteLine($"CSV file not found: {csvFilePath}");
            return;
        }

        // Dictionaries to hold summary data.
        int totalShapes = 0;
        Dictionary<string, int> masterCounts = new Dictionary<string, int>(StringComparer.OrdinalIgnoreCase);

        using (StreamReader reader = new StreamReader(csvFilePath))
        {
            // Read header line.
            string headerLine = reader.ReadLine();
            if (headerLine == null)
            {
                Console.WriteLine("CSV file is empty.");
                return;
            }

            // Process each data line.
            string line;
            while ((line = reader.ReadLine()) != null)
            {
                // Simple CSV split (assuming no commas inside quoted fields after extraction).
                // For robustness, a full CSV parser would be needed, but this suffices for the generated file.
                string[] parts = SplitCsvLine(line);
                if (parts.Length < 5)
                    continue; // Skip malformed lines.

                totalShapes++;

                string master = parts[3];
                if (string.IsNullOrWhiteSpace(master))
                    master = "(No Master)";

                if (masterCounts.ContainsKey(master))
                    masterCounts[master]++;
                else
                    masterCounts[master] = 1;
            }
        }

        // Build report content.
        List<string> reportLines = new List<string>();
        reportLines.Add("Visio Diagram Shape Summary Report");
        reportLines.Add($"Generated on: {DateTime.Now}");
        reportLines.Add($"Total shapes processed: {totalShapes}");
        reportLines.Add(string.Empty);
        reportLines.Add("Shapes per Master:");
        foreach (KeyValuePair<string, int> kvp in masterCounts)
        {
            reportLines.Add($"- {kvp.Key}: {kvp.Value}");
        }

        // Write report to console.
        Console.WriteLine();
        foreach (string line in reportLines)
        {
            Console.WriteLine(line);
        }

        // Optionally write report to a file.
        try
        {
            File.WriteAllLines(reportFilePath, reportLines);
            Console.WriteLine($"\nReport saved to: {reportFilePath}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"\nFailed to write report file: {ex.Message}");
        }
    }

    private static string[] SplitCsvLine(string line)
    {
        // Basic CSV splitter handling quoted fields.
        List<string> fields = new List<string>();
        bool inQuotes = false;
        int start = 0;

        for (int i = 0; i < line.Length; i++)
        {
            if (line[i] == '\"')
            {
                inQuotes = !inQuotes;
            }
            else if (line[i] == ',' && !inQuotes)
            {
                fields.Add(UnescapeCsv(line.Substring(start, i - start)));
                start = i + 1;
            }
        }

        // Add the last field.
        if (start <= line.Length)
        {
            fields.Add(UnescapeCsv(line.Substring(start)));
        }

        return fields.ToArray();
    }

    private static string UnescapeCsv(string field)
    {
        // Remove surrounding quotes if present and unescape double quotes.
        if (field.StartsWith("\"") && field.EndsWith("\""))
        {
            string inner = field.Substring(1, field.Length - 2);
            return inner.Replace("\"\"", "\"");
        }
        return field;
    }
}