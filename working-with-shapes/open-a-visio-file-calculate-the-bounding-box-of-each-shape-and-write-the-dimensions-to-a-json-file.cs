using System;
using System.IO;
using System.Collections.Generic;
using System.Text.Json;
using Aspose.Diagram;

class Program
{
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

        // Guard: ensure the input Visio file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the Visio diagram inside a using block (required by the API)
            using (Diagram diagram = new Diagram(inputPath))
            {
                // List to hold dimension information for each shape
                List<ShapeInfo> shapeInfos = new List<ShapeInfo>();

                // Iterate over all pages in the diagram
                foreach (Page page in diagram.Pages)
                {
                    // Iterate over all shapes on the current page
                    foreach (Shape shape in page.Shapes)
                    {
                        // Skip shapes that are marked as deleted
                        if (shape.Del == BOOL.True)
                            continue;

                        // Retrieve width and height (values are in inches)
                        double width = shape.XForm.Width.Value;
                        double height = shape.XForm.Height.Value;

                        // Create a ShapeInfo object and add it to the collection
                        ShapeInfo info = new ShapeInfo
                        {
                            Id = shape.ID,          // shape.ID is a long
                            Name = shape.NameU,
                            Width = width,
                            Height = height
                        };
                        shapeInfos.Add(info);
                    }
                }

                // Serialize the list to JSON with indentation for readability
                JsonSerializerOptions jsonOptions = new JsonSerializerOptions
                {
                    WriteIndented = true
                };
                string json = JsonSerializer.Serialize(shapeInfos, jsonOptions);

                // Ensure the output directory exists
                string outputDir = Path.GetDirectoryName(outputPath);
                if (!string.IsNullOrEmpty(outputDir) && !Directory.Exists(outputDir))
                {
                    Directory.CreateDirectory(outputDir);
                }

                // Write the JSON content to the output file
                File.WriteAllText(outputPath, json);
                Console.WriteLine($"Shape dimensions have been written to: {outputPath}");
            }
        }
        catch (Exception ex)
        {
            // Output any errors to the error console
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }

    // DTO class representing the dimensions of a shape
    public class ShapeInfo
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public double Width { get; set; }
        public double Height { get; set; }
    }
}