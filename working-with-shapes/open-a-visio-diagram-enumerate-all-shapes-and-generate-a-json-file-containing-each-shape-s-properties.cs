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
            Console.Error.WriteLine($"Input file does not exist: {inputPath}");
            return;
        }

        try
        {
            // Load the Visio diagram inside a using block as required.
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Prepare a list to hold shape information for JSON serialization.
                List<ShapeInfo> shapeInfos = new List<ShapeInfo>();

                // Iterate over each page in the diagram.
                foreach (Page page in diagram.Pages)
                {
                    // Iterate over each shape on the current page.
                    foreach (Shape shape in page.Shapes)
                    {
                        // Skip logically deleted shapes.
                        if (shape.Del == BOOL.True)
                            continue;

                        // Extract the plain text from the shape.
                        string shapeText = shape.Text?.Value?.Text ?? string.Empty;

                        // Collect custom properties (shape.Props) into a dictionary.
                        Dictionary<string, string> customProps = new Dictionary<string, string>();
                        foreach (Prop prop in shape.Props)
                        {
                            // Prop.Value may be null; guard against it.
                            string propValue = prop.Value?.Val ?? string.Empty;
                            customProps[prop.Name] = propValue;
                        }

                        // Determine if the shape is a group (no IsGroup property; use Type check).
                        bool isGroup = shape.Type == TypeValue.Group;

                        // Build the ShapeInfo object with selected properties.
                        ShapeInfo info = new ShapeInfo
                        {
                            Id = shape.ID,
                            Name = shape.Name ?? string.Empty,
                            NameU = shape.NameU ?? string.Empty,
                            Type = shape.Type.ToString(),
                            IsDeleted = shape.Del == BOOL.True,
                            IsGroup = isGroup,
                            PinX = shape.XForm.PinX.Value,
                            PinY = shape.XForm.PinY.Value,
                            Width = shape.XForm.Width.Value,
                            Height = shape.XForm.Height.Value,
                            Text = shapeText,
                            CustomProperties = customProps
                        };

                        shapeInfos.Add(info);
                    }
                }

                // Serialize the list of shape information to JSON with indentation.
                string json = JsonSerializer.Serialize(shapeInfos, new JsonSerializerOptions { WriteIndented = true });

                // Write JSON to the specified output file.
                File.WriteAllText(outputPath, json);
                Console.WriteLine($"Shape information successfully written to: {outputPath}");
            }
        }
        catch (Exception ex)
        {
            // Report any errors that occur during processing.
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }

    // POCO class representing the properties we want to capture for each shape.
    private class ShapeInfo
    {
        public long Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string NameU { get; set; } = string.Empty;
        public string Type { get; set; } = string.Empty;
        public bool IsDeleted { get; set; }
        public bool IsGroup { get; set; }
        public double PinX { get; set; }
        public double PinY { get; set; }
        public double Width { get; set; }
        public double Height { get; set; }
        public string Text { get; set; } = string.Empty;
        public Dictionary<string, string> CustomProperties { get; set; } = new();
    }
}
