using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Diagram;

class Program
    {
        static void Main(string[] args)
        {
            try
            {

                // Input Visio file path
                string visioPath = "input.vsdx";
                // Output CSV file path
                string csvPath = "shape_metadata.csv";

                // Load the Visio diagram
                using (Diagram diagram = new Diagram(visioPath))
                {
                    // Prepare to write CSV
                    using (StreamWriter writer = new StreamWriter(csvPath))
                    {
                        // Write CSV header
                        writer.WriteLine("PageName,ShapeID,ShapeName,ShapeNameU,MasterName,PlainText");

                        // Iterate through pages explicitly typing the iterator
                        foreach (Page page in diagram.Pages)
                        {
                            // Iterate through shapes on the page
                            foreach (Shape shape in page.Shapes)
                            {
                                // Skip deleted shapes
                                if (shape.Del == BOOL.True)
                                    continue;

                                // Gather metadata
                                string pageName = page.Name ?? "";
                                string shapeId = shape.ID.ToString();
                                string shapeName = shape.Name ?? "";
                                string shapeNameU = shape.NameU ?? "";
                                string masterName = shape.Master != null ? shape.Master.Name ?? "" : "";
                                string plainText = shape.Text != null ? shape.Text.Value.Text ?? "" : "";

                                // Replace commas in text fields to avoid CSV column issues
                                shapeName = shapeName.Replace(",", " ");
                                shapeNameU = shapeNameU.Replace(",", " ");
                                masterName = masterName.Replace(",", " ");
                                plainText = plainText.Replace(",", " ");

                                // Write CSV line
                                writer.WriteLine($"{pageName},{shapeId},{shapeName},{shapeNameU},{masterName},{plainText}");
                            }
                        }
                    }
                }

                // Generate summary report from the CSV
                // Dictionary to count shapes per master name
                Dictionary<string, int> masterCounts = new Dictionary<string, int>(StringComparer.OrdinalIgnoreCase);

                // Read all lines from CSV
                string[] csvLines = File.ReadAllLines(csvPath);
                // Skip header (first line)
                for (int i = 1; i < csvLines.Length; i++)
                {
                    string line = csvLines[i];
                    // Simple split by comma (fields do not contain commas after replacement)
                    string[] parts = line.Split(',');

                    if (parts.Length < 6)
                        continue; // malformed line

                    string masterName = parts[4];

                    if (masterCounts.ContainsKey(masterName))
                        masterCounts[masterName]++;
                    else
                        masterCounts[masterName] = 1;
                }

                // Output summary report to console
                Console.WriteLine("=== Shape Summary Report ===");
                Console.WriteLine("Total distinct masters: " + masterCounts.Count);
                Console.WriteLine();

                foreach (KeyValuePair<string, int> kvp in masterCounts)
                {
                    string master = string.IsNullOrEmpty(kvp.Key) ? "(No Master)" : kvp.Key;
                    Console.WriteLine($"Master: {master} - Shape Count: {kvp.Value}");
                }

                Console.WriteLine();
                Console.WriteLine("Report generation completed.");

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }