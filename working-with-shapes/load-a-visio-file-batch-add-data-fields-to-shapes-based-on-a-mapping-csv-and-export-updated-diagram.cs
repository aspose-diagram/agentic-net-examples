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

                // Input Visio file path
                string inputVisioPath = "input.vsdx";
                // CSV mapping file path (format: ShapeName,Data1,Data2,Data3)
                string csvMappingPath = "mapping.csv";
                // Output Visio file path
                string outputVisioPath = "output.vsdx";

                // Load the Visio diagram
                Diagram diagram = new Diagram(inputVisioPath);

                // Read CSV and build a lookup dictionary keyed by shape universal name
                var shapeDataMap = new Dictionary<string, (string Data1, string Data2, string Data3)>(StringComparer.OrdinalIgnoreCase);
                if (File.Exists(csvMappingPath))
                {
                    string[] csvLines = File.ReadAllLines(csvMappingPath);
                    foreach (string line in csvLines)
                    {
                        // Skip empty lines
                        if (string.IsNullOrWhiteSpace(line))
                            continue;

                        // Split by comma (basic CSV handling; assumes no commas inside fields)
                        string[] parts = line.Split(',');
                        if (parts.Length < 4)
                            continue; // Not enough columns, ignore

                        string shapeName = parts[0].Trim();
                        string data1 = parts[1].Trim();
                        string data2 = parts[2].Trim();
                        string data3 = parts[3].Trim();

                        shapeDataMap[shapeName] = (data1, data2, data3);
                    }
                }
                else
                {
                    Console.WriteLine($"CSV mapping file not found: {csvMappingPath}");
                    return;
                }

                // Iterate through all pages and shapes, applying data fields where a mapping exists
                foreach (Page page in diagram.Pages)
                {
                    foreach (Shape shape in page.Shapes)
                    {
                        // Skip deleted shapes
                        if (shape.Del == BOOL.True)
                            continue;

                        // Use the universal name for matching
                        if (shapeDataMap.TryGetValue(shape.NameU, out var values))
                        {
                            // Assign Data1, Data2, Data3 directly (these are string properties)
                            shape.Data1 = values.Data1;
                            shape.Data2 = values.Data2;
                            shape.Data3 = values.Data3;
                        }
                    }
                }

                // Save the updated diagram in VSDX format
                diagram.Save(outputVisioPath, SaveFileFormat.Csv);

                Console.WriteLine($"Diagram saved to {outputVisioPath}");

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }