using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Aspose.Diagram;

class Program
    {
        static void Main(string[] args)
        {
            // Expect two arguments: input Visio file path and output CSV file path
            if (args.Length < 2)
            {
                Console.WriteLine("Usage: VisioTagFrequencyReport <inputVisioPath> <outputCsvPath>");
                return;
            }

            string inputPath = args[0];
            string outputCsvPath = args[1];

            // Load the Visio diagram
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Dictionary to hold tag (custom property) name frequencies
                Dictionary<string, int> tagFrequencies = new Dictionary<string, int>(StringComparer.OrdinalIgnoreCase);

                // Iterate through all pages and shapes
                foreach (Page page in diagram.Pages)
                {
                    foreach (Shape shape in page.Shapes)
                    {
                        // Iterate through custom properties (Props) of the shape
                        foreach (Prop prop in shape.Props)
                        {
                            string tagName = prop.Name ?? string.Empty;

                            if (tagFrequencies.ContainsKey(tagName))
                            {
                                tagFrequencies[tagName]++;
                            }
                            else
                            {
                                tagFrequencies[tagName] = 1;
                            }
                        }
                    }
                }

                // Build CSV content
                StringBuilder csvBuilder = new StringBuilder();
                csvBuilder.AppendLine("Tag,Count"); // Header

                foreach (KeyValuePair<string, int> entry in tagFrequencies)
                {
                    // Escape commas in tag names if necessary
                    string escapedTag = entry.Key.Contains(",") ? $"\"{entry.Key}\"" : entry.Key;
                    csvBuilder.AppendLine($"{escapedTag},{entry.Value}");
                }

                // Write CSV to file
                try
                {
                    File.WriteAllText(outputCsvPath, csvBuilder.ToString(), Encoding.UTF8);
                    Console.WriteLine($"Tag frequency report generated successfully at: {outputCsvPath}");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error writing CSV file: {ex.Message}");
                }
            }
        }
    }