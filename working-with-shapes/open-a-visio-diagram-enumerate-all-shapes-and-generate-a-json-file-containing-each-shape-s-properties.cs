using System;
using System.IO;
using System.Collections.Generic;
using System.Text.Json;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Validate that an input file path was provided.
        if (args.Length < 1)
        {
            Console.Error.WriteLine("Usage: Program <inputVisioFile> [outputJsonFile]");
            return;
        }

        // Input Visio file path from the first argument.
        string inputPath = args[0];
        // Guard: ensure the input file exists before proceeding.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Determine output JSON file path (optional second argument or default).
        string outputPath = args.Length >= 2 ? args[1] : "shapes.json";

        try
        {
            // Load the Visio diagram from the specified file.
            Diagram diagram = new Diagram(inputPath);

            // Prepare a list to hold information about every shape in the diagram.
            List<ShapeInfo> allShapes = new List<ShapeInfo>();

            // Iterate over each page in the diagram using explicit type (no var).
            foreach (Page page in diagram.Pages)
            {
                // Iterate over each shape on the current page.
                foreach (Shape shape in page.Shapes)
                {
                    // Skip shapes that are marked as deleted (shape.Del == BOOL.True).
                    if (shape.Del == BOOL.True)
                        continue;

                    // Create a DTO to capture basic shape properties.
                    ShapeInfo info = new ShapeInfo
                    {
                        Id = shape.ID,
                        Name = shape.Name,
                        NameU = shape.NameU,
                        // Master may be null for some shapes; use null‑conditional operator.
                        MasterName = shape.Master != null ? shape.Master.Name : null,
                        Type = shape.Type.ToString(),
                        // Capture geometric data from the XForm cell collection.
                        PinX = shape.XForm.PinX.Value,
                        PinY = shape.XForm.PinY.Value,
                        Width = shape.XForm.Width.Value,
                        Height = shape.XForm.Height.Value,
                        Angle = shape.XForm.Angle.Value,
                        // Extract plain text from the shape (concatenated Txt runs).
                        Text = shape.Text.Value.Text,
                        // Initialize the custom properties list.
                        CustomProperties = new List<PropInfo>()
                    };

                    // Enumerate custom shape properties (Props collection) if present.
                    if (shape.Props != null)
                    {
                        foreach (Prop prop in shape.Props)
                        {
                            // Add each property name/value pair to the DTO.
                            PropInfo pInfo = new PropInfo
                            {
                                Name = prop.Name,
                                Value = prop.Value.Val
                            };
                            info.CustomProperties.Add(pInfo);
                        }
                    }

                    // Add the populated shape info to the master list.
                    allShapes.Add(info);
                }
            }

            // Serialize the list of shape DTOs to indented JSON.
            JsonSerializerOptions jsonOptions = new JsonSerializerOptions
            {
                WriteIndented = true
            };
            string json = JsonSerializer.Serialize(allShapes, jsonOptions);

            // Write the JSON string to the output file (overwrites if exists).
            File.WriteAllText(outputPath, json);
            Console.WriteLine($"Shape data exported to: {outputPath}");
        }
        catch (Exception ex)
        {
            // Log any unexpected errors to the error stream.
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }

    // DTO representing a shape's exported data.
    public class ShapeInfo
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string NameU { get; set; }
        public string MasterName { get; set; }
        public string Type { get; set; }
        public double PinX { get; set; }
        public double PinY { get; set; }
        public double Width { get; set; }
        public double Height { get; set; }
        public double Angle { get; set; }
        public string Text { get; set; }
        public List<PropInfo> CustomProperties { get; set; }
    }

    // DTO for a custom property (Prop) on a shape.
    public class PropInfo
    {
        public string Name { get; set; }
        public string Value { get; set; }
    }
}