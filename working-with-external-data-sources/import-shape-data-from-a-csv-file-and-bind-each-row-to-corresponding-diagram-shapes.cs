using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Diagram;

class Program
    {
        static void Main(string[] args)
        {
            // Expect three arguments: diagram file path, CSV file path, output diagram path
            if (args.Length < 3)
            {
                Console.WriteLine("Usage: DiagramCsvBinding <diagramPath> <csvPath> <outputPath>");
                return;
            }

            string diagramPath = args[0];
            string csvPath = args[1];
            string outputPath = args[2];

            // Load the existing Visio diagram
            Diagram diagram = new Diagram(diagramPath);

            // Assume the diagram has at least one page; use the first page
            Page page = diagram.Pages[0];

            // Read all lines from the CSV file
            string[] csvLines = File.ReadAllLines(csvPath);
            if (csvLines.Length == 0)
            {
                Console.WriteLine("CSV file is empty.");
                return;
            }

            // Optional: skip header line if it contains non-numeric first column
            int startIndex = 0;
            string[] headerParts = csvLines[0].Split(',');
            if (headerParts.Length > 0 && !long.TryParse(headerParts[0], out _))
            {
                startIndex = 1; // skip header
            }

            for (int i = startIndex; i < csvLines.Length; i++)
            {
                string line = csvLines[i];
                if (string.IsNullOrWhiteSpace(line))
                    continue;

                // Simple CSV split (does not handle quoted commas)
                string[] parts = line.Split(',');

                if (parts.Length < 4)
                {
                    Console.WriteLine($"Skipping line {i + 1}: insufficient columns.");
                    continue;
                }

                // Parse shape ID (first column)
                if (!long.TryParse(parts[0].Trim(), out long shapeId))
                {
                    Console.WriteLine($"Skipping line {i + 1}: invalid shape ID.");
                    continue;
                }

                // Retrieve the shape by ID
                Shape shape = page.Shapes.GetShape(shapeId);
                if (shape == null)
                {
                    Console.WriteLine($"Shape with ID {shapeId} not found on page.");
                    continue;
                }

                // Skip deleted shapes
                if (shape.Del == BOOL.True)
                {
                    Console.WriteLine($"Shape with ID {shapeId} is marked as deleted; skipping.");
                    continue;
                }

                // Bind CSV columns to shape data fields
                shape.Data1 = parts[1].Trim();
                shape.Data2 = parts[2].Trim();
                shape.Data3 = parts[3].Trim();
            }

            // Save the modified diagram
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Diagram saved to {outputPath}");
        }
    }