using System;
using System.IO;
using System.Collections.Generic;
using System.Text.Json;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Validate command‑line arguments.
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <inputVisioPath> <outputJsonPath>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

        // Guard against missing input file.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the Visio diagram inside a using block as required.
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Prepare a list to hold extracted shape information.
                List<ShapeInfo> shapesInfo = new List<ShapeInfo>();

                // Iterate through all pages in the diagram.
                foreach (Page page in diagram.Pages)
                {
                    // Iterate through all shapes on the current page.
                    foreach (Aspose.Diagram.Shape shape in page.Shapes)
                    {
                        // Extract the shape ID.
                        long id = shape.ID;

                        // Extract the shape name (may be empty).
                        string name = shape.Name ?? string.Empty;

                        // Attempt to retrieve a theme‑related color.
                        // Here we use the foreground fill color as a representative theme color.
                        string themeColor = string.Empty;
                        if (shape.Fill != null && shape.Fill.FillForegnd != null && shape.Fill.FillForegnd.Value != null)
                        {
                            themeColor = shape.Fill.FillForegnd.Value;
                        }

                        // Add the collected data to the list.
                        shapesInfo.Add(new ShapeInfo
                        {
                            ID = id,
                            Name = name,
                            ThemeColor = themeColor
                        });
                    }
                }

                // Serialize the list to JSON with indentation for readability.
                var jsonOptions = new JsonSerializerOptions { WriteIndented = true };
                string json = JsonSerializer.Serialize(shapesInfo, jsonOptions);

                // Write the JSON output to the specified file.
                File.WriteAllText(outputPath, json);
                Console.WriteLine($"Shape information exported to: {outputPath}");
            }
        }
        catch (Exception ex)
        {
            // Report any errors that occur during processing.
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }

    // Simple DTO to hold shape properties for JSON serialization.
    private class ShapeInfo
    {
        public long ID { get; set; }
        public string Name { get; set; }
        public string ThemeColor { get; set; }
    }
}