using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Input and output file paths
        string inputPath = "input.vsdx";
        if (!System.IO.File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }
        string outputPath = "output.vsdx";

        // Guard against missing input file
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the diagram
            Diagram diagram = new Diagram(inputPath);

            // Define text color mapping based on shape master name (type)
            var typeColorMap = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
            {
                { "Rectangle", "#FF0000" },   // Red for rectangles
                { "Ellipse",   "#00FF00" },   // Green for ellipses
                { "Hexagon",   "#0000FF" },   // Blue for hexagons
                // Add more mappings as needed
            };

            // Prepare a stylesheet per shape type
            var typeStyleMap = new Dictionary<string, StyleSheet>(StringComparer.OrdinalIgnoreCase);

            foreach (var kvp in typeColorMap)
            {
                // Create a new stylesheet
                StyleSheet ss = new StyleSheet();
                ss.ID = diagram.StyleSheets.Count + 1; // Unique ID

                // Define a character style with the desired text color
                Aspose.Diagram.Char ch = new Aspose.Diagram.Char();
                ch.Color.Value = kvp.Value; // Hex color string
                ch.IX = 0; // Index of the character style
                ss.Chars.Add(ch);

                // Add stylesheet to the diagram and store it for later use
                diagram.StyleSheets.Add(ss);
                typeStyleMap[kvp.Key] = ss;
            }

            // Apply the appropriate stylesheet to each shape based on its master name
            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    // Skip deleted shapes
                    if (shape.Del == BOOL.True)
                        continue;

                    // Determine shape type via master name; fallback to "Unknown"
                    string shapeType = shape.Master?.Name ?? "Unknown";

                    // Find a matching stylesheet; if none, skip
                    if (typeStyleMap.TryGetValue(shapeType, out StyleSheet ss))
                    {
                        // Assign the stylesheet to the shape's text style
                        shape.TextStyle = ss;
                    }
                }
            }

            // Validation: ensure each shape's text color matches the expected mapping
            bool validationFailed = false;
            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    if (shape.Del == BOOL.True)
                        continue;

                    string shapeType = shape.Master?.Name ?? "Unknown";

                    if (typeColorMap.TryGetValue(shapeType, out string expectedColor))
                    {
                        // Retrieve the color from the assigned stylesheet (if any)
                        string actualColor = shape.TextStyle?.Chars.Count > 0
                            ? shape.TextStyle.Chars[0].Color.Value
                            : null;

                        if (!string.Equals(actualColor, expectedColor, StringComparison.OrdinalIgnoreCase))
                        {
                            Console.Error.WriteLine($"Validation error on shape ID {shape.ID}: Expected color {expectedColor}, but found {(actualColor ?? "null")}.");
                            validationFailed = true;
                        }
                    }
                }
            }

            if (validationFailed)
            {
                Console.Error.WriteLine("One or more shapes have incorrect text colors.");
                // Optionally, you could throw an exception to stop further processing
                // throw new Exception("Validation failed.");
            }
            else
            {
                Console.WriteLine("All shape text colors validated successfully.");
            }

            // Save the modified diagram
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Diagram saved to {outputPath}");
        }
        catch (Exception ex)
        {
            // Write any Aspose or runtime errors to the error stream
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}