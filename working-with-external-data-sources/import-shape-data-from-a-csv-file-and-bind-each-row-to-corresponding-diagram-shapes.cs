using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Expected arguments: diagramPath csvPath outputPath
        if (args.Length < 3)
        {
            Console.Error.WriteLine("Usage: Program <diagramPath> <csvPath> <outputPath>");
            return;
        }

        string diagramPath = args[0];
        string csvPath = args[1];
        string outputPath = args[2];

        // Guard against missing files
        if (!File.Exists(diagramPath))
        {
            Console.Error.WriteLine($"Diagram file not found: {diagramPath}");
            return;
        }

        if (!File.Exists(csvPath))
        {
            Console.Error.WriteLine($"CSV file not found: {csvPath}");
            return;
        }

        try
        {
            // Load the Visio diagram
            Diagram diagram = new Diagram(diagramPath);

            // Read all lines from the CSV file
            string[] csvLines = File.ReadAllLines(csvPath);

            // Expect header: ShapeId,Data1,Data2,Data3 (case‑insensitive)
            // Skip empty lines and the header line
            for (int i = 1; i < csvLines.Length; i++)
            {
                string line = csvLines[i].Trim();
                if (string.IsNullOrEmpty(line))
                    continue;

                // Split by comma – simple CSV parsing (no quoted commas handling)
                string[] parts = line.Split(',');

                if (parts.Length < 4)
                {
                    Console.Error.WriteLine($"Invalid CSV format at line {i + 1}: {line}");
                    continue;
                }

                // Parse shape ID
                if (!long.TryParse(parts[0].Trim(), out long shapeId))
                {
                    Console.Error.WriteLine($"Invalid ShapeId at line {i + 1}: {parts[0]}");
                    continue;
                }

                string data1 = parts[1].Trim();
                string data2 = parts[2].Trim();
                string data3 = parts[3].Trim();

                // Locate the shape across all pages
                Shape targetShape = null;
                foreach (Page page in diagram.Pages)
                {
                    try
                    {
                        // GetShape expects an int identifier; cast safely
                        targetShape = page.Shapes.GetShape((int)shapeId);
                        if (targetShape != null)
                            break;
                    }
                    catch
                    {
                        // Shape not found on this page – continue searching
                    }
                }

                if (targetShape == null)
                {
                    Console.Error.WriteLine($"Shape with ID {shapeId} not found in diagram.");
                    continue;
                }

                // Bind CSV data to shape's custom data fields
                targetShape.Data1 = data1;
                targetShape.Data2 = data2;
                targetShape.Data3 = data3;
            }

            // Save the modified diagram
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Diagram saved successfully to {outputPath}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}