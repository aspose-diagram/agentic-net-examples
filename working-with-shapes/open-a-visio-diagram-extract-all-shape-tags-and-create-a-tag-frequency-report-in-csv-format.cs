using System;
using System.Collections.Generic;
using System.IO;
using Aspose.Diagram;

class Program
    {
        static void Main(string[] args)
        {
            // Expect two arguments: input Visio file path and output CSV file path.
            if (args.Length != 2)
            {
                Console.WriteLine("Usage: VisioTagFrequencyReport <inputVisioPath> <outputCsvPath>");
                return;
            }

            string inputPath = args[0];
            string outputCsvPath = args[1];

            // Load the Visio diagram.
            Diagram diagram = new Diagram(inputPath);

            // Dictionary to hold tag (custom property) name frequencies.
            Dictionary<string, int> tagFrequencies = new Dictionary<string, int>(StringComparer.OrdinalIgnoreCase);

            // Iterate through all pages.
            foreach (Page page in diagram.Pages)
            {
                // Iterate through all shapes on the page.
                foreach (Shape shape in page.Shapes)
                {
                    // Iterate through all custom properties (Props) of the shape.
                    foreach (Prop prop in shape.Props)
                    {
                        string tagName = prop.Name ?? string.Empty;

                        if (tagFrequencies.ContainsKey(tagName))
                        {
                            tagFrequencies[tagName] += 1;
                        }
                        else
                        {
                            tagFrequencies[tagName] = 1;
                        }
                    }
                }
            }

            // Write the frequency report to a CSV file.
            try
            {
                using (StreamWriter writer = new StreamWriter(outputCsvPath, false))
                {
                    // Header row.
                    writer.WriteLine("TagName,Frequency");

                    // Data rows.
                    foreach (KeyValuePair<string, int> entry in tagFrequencies)
                    {
                        // Escape commas in tag names if necessary.
                        string escapedTagName = entry.Key.Contains(",") ? $"\"{entry.Key}\"" : entry.Key;
                        writer.WriteLine($"{escapedTagName},{entry.Value}");
                    }
                }

                Console.WriteLine($"Tag frequency report generated successfully at: {outputCsvPath}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error writing CSV file: {ex.Message}");
            }
        }
    }