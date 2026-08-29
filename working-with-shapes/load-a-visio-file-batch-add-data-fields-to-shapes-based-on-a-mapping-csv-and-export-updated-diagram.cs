using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

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
                Console.WriteLine($"Error: Visio file not found at '{inputVisioPath}'.");
                return;
            }

            if (!File.Exists(csvPath))
            {
                Console.WriteLine($"Error: CSV file not found at '{csvPath}'.");
                return;
            }

            // Load the Visio diagram
            Diagram diagram;
            try
            {
                diagram = new Diagram(inputVisioPath);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to load Visio file: {ex.Message}");
                return;
            }

            // Read CSV mapping into a dictionary
            // Expected CSV format: ShapeNameU,Data1,Data2,Data3
            var mapping = new Dictionary<string, string[]>(StringComparer.OrdinalIgnoreCase);
            try
            {
                using (var reader = new StreamReader(csvPath))
                {
                    string line;
                    while ((line = reader.ReadLine()) != null)
                    {
                        // Skip empty lines
                        if (string.IsNullOrWhiteSpace(line))
                            continue;

                        // Split by comma (basic CSV, no quoted commas handling)
                        string[] parts = line.Split(',');

                        if (parts.Length < 2)
                            continue; // Not enough data, ignore

                        string shapeName = parts[0].Trim();
                        // Ensure we have exactly three data fields; missing fields are set to empty string
                        string[] dataFields = new string[3];
                        for (int i = 0; i < 3; i++)
                        {
                            if (i + 1 < parts.Length)
                                dataFields[i] = parts[i + 1].Trim();
                            else
                                dataFields[i] = string.Empty;
                        }

                        mapping[shapeName] = dataFields;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to read CSV file: {ex.Message}");
                return;
            }

            // Apply data fields to matching shapes
            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    // Skip deleted shapes
                    if (shape.Del == BOOL.True)
                        continue;

                    // Use universal name for matching
                    string shapeNameU = shape.NameU ?? string.Empty;

                    if (mapping.TryGetValue(shapeNameU, out string[] fields))
                    {
                        // Assign Data1, Data2, Data3 directly (no .Value)
                        shape.Data1 = fields[0];
                        shape.Data2 = fields[1];
                        shape.Data3 = fields[2];
                        Console.WriteLine($"Updated shape '{shapeNameU}' (ID: {shape.ID}) with Data1='{fields[0]}', Data2='{fields[1]}', Data3='{fields[2]}'.");
                    }
                }
            }

            // Save the updated diagram
            try
            {
                diagram.Save(outputVisioPath, SaveFileFormat.Vsdx);
                Console.WriteLine($"Diagram saved successfully to '{outputVisioPath}'.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to save diagram: {ex.Message}");
            }
        }
    }