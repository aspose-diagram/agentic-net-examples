using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Diagram;

namespace VisioShapeAreaReport
{
    // Simple data holder for shapes that exceed the area threshold
    public class ShapeRecord
    {
        public string PageName { get; set; }
        public long ShapeId { get; set; }
        public string ShapeName { get; set; }
        public double Area { get; set; }
    }

    public class Program
    {
        // Entry point
        public static void Main(string[] args)
        {
            // Expected arguments: inputVisioPath areaThreshold outputCsvPath
            if (args == null || args.Length < 3)
            {
                Console.WriteLine("Usage: VisioShapeAreaReport <inputVisioPath> <areaThreshold> <outputCsvPath>");
                return;
            }

            string inputPath = args[0];
            string thresholdArg = args[1];
            string outputCsvPath = args[2];

            if (!File.Exists(inputPath))
            {
                Console.WriteLine($"Error: Input file does not exist: {inputPath}");
                return;
            }

            if (!double.TryParse(thresholdArg, out double areaThreshold))
            {
                Console.WriteLine($"Error: Unable to parse area threshold '{thresholdArg}'.");
                return;
            }

            try
            {
                // Load the Visio diagram
                Diagram diagram = new Diagram(inputPath);

                // Collect records of shapes whose area exceeds the threshold
                List<ShapeRecord> exceedingShapes = new List<ShapeRecord>();

                // Iterate through each page
                foreach (Page page in diagram.Pages)
                {
                    // Iterate through each shape on the page
                    foreach (Shape shape in page.Shapes)
                    {
                        // Skip shapes that are marked as deleted
                        if (shape.Del == BOOL.True)
                            continue;

                        // Retrieve width and height (in inches)
                        double width = shape.XForm.Width.Value;
                        double height = shape.XForm.Height.Value;

                        // Compute area
                        double area = width * height;

                        if (area > areaThreshold)
                        {
                            ShapeRecord record = new ShapeRecord
                            {
                                PageName = page.Name,
                                ShapeId = shape.ID,
                                ShapeName = shape.Name,
                                Area = area
                            };
                            exceedingShapes.Add(record);
                        }
                    }
                }

                // Write the CSV report
                using (StreamWriter writer = new StreamWriter(outputCsvPath, false))
                {
                    // Header
                    writer.WriteLine("PageName,ShapeID,ShapeName,Area");

                    // Data rows
                    foreach (ShapeRecord rec in exceedingShapes)
                    {
                        // Ensure proper CSV escaping for text fields
                        string safePageName = EscapeCsv(rec.PageName);
                        string safeShapeName = EscapeCsv(rec.ShapeName);
                        writer.WriteLine($"{safePageName},{rec.ShapeId},{safeShapeName},{rec.Area}");
                    }
                }

                Console.WriteLine($"Report generated successfully. {exceedingShapes.Count} shape(s) exceed the threshold of {areaThreshold} square inches.");
                Console.WriteLine($"CSV saved to: {outputCsvPath}");

                // Optionally, also save the original diagram as CSV using Aspose (if needed)
                // diagram.Save("original_diagram.csv", SaveFileFormat.Csv);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
                // Rethrow if you want the caller to see the stack trace
                // throw;
            }
        }

        // Helper method to escape CSV fields containing commas or quotes
        private static string EscapeCsv(string field)
        {
            if (field == null)
                return string.Empty;

            if (field.Contains("\""))
                field = field.Replace("\"", "\"\"");

            if (field.Contains(",") || field.Contains("\"") || field.Contains("\n") || field.Contains("\r"))
                field = $"\"{field}\"";

            return field;
        }
    }
}