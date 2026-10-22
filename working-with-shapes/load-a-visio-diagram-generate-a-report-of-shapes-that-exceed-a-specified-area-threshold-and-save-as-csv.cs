using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Validate command‑line arguments.
        if (args.Length < 3)
        {
            Console.Error.WriteLine("Usage: <program> <inputVisioPath> <outputCsvPath> <areaThreshold>");
            return;
        }

        string inputPath = args[0];
        string outputCsvPath = args[1];
        string thresholdArg = args[2];

        // Guard: ensure the input Visio file exists.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Error: Input file \"{inputPath}\" does not exist.");
            return;
        }

        // Parse the area threshold (in square inches).
        if (!double.TryParse(thresholdArg, out double areaThreshold))
        {
            Console.Error.WriteLine($"Error: Unable to parse area threshold \"{thresholdArg}\" as a number.");
            return;
        }

        try
        {
            // Load the Visio diagram.
            Diagram diagram = new Diagram(inputPath);

            // Prepare CSV lines.
            var reportLines = new List<string>();
            // CSV header.
            reportLines.Add("ShapeID,ShapeName,MasterName,Area");

            // Iterate through all pages.
            foreach (Page page in diagram.Pages)
            {
                // Iterate through all shapes on the page.
                foreach (Shape shape in page.Shapes)
                {
                    // Retrieve width and height (in inches) from the shape's XForm.
                    double width = shape.XForm.Width.Value;
                    double height = shape.XForm.Height.Value;
                    double area = width * height;

                    // Check if the shape's area exceeds the threshold.
                    if (area > areaThreshold)
                    {
                        // Gather shape details.
                        long shapeId = shape.ID;
                        string shapeName = shape.Name ?? string.Empty;
                        string masterName = shape.Master != null ? shape.Master.Name ?? string.Empty : string.Empty;

                        // Escape commas in text fields.
                        shapeName = shapeName.Replace(",", ";");
                        masterName = masterName.Replace(",", ";");

                        // Build CSV line.
                        string line = $"{shapeId},{shapeName},{masterName},{area}";
                        reportLines.Add(line);
                    }
                }
            }

            // Write the CSV report to the specified output file.
            File.WriteAllLines(outputCsvPath, reportLines);
            Console.WriteLine($"Report generated successfully at \"{outputCsvPath}\".");
        }
        catch (Exception ex)
        {
            // Capture any Aspose.Diagram or I/O errors.
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}