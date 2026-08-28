using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
    {
        static void Main()
        {
            try
            {

                // Paths to the input diagram and external CSV data file
                string diagramPath = "input.vsdx";
                string csvPath = "data.csv";
                string outputPath = "output.vsdx";

                // Load the Visio diagram
                Diagram diagram = new Diagram(diagramPath);

                // Read external data from CSV into a dictionary (key: shape NameU, value: raw data)
                Dictionary<string, string> dataMap = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
                using (StreamReader reader = new StreamReader(csvPath))
                {
                    while (!reader.EndOfStream)
                    {
                        string line = reader.ReadLine();
                        if (string.IsNullOrWhiteSpace(line))
                            continue;

                        string[] parts = line.Split(',');
                        if (parts.Length >= 2)
                        {
                            string key = parts[0].Trim();
                            string val = parts[1].Trim();
                            dataMap[key] = val;
                        }
                    }
                }

                // Iterate through all pages and shapes, updating text where a matching entry exists
                foreach (Aspose.Diagram.Page page in diagram.Pages)
                {
                    foreach (Aspose.Diagram.Shape shape in page.Shapes)
                    {
                        string shapeName = shape.NameU;
                        if (dataMap.ContainsKey(shapeName))
                        {
                            string rawValue = dataMap[shapeName];
                            string transformedValue;

                            // Example transformation: numeric values are multiplied by 1.5, others are upper‑cased
                            if (double.TryParse(rawValue, out double numeric))
                            {
                                double newValue = numeric * 1.5;
                                transformedValue = newValue.ToString("F2");
                            }
                            else
                            {
                                transformedValue = rawValue.ToUpperInvariant();
                            }

                            // Clear existing text and set the transformed value
                            shape.Text.Value.Clear();
                            shape.Text.Value.Add(new Txt(transformedValue));
                        }
                    }
                }

                // Save the modified diagram
                diagram.Save(outputPath, SaveFileFormat.Csv);

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }