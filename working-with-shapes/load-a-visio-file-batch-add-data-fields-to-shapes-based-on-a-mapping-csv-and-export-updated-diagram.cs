using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Diagram;

class Program
    {
        static void Main(string[] args)
        {
            // Expect three arguments: input Visio file, CSV mapping file, output Visio file
            if (args.Length < 3)
            {
                Console.WriteLine("Usage: VisioBatchUpdate <inputVisioPath> <mappingCsvPath> <outputVisioPath>");
                return;
            }

            string inputVisioPath = args[0];
            string csvPath = args[1];
            string outputVisioPath = args[2];

            // Load mapping definitions from CSV
            List<Mapping> mappings = LoadMappings(csvPath);

            // Load the Visio diagram
            using (Diagram diagram = new Diagram(inputVisioPath))
            {
                // Apply each mapping to the corresponding shape
                foreach (Mapping map in mappings)
                {
                    bool updated = UpdateShapeData(diagram, map);
                    if (!updated)
                    {
                        Console.WriteLine($"Warning: Shape '{map.ShapeName}' not found.");
                    }
                }

                // Save the updated diagram
                diagram.Save(outputVisioPath, SaveFileFormat.Vsdx);
                Console.WriteLine($"Diagram saved to '{outputVisioPath}'.");
            }
        }

        // Reads the CSV file and returns a list of mapping records
        static List<Mapping> LoadMappings(string csvFile)
        {
            var list = new List<Mapping>();

            foreach (string line in File.ReadLines(csvFile))
            {
                if (string.IsNullOrWhiteSpace(line))
                    continue;

                // Skip header line if present
                if (line.StartsWith("ShapeName", StringComparison.OrdinalIgnoreCase))
                    continue;

                string[] parts = line.Split(',');
                if (parts.Length < 3)
                {
                    Console.WriteLine($"Invalid CSV line (expected 3 columns): {line}");
                    continue;
                }

                var map = new Mapping
                {
                    ShapeName = parts[0].Trim(),
                    DataField = parts[1].Trim(),
                    Value = parts[2].Trim()
                };
                list.Add(map);
            }

            return list;
        }

        // Finds the shape by universal name and updates the requested data field
        static bool UpdateShapeData(Diagram diagram, Mapping map)
        {
            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    if (string.Equals(shape.NameU, map.ShapeName, StringComparison.OrdinalIgnoreCase))
                    {
                        switch (map.DataField)
                        {
                            case "Data1":
                                shape.Data1 = map.Value;
                                break;
                            case "Data2":
                                shape.Data2 = map.Value;
                                break;
                            case "Data3":
                                shape.Data3 = map.Value;
                                break;
                            default:
                                Console.WriteLine($"Unsupported data field '{map.DataField}' for shape '{map.ShapeName}'.");
                                break;
                        }
                        return true;
                    }
                }
            }
            return false;
        }

        // DTO representing a single CSV row
        class Mapping
        {
            public string ShapeName { get; set; } = string.Empty;
            public string DataField { get; set; } = string.Empty;
            public string Value { get; set; } = string.Empty;
        }
    }