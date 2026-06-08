using System;
using System.IO;
using Aspose.Diagram;

class Program
    {
        static void Main(string[] args)
        {
            try
            {

                // Paths – adjust as needed
                string diagramPath = "input.vsdx";
                string csvPath = "data.csv";
                string outputPath = "output.vsdx";

                // Load the Visio diagram
                Diagram diagram = new Diagram(diagramPath);

                // Verify CSV file exists
                if (!File.Exists(csvPath))
                {
                    Console.WriteLine($"CSV file not found: {csvPath}");
                    return;
                }

                // Read all lines from the CSV (first line assumed header)
                string[] lines = File.ReadAllLines(csvPath);
                if (lines.Length < 2)
                {
                    Console.WriteLine("CSV file does not contain data rows.");
                    return;
                }

                // Process each data row
                for (int i = 1; i < lines.Length; i++)
                {
                    string line = lines[i];
                    if (string.IsNullOrWhiteSpace(line))
                        continue;

                    // Simple CSV split – assumes no commas inside quoted fields
                    string[] parts = line.Split(',');
                    if (parts.Length < 4)
                    {
                        Console.WriteLine($"Invalid CSV format at line {i + 1}");
                        continue;
                    }

                    string shapeIdentifier = parts[0].Trim(); // Expected to match shape.NameU
                    string data1 = parts[1].Trim();
                    string data2 = parts[2].Trim();
                    string data3 = parts[3].Trim();

                    bool shapeFound = false;

                    // Search for the shape across all pages
                    foreach (Aspose.Diagram.Page page in diagram.Pages)
                    {
                        foreach (Aspose.Diagram.Shape shape in page.Shapes)
                        {
                            if (shape.NameU != null && shape.NameU.Equals(shapeIdentifier, StringComparison.OrdinalIgnoreCase))
                            {
                                // Bind CSV values to shape data fields
                                shape.Data1 = data1;
                                shape.Data2 = data2;
                                shape.Data3 = data3;
                                shapeFound = true;
                                break;
                            }
                        }
                        if (shapeFound) break;
                    }

                    if (!shapeFound)
                    {
                        Console.WriteLine($"Shape not found for identifier '{shapeIdentifier}'");
                    }
                }

                // Save the updated diagram
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
                Console.WriteLine($"Diagram saved to {outputPath}");

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }