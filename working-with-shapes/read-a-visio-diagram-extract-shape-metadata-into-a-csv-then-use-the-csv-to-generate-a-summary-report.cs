using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Expect three arguments: input Visio file, output CSV file, summary report file
        if (args.Length < 3)
        {
            Console.Error.WriteLine("Usage: <program> <inputVisioPath> <outputCsvPath> <reportPath>");
            return;
        }

        string visioPath = args[0];
        string csvPath = args[1];
        string reportPath = args[2];

        // Guard: ensure the Visio file exists
        if (!File.Exists(visioPath))
        {
            Console.Error.WriteLine($"Input Visio file not found: {visioPath}");
            return;
        }

        try
        {
            // Load the Visio diagram inside a using block as required
            using (Diagram diagram = new Diagram(visioPath))
            {
                // ---------- Extract shape metadata and write to CSV ----------
                using (StreamWriter csvWriter = new StreamWriter(csvPath, false))
                {
                    // Write CSV header
                    csvWriter.WriteLine("\"PageName\",\"ShapeID\",\"MasterName\",\"Text\"");

                    // Iterate through pages and shapes
                    foreach (Page page in diagram.Pages)
                    {
                        // Page.Name is a plain string (no .Value)
                        string pageName = page.Name ?? string.Empty;

                        foreach (Shape shape in page.Shapes)
                        {
                            // Shape ID
                            long shapeId = shape.ID;

                            // Master name (if any)
                            string masterName = shape.Master != null ? shape.Master.Name ?? string.Empty : string.Empty;

                            // Shape text (plain)
                            string shapeText = shape.Text != null ? shape.Text.Value.Text ?? string.Empty : string.Empty;

                            // Escape double quotes by doubling them
                            string escPageName = pageName.Replace("\"", "\"\"");
                            string escMasterName = masterName.Replace("\"", "\"\"");
                            string escShapeText = shapeText.Replace("\"", "\"\"");

                            // Write CSV line with quoted fields
                            csvWriter.WriteLine($"\"{escPageName}\",\"{shapeId}\",\"{escMasterName}\",\"{escShapeText}\"");
                        }
                    }
                }

                // ---------- Read CSV and generate summary report ----------
                int totalShapes = 0;
                Dictionary<string, int> masterCounts = new Dictionary<string, int>(StringComparer.OrdinalIgnoreCase);

                using (StreamReader csvReader = new StreamReader(csvPath))
                {
                    // Read header line
                    string headerLine = csvReader.ReadLine();

                    // Process each data line
                    string line;
                    while ((line = csvReader.ReadLine()) != null)
                    {
                        // Simple CSV split assuming no commas inside quoted fields after our own escaping
                        string[] parts = SplitCsvLine(line);
                        if (parts.Length < 4)
                            continue; // malformed line, skip

                        // parts[0] = PageName, parts[1] = ShapeID, parts[2] = MasterName, parts[3] = Text
                        totalShapes++;

                        string masterName = parts[2];
                        if (!masterCounts.ContainsKey(masterName))
                            masterCounts[masterName] = 0;
                        masterCounts[masterName]++;
                    }
                }

                // Write summary report
                using (StreamWriter reportWriter = new StreamWriter(reportPath, false))
                {
                    reportWriter.WriteLine("Shape Metadata Summary Report");
                    reportWriter.WriteLine("------------------------------");
                    reportWriter.WriteLine($"Total shapes processed: {totalShapes}");
                    reportWriter.WriteLine();
                    reportWriter.WriteLine("Shapes per Master:");
                    foreach (var kvp in masterCounts)
                    {
                        string masterDisplay = string.IsNullOrEmpty(kvp.Key) ? "(No Master)" : kvp.Key;
                        reportWriter.WriteLine($"- {masterDisplay}: {kvp.Value}");
                    }
                }

                Console.WriteLine("Processing completed successfully.");
            }
        }
        catch (Exception ex)
        {
            // Log any errors that occur during processing
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }

    // Helper method to split a CSV line that uses double-quoted fields
    private static string[] SplitCsvLine(string line)
    {
        List<string> fields = new List<string>();
        bool inQuotes = false;
        int start = 0;

        for (int i = 0; i < line.Length; i++)
        {
            if (line[i] == '\"')
            {
                // Toggle inQuotes when encountering a quote
                inQuotes = !inQuotes;
            }
            else if (line[i] == ',' && !inQuotes)
            {
                // End of field
                string field = line.Substring(start, i - start).Trim();
                fields.Add(Unquote(field));
                start = i + 1;
            }
        }

        // Add the last field
        if (start < line.Length)
        {
            string field = line.Substring(start).Trim();
            fields.Add(Unquote(field));
        }

        return fields.ToArray();
    }

    // Remove surrounding quotes and unescape double quotes
    private static string Unquote(string field)
    {
        if (field.StartsWith("\"") && field.EndsWith("\"") && field.Length >= 2)
        {
            field = field.Substring(1, field.Length - 2);
        }
        return field.Replace("\"\"", "\"");
    }
}