using System;
using System.Collections.Generic;
using System.IO;
using Aspose.Diagram;

class Program
    {
        static void Main(string[] args)
        {
            try
            {

                // Input Visio file path (modify as needed)
                string inputPath = "input.vsdx";

                // Output CSV file path
                string outputCsv = "tag_report.csv";

                // Load the Visio diagram
                Diagram diagram = new Diagram(inputPath);

                // Dictionary to hold tag (custom property) frequencies
                Dictionary<string, int> tagCounts = new Dictionary<string, int>(StringComparer.OrdinalIgnoreCase);

                // Iterate through all pages and shapes
                foreach (Page page in diagram.Pages)
                {
                    foreach (Shape shape in page.Shapes)
                    {
                        // Ensure the shape has custom properties (Props)
                        if (shape.Props != null)
                        {
                            foreach (Prop prop in shape.Props)
                            {
                                string tagName = prop.Name ?? string.Empty;

                                if (tagCounts.ContainsKey(tagName))
                                    tagCounts[tagName]++;
                                else
                                    tagCounts[tagName] = 1;
                            }
                        }
                    }
                }

                // Write the frequency report to a CSV file
                using (StreamWriter writer = new StreamWriter(outputCsv))
                {
                    // CSV header
                    writer.WriteLine("Tag,Count");

                    // Write each tag and its count
                    foreach (KeyValuePair<string, int> kvp in tagCounts)
                    {
                        // Escape commas in tag names if necessary
                        string escapedTag = kvp.Key.Contains(",") ? $"\"{kvp.Key}\"" : kvp.Key;
                        writer.WriteLine($"{escapedTag},{kvp.Value}");
                    }
                }

                Console.WriteLine($"Tag frequency report generated: {outputCsv}");

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }