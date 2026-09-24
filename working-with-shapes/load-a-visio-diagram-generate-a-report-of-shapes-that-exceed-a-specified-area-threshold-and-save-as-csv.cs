using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Diagram;

class Program
    {
        static void Main(string[] args)
        {
            // Expected arguments: inputVisioPath outputCsvPath areaThreshold
            if (args.Length < 3)
            {
                Console.WriteLine("Usage: VisioShapeAreaReport <inputVisioPath> <outputCsvPath> <areaThreshold>");
                return;
            }

            string inputPath = args[0];
            string outputPath = args[1];
            if (!double.TryParse(args[2], out double areaThreshold))
            {
                Console.WriteLine("Invalid area threshold. Provide a numeric value.");
                return;
            }

            // Load the Visio diagram
            Diagram diagram;
            try
            {
                diagram = new Diagram(inputPath);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to load diagram: {ex.Message}");
                return;
            }

            // Prepare list to hold report rows
            List<string> csvLines = new List<string>();
            // Header
            csvLines.Add("PageName,ShapeID,ShapeName,MasterName,Width,Height,Area");

            // Iterate through pages and shapes
            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    // Skip deleted shapes
                    if (shape.Del == BOOL.True)
                        continue;

                    // Retrieve width and height (in inches)
                    double width = shape.XForm.Width.Value;
                    double height = shape.XForm.Height.Value;
                    double area = width * height;

                    if (area > areaThreshold)
                    {
                        string pageName = page.NameU ?? string.Empty;
                        string shapeId = shape.ID.ToString();
                        string shapeName = shape.NameU ?? string.Empty;
                        string masterName = shape.Master != null ? shape.Master.Name ?? string.Empty : string.Empty;

                        // Build CSV line (comma escaped if needed)
                        string line = $"{EscapeCsv(pageName)},{shapeId},{EscapeCsv(shapeName)},{EscapeCsv(masterName)},{width:F4},{height:F4},{area:F4}";
                        csvLines.Add(line);
                    }
                }
            }

            // Write CSV file
            try
            {
                File.WriteAllLines(outputPath, csvLines);
                Console.WriteLine($"Report generated successfully at: {outputPath}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to write CSV: {ex.Message}");
            }
        }

        // Helper to escape commas and quotes in CSV fields
        private static string EscapeCsv(string field)
        {
            if (field.Contains(",") || field.Contains("\"") || field.Contains("\n"))
            {
                string escaped = field.Replace("\"", "\"\"");
                return $"\"{escaped}\"";
            }
            return field;
        }
    }