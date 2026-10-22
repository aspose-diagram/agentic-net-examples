using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Validate command‑line arguments.
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <inputVisioPath> <outputCsvPath>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

        // Guard: ensure the input Visio file exists.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Guard: ensure the output directory exists (create if necessary).
        string outputDir = Path.GetDirectoryName(outputPath);
        if (!string.IsNullOrEmpty(outputDir) && !Directory.Exists(outputDir))
        {
            try
            {
                Directory.CreateDirectory(outputDir);
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Failed to create output directory: {ex.Message}");
                return;
            }
        }

        try
        {
            // Load the Visio diagram inside a using block as required.
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Dictionary to hold tag (custom property) frequencies.
                var tagCounts = new Dictionary<string, int>(StringComparer.OrdinalIgnoreCase);

                // Iterate through all pages.
                foreach (Page page in diagram.Pages)
                {
                    // Iterate through all shapes on the page.
                    foreach (Shape shape in page.Shapes)
                    {
                        // Skip logically deleted shapes.
                        if (shape.Del == BOOL.True)
                            continue;

                        // Iterate through custom properties (tags) of the shape.
                        foreach (Prop prop in shape.Props)
                        {
                            string tagName = prop.Name;
                            if (string.IsNullOrEmpty(tagName))
                                continue;

                            if (tagCounts.ContainsKey(tagName))
                                tagCounts[tagName]++;
                            else
                                tagCounts[tagName] = 1;
                        }
                    }
                }

                // Write the tag frequency report to CSV.
                using (StreamWriter writer = new StreamWriter(outputPath))
                {
                    // Header row.
                    writer.WriteLine("Tag,Count");

                    // Data rows.
                    foreach (var kvp in tagCounts)
                    {
                        writer.WriteLine($"{EscapeCsv(kvp.Key)},{kvp.Value}");
                    }
                }

                Console.WriteLine($"Tag frequency report generated: {outputPath}");
            }
        }
        catch (Exception ex)
        {
            // Report any errors that occur during processing.
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }

    // Helper method to escape CSV fields containing commas or quotes.
    private static string EscapeCsv(string field)
    {
        if (field.Contains("\""))
            field = field.Replace("\"", "\"\"");

        if (field.Contains(",") || field.Contains("\"") || field.Contains("\n") || field.Contains("\r"))
            field = $"\"{field}\"";

        return field;
    }
}