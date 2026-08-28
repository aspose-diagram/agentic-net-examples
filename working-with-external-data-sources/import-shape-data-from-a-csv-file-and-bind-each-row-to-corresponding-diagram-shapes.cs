using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
    {
        static void Main(string[] args)
        {
            // Validate arguments: [0] = Visio file path, [1] = CSV file path, [2] = output Visio file path
            if (args.Length < 3)
            {
                Console.WriteLine("Usage: DiagramCsvBinding <inputVisioPath> <csvPath> <outputVisioPath>");
                return;
            }

            string visioPath = args[0];
            string csvPath = args[1];
            string outputPath = args[2];

            // Load the Visio diagram
            Diagram diagram = new Diagram(visioPath);

            // Read CSV file (simple comma‑separated, no quotes handling)
            List<string[]> csvRows = new List<string[]>();
            using (var reader = new StreamReader(csvPath))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    // Skip empty lines
                    if (string.IsNullOrWhiteSpace(line))
                        continue;

                    // Split by comma
                    string[] columns = line.Split(',');
                    csvRows.Add(columns);
                }
            }

            // Expect at least three columns: ShapeName, Data1, Data2, Data3 (optional extra columns ignored)
            foreach (var row in csvRows)
            {
                if (row.Length < 4)
                {
                    Console.WriteLine("Skipping row with insufficient columns.");
                    continue;
                }

                string targetShapeName = row[0].Trim();
                string data1 = row[1].Trim();
                string data2 = row[2].Trim();
                string data3 = row[3].Trim();

                // Find the shape by universal name (NameU) across all pages
                Shape targetShape = null;
                foreach (Page page in diagram.Pages)
                {
                    foreach (Shape shape in page.Shapes)
                    {
                        if (shape.NameU != null && shape.NameU.Equals(targetShapeName, StringComparison.OrdinalIgnoreCase))
                        {
                            targetShape = shape;
                            break;
                        }
                    }
                    if (targetShape != null)
                        break;
                }

                if (targetShape == null)
                {
                    Console.WriteLine($"Shape \"{targetShapeName}\" not found in the diagram.");
                    continue;
                }

                // Bind CSV values to the shape's Data fields
                targetShape.Data1 = data1;
                targetShape.Data2 = data2;
                targetShape.Data3 = data3;
            }

            // Save the updated diagram
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
        }
    }