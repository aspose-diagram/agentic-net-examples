using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Expect two arguments: diagram file path and CSV file path with intended angles.
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <diagramPath> <anglesCsvPath>");
            return;
        }

        string diagramPath = args[0];
        string csvPath = args[1];

        // Guard against missing files.
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

        // Load intended rotation values from CSV.
        // CSV format: ShapeId,AngleInDegrees (e.g., 123,45.0)
        var intendedAngles = new Dictionary<long, double>();
        try
        {
            foreach (var line in File.ReadAllLines(csvPath))
            {
                if (string.IsNullOrWhiteSpace(line))
                    continue;

                var parts = line.Split(',');
                if (parts.Length != 2)
                {
                    Console.Error.WriteLine($"Invalid CSV line (expected 2 columns): {line}");
                    continue;
                }

                if (!long.TryParse(parts[0].Trim(), out long shapeId))
                {
                    Console.Error.WriteLine($"Invalid ShapeId in CSV line: {line}");
                    continue;
                }

                if (!double.TryParse(parts[1].Trim(), out double angle))
                {
                    Console.Error.WriteLine($"Invalid Angle in CSV line: {line}");
                    continue;
                }

                intendedAngles[shapeId] = angle;
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error reading CSV file: {ex.Message}");
            return;
        }

        // Load the Visio diagram.
        Diagram diagram;
        try
        {
            diagram = new Diagram(diagramPath);
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Failed to load diagram: {ex.Message}");
            return;
        }

        // Iterate through all pages and shapes to verify angles.
        foreach (Page page in diagram.Pages)
        {
            foreach (Shape shape in page.Shapes)
            {
                // Skip logically deleted shapes.
                if (shape.Del == BOOL.True)
                    continue;

                // Check if we have an intended angle for this shape.
                if (!intendedAngles.TryGetValue(shape.ID, out double expectedAngle))
                    continue; // No expectation defined; skip verification.

                // Retrieve the actual angle from the shape's XForm.
                double actualAngle = shape.XForm.Angle.Value;

                // Compare with a tolerance to account for floating‑point precision.
                const double tolerance = 0.0001;
                if (Math.Abs(actualAngle - expectedAngle) > tolerance)
                {
                    Console.Error.WriteLine($"Shape ID {shape.ID} angle mismatch. Expected: {expectedAngle}, Actual: {actualAngle}");
                }
                else
                {
                    Console.WriteLine($"Shape ID {shape.ID} angle verified: {actualAngle}°");
                }
            }
        }
    }
}