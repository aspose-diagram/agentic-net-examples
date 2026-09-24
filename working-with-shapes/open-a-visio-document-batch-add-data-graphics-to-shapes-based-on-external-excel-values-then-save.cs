using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
    {
        static void Main(string[] args)
        {
            try
            {

                // Paths to the Visio file and the external CSV (exported from Excel)
                string visioPath = "input.vsdx";
                string csvPath = "data.csv";
                string outputPath = "output.vsdx";

                // Load the Visio diagram
                Diagram diagram = new Diagram(visioPath);

                // Read CSV data into a dictionary: key = shape NameU, value = data to assign
                var dataMap = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
                if (File.Exists(csvPath))
                {
                    string[] lines = File.ReadAllLines(csvPath);
                    foreach (string line in lines)
                    {
                        // Skip empty lines
                        if (string.IsNullOrWhiteSpace(line))
                            continue;

                        // Expected format: ShapeName,Value
                        string[] parts = line.Split(',');
                        if (parts.Length >= 2)
                        {
                            string shapeName = parts[0].Trim();
                            string value = parts[1].Trim();
                            if (!dataMap.ContainsKey(shapeName))
                            {
                                dataMap.Add(shapeName, value);
                            }
                        }
                    }
                }
                else
                {
                    Console.WriteLine($"CSV file not found: {csvPath}");
                    return;
                }

                // Iterate over shapes on the first page and assign Data1 based on the CSV mapping
                Page page = diagram.Pages[0];
                foreach (Shape shape in page.Shapes)
                {
                    // Skip deleted shapes
                    if (shape.Del == BOOL.True)
                        continue;

                    // Use the universal name of the shape to look up data
                    string shapeNameU = shape.NameU;
                    if (dataMap.TryGetValue(shapeNameU, out string dataValue))
                    {
                        // Assign the external value to the shape's Data1 field
                        shape.Data1 = dataValue;
                    }
                }

                // Save the modified diagram
                diagram.Save(outputPath, SaveFileFormat.Csv);
                Console.WriteLine($"Diagram saved to {outputPath}");

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }