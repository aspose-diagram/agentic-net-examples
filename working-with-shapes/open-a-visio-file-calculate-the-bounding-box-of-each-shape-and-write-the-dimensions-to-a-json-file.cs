using System;
using System.IO;
using System.Collections.Generic;
using System.Text.Json;
using Aspose.Diagram;

class Program
{
    // DTO for JSON serialization of shape dimensions
    public class ShapeInfo
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public double Width { get; set; }
        public double Height { get; set; }
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
                // List to hold shape dimension data
                List<ShapeInfo> shapesInfo = new List<ShapeInfo>();

                // Iterate through all pages
                foreach (Page page in diagram.Pages)
                {
                    // Iterate through all shapes on the current page
                    foreach (Shape shape in page.Shapes)
                    {
                        // Ensure the shape has a valid XForm (required for dimensions)
                        if (shape.XForm != null)
                        {
                            double width = shape.XForm.Width != null ? shape.XForm.Width.Value : 0.0;
                            double height = shape.XForm.Height != null ? shape.XForm.Height.Value : 0.0;

                            ShapeInfo info = new ShapeInfo
                            {
                                Id = shape.ID,
                                Name = shape.NameU,
                                Width = width,
                                Height = height
                            };

                            shapesInfo.Add(info);
                        }
                    }
                }

                // Serialize the list to JSON with indentation for readability
                JsonSerializerOptions jsonOptions = new JsonSerializerOptions
                {
                    WriteIndented = true
                };
                string json = JsonSerializer.Serialize(shapesInfo, jsonOptions);

                // Write JSON to the output file (overwrite if exists)
                File.WriteAllText(outputPath, json);
                Console.WriteLine($"Shape dimensions have been written to: {outputPath}");
            }
        }
        catch (Exception ex)
        {
            // Capture any Aspose or I/O errors
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}