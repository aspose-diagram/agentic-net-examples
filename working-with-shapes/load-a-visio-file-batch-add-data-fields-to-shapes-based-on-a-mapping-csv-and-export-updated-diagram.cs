using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Diagram;

class Program
    {
        static void Main(string[] args)
        {
            // Expect three arguments: input Visio file, mapping CSV file, output Visio file
            if (args.Length != 3)
            {
                Console.WriteLine("Usage: VisioBatchDataUpdater <inputVisioPath> <mappingCsvPath> <outputVisioPath>");
                return;
            }

            string inputVisioPath = args[0];
            string csvPath = args[1];
            string outputVisioPath = args[2];

            // Validate input files
            if (!File.Exists(inputVisioPath))
            {
                Console.WriteLine($"Input Visio file not found: {inputVisioPath}");
                return;
            }

            if (!File.Exists(csvPath))
            {
                Console.WriteLine($"Mapping CSV file not found: {csvPath}");
                return;
            }

            // Load mapping CSV into a dictionary: ShapeNameU -> (Data1, Data2, Data3)
            Dictionary<string, Tuple<string, string, string>> shapeDataMap = new Dictionary<string, Tuple<string, string, string>>(StringComparer.OrdinalIgnoreCase);

            try
            {
                string[] csvLines = File.ReadAllLines(csvPath);
                foreach (string line in csvLines)
                {
                    // Skip empty lines
                    if (string.IsNullOrWhiteSpace(line))
                        continue;

                    // Assume CSV format: ShapeName,Data1,Data2,Data3
                    string[] parts = line.Split(',');

                    if (parts.Length < 1)
                        continue; // No shape name, ignore

                    string shapeName = parts[0].Trim();

                    string data1 = parts.Length > 1 ? parts[1].Trim() : string.Empty;
                    string data2 = parts.Length > 2 ? parts[2].Trim() : string.Empty;
                    string data3 = parts.Length > 3 ? parts[3].Trim() : string.Empty;

                    shapeDataMap[shapeName] = new Tuple<string, string, string>(data1, data2, data3);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error reading CSV file: {ex.Message}");
                return;
            }

            // Load the Visio diagram
            try
            {
                using (Diagram diagram = new Diagram(inputVisioPath))
                {
                    // Iterate through all pages and shapes
                    foreach (Page page in diagram.Pages)
                    {
                        foreach (Shape shape in page.Shapes)
                        {
                            // Match shape by its universal name (NameU)
                            if (shape.NameU != null && shapeDataMap.TryGetValue(shape.NameU, out Tuple<string, string, string> dataTuple))
                            {
                                // Assign Data1, Data2, Data3 directly (no .Value)
                                shape.Data1 = dataTuple.Item1;
                                shape.Data2 = dataTuple.Item2;
                                shape.Data3 = dataTuple.Item3;
                            }
                        }
                    }

                    // Save the updated diagram
                    diagram.Save(outputVisioPath, SaveFileFormat.Vsdx);
                    Console.WriteLine($"Diagram saved successfully to: {outputVisioPath}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error processing Visio diagram: {ex.Message}");
            }
        }
    }