using System;
using System.IO;
using System.Collections.Generic;
using System.Globalization;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Expect two arguments: diagram file path and external data file path
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <diagramPath> <dataPath>");
            return;
        }

        string diagramPath = args[0];
        string dataPath = args[1];

        // Guard: ensure diagram file exists
        if (!File.Exists(diagramPath))
        {
            Console.Error.WriteLine($"Diagram file not found: {diagramPath}");
            return;
        }

        // Guard: ensure external data file exists
        if (!File.Exists(dataPath))
        {
            Console.Error.WriteLine($"Data file not found: {dataPath}");
            return;
        }

        // Load external data into a dictionary (shape name -> numeric value)
        var shapeValues = new Dictionary<string, double>(StringComparer.OrdinalIgnoreCase);
        try
        {
            foreach (string line in File.ReadAllLines(dataPath))
            {
                if (string.IsNullOrWhiteSpace(line))
                    continue;

                // Expected format: ShapeName,Value
                string[] parts = line.Split(new[] { ',' }, 2);
                if (parts.Length != 2)
                {
                    Console.Error.WriteLine($"Invalid data line (expected 'ShapeName,Value'): {line}");
                    continue;
                }

                string shapeName = parts[0].Trim();
                if (double.TryParse(parts[1].Trim(), NumberStyles.Any, CultureInfo.InvariantCulture, out double value))
                {
                    shapeValues[shapeName] = value;
                }
                else
                {
                    Console.Error.WriteLine($"Unable to parse numeric value for shape '{shapeName}': {parts[1]}");
                }
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error reading data file: {ex.Message}");
            return;
        }

        // Process the diagram
        try
        {
            using (Diagram diagram = new Diagram(diagramPath))
            {
                // Iterate through all pages and shapes
                foreach (Page page in diagram.Pages)
                {
                    foreach (Shape shape in page.Shapes)
                    {
                        // Skip deleted shapes
                        if (shape.Del == BOOL.True)
                            continue;

                        // Apply formatting only if the shape name exists in the external data
                        if (!string.IsNullOrEmpty(shape.NameU) && shapeValues.TryGetValue(shape.NameU, out double metric))
                        {
                            // Example rule: metric >= 100 => red fill, else green fill
                            if (metric >= 100)
                            {
                                // Red fill
                                shape.Fill.FillForegnd.Value = "#FF0000";
                                // Thicker line for high values
                                shape.Line.LineWeight.Value = 0.03; // inches
                            }
                            else
                            {
                                // Green fill
                                shape.Fill.FillForegnd.Value = "#00FF00";
                                // Thinner line for lower values
                                shape.Line.LineWeight.Value = 0.01; // inches
                            }

                            // Ensure the shape is not flipped horizontally
                            shape.XForm.FlipX.Value = BOOL.False;
                        }
                    }
                }

                // Determine output path (same directory with suffix)
                string outputPath = Path.Combine(
                    Path.GetDirectoryName(diagramPath) ?? string.Empty,
                    Path.GetFileNameWithoutExtension(diagramPath) + "_formatted.vsdx");

                // Save the modified diagram in VSDX format
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
                Console.WriteLine($"Diagram saved with conditional formatting to: {outputPath}");
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}