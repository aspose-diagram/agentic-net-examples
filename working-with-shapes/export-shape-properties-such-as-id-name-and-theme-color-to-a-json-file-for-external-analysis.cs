using System;
using System.IO;
using System.Collections.Generic;
using System.Text.Json;
using Aspose.Diagram;

class Program
{
    // Simple DTO to hold shape information for JSON serialization
    private class ShapeInfo
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string ThemeColor { get; set; }
    }

    static void Main(string[] args)
    {
        // Validate command‑line arguments
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <inputVisioPath> <outputJsonPath>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

        // Guard against missing input file
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file does not exist: {inputPath}");
            return;
        }

        try
        {
            // Load the Visio diagram inside a using block as required
            using (Diagram diagram = new Diagram(inputPath))
            {
                var shapesData = new List<ShapeInfo>();

                // Iterate through all pages in the diagram
                for (int pageIndex = 0; pageIndex < diagram.Pages.Count; pageIndex++)
                {
                    Page page = diagram.Pages[pageIndex];

                    // Iterate through all shapes on the current page
                    foreach (Shape shape in page.Shapes)
                    {
                        // Retrieve shape ID
                        long shapeId = shape.ID;

                        // Retrieve shape name (fallback to empty string if null)
                        string shapeName = shape.Name ?? string.Empty;

                        // Retrieve a theme‑related color.
                        // Using the foreground fill color as a representative theme color.
                        // If the fill color is not set, default to empty string.
                        string themeColor = string.Empty;
                        if (shape.Fill != null && shape.Fill.FillForegnd != null && shape.Fill.FillForegnd.Value != null)
                        {
                            themeColor = shape.Fill.FillForegnd.Value;
                        }

                        // Add collected information to the list
                        shapesData.Add(new ShapeInfo
                        {
                            Id = shapeId,
                            Name = shapeName,
                            ThemeColor = themeColor
                        });
                    }
                }

                // Serialize the list to JSON with indentation for readability
                var jsonOptions = new JsonSerializerOptions { WriteIndented = true };
                string json = JsonSerializer.Serialize(shapesData, jsonOptions);

                // Ensure the output directory exists
                string outputDir = Path.GetDirectoryName(outputPath);
                if (!string.IsNullOrEmpty(outputDir) && !Directory.Exists(outputDir))
                {
                    Directory.CreateDirectory(outputDir);
                }

                // Write JSON to the specified file
                File.WriteAllText(outputPath, json);
                Console.WriteLine($"Shape information exported to: {outputPath}");
            }
        }
        catch (Exception ex)
        {
            // Log any errors that occur during processing
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}