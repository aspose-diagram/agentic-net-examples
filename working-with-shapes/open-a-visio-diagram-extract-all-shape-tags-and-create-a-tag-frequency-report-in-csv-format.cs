using System;
using System.Collections.Generic;
using System.IO;
using Aspose.Diagram;

class Program
    {
        static void Main()
        {
            try
            {

                // Input Visio file path
                string inputPath = "input.vsdx";
                // Output CSV file path
                string outputCsvPath = "TagFrequencyReport.csv";

                // Dictionary to hold tag (custom property name) frequencies
                Dictionary<string, int> tagCounts = new Dictionary<string, int>(StringComparer.OrdinalIgnoreCase);

                try
                {
                    // Load the Visio diagram
                    using (Diagram diagram = new Diagram(inputPath))
                    {
                        // Iterate through each page in the diagram
                        foreach (Page page in diagram.Pages)
                        {
                            // Iterate through each shape on the page
                            foreach (Shape shape in page.Shapes)
                            {
                                // Iterate through each custom property (Prop) of the shape
                                foreach (Prop prop in shape.Props)
                                {
                                    string tagName = prop.Name ?? string.Empty;

                                    if (tagCounts.ContainsKey(tagName))
                                    {
                                        tagCounts[tagName] = tagCounts[tagName] + 1;
                                    }
                                    else
                                    {
                                        tagCounts.Add(tagName, 1);
                                    }
                                }
                            }
                        }
                    }

                    // Write the frequency report to a CSV file
                    using (StreamWriter writer = new StreamWriter(outputCsvPath))
                    {
                        // CSV header
                        writer.WriteLine("Tag,Count");

                        // Write each tag and its count
                        foreach (KeyValuePair<string, int> entry in tagCounts)
                        {
                            // Escape double quotes in tag names if necessary
                            string escapedTag = entry.Key.Replace("\"", "\"\"");
                            writer.WriteLine($"\"{escapedTag}\",{entry.Value}");
                        }
                    }

                    Console.WriteLine($"Tag frequency report generated successfully at: {outputCsvPath}");
                }
                catch (Exception ex)
                {
                    // Report any errors that occur during processing
                    Console.WriteLine("An error occurred while generating the tag frequency report:");
                    Console.WriteLine(ex.Message);
                    throw;
                }

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }