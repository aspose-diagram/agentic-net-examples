using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Validate input arguments.
        if (args.Length < 1)
        {
            Console.Error.WriteLine("Usage: Program <inputVisioFile> [outputCsvFile]");
            return;
        }

        string inputPath = args[0];
        // Guard: ensure the input Visio file exists.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Error: Input file \"{inputPath}\" does not exist.");
            return;
        }

        // Determine output CSV path.
        string outputPath;
        if (args.Length >= 2)
        {
            outputPath = args[1];
        }
        else
        {
            // Default: same folder, same name with suffix "_TagReport.csv".
            string directory = Path.GetDirectoryName(inputPath) ?? "";
            string filenameWithoutExt = Path.GetFileNameWithoutExtension(inputPath);
            outputPath = Path.Combine(directory, $"{filenameWithoutExt}_TagReport.csv");
        }

        // Dictionary to hold tag name frequencies.
        Dictionary<string, int> tagFrequencies = new Dictionary<string, int>(StringComparer.OrdinalIgnoreCase);

        try
        {
            // Load the Visio diagram inside a using block as required.
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Iterate through all pages in the diagram.
                foreach (Page page in diagram.Pages)
                {
                    // Iterate through all shapes on the current page.
                    foreach (Shape shape in page.Shapes)
                    {
                        // Skip logically deleted shapes.
                        if (shape.Del == BOOL.True)
                            continue;

                        // Ensure the shape has a Props collection.
                        if (shape.Props == null)
                            continue;

                        // Iterate through all custom properties (tags) of the shape.
                        foreach (Prop prop in shape.Props)
                        {
                            // The property name serves as the tag identifier.
                            string tagName = prop.Name ?? string.Empty;

                            if (string.IsNullOrEmpty(tagName))
                                continue;

                            // Increment frequency count for this tag.
                            if (tagFrequencies.ContainsKey(tagName))
                                tagFrequencies[tagName]++;
                            else
                                tagFrequencies[tagName] = 1;
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error processing Visio file: {ex.Message}");
            return;
        }

        try
        {
            // Write the tag frequency report to a CSV file.
            using (StreamWriter writer = new StreamWriter(outputPath))
            {
                // CSV header.
                writer.WriteLine("TagName,Frequency");

                // Write each tag and its count.
                foreach (KeyValuePair<string, int> entry in tagFrequencies)
                {
                    // Simple CSV escaping for commas and quotes.
                    string escapedTag = entry.Key.Replace("\"", "\"\"");
                    if (escapedTag.Contains(",") || escapedTag.Contains("\""))
                        escapedTag = $"\"{escapedTag}\"";

                    writer.WriteLine($"{escapedTag},{entry.Value}");
                }
            }

            Console.WriteLine($"Tag frequency report generated successfully at \"{outputPath}\".");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error writing CSV file: {ex.Message}");
        }
    }
}