using System;
using System.IO;
using System.Collections.Generic;
using System.Text.Json;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Expect two arguments: input Visio file path and output JSON file path
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <inputVisioPath> <outputJsonPath>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

        // Guard: verify input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Ensure output directory exists
        string outputDir = Path.GetDirectoryName(outputPath);
        if (!string.IsNullOrEmpty(outputDir) && !Directory.Exists(outputDir))
        {
            Directory.CreateDirectory(outputDir);
        }

        try
        {
            // Load the Visio diagram inside a using block
            using (Diagram diagram = new Diagram(inputPath))
            {
                // List to hold information about each shape
                List<ShapeInfo> allShapes = new List<ShapeInfo>();

                // Iterate through all pages in the diagram
                foreach (Page page in diagram.Pages)
                {
                    // Iterate through all shapes on the current page
                    foreach (Shape shape in page.Shapes)
                    {
                        // Skip shapes that are marked as deleted
                        if (shape.Del == BOOL.True)
                            continue;

                        // Determine if the shape is a group
                        bool isGroup = shape.Type == TypeValue.Group;

                        // Populate basic shape information
                        ShapeInfo info = new ShapeInfo
                        {
                            Id = shape.ID,
                            Name = shape.Name,               // Name is a plain string
                            NameU = shape.NameU,             // Universal name, also a plain string
                            Type = shape.Type.ToString(),
                            IsGroup = isGroup,
                            PinX = shape.XForm.PinX.Value,
                            PinY = shape.XForm.PinY.Value,
                            Width = shape.XForm.Width.Value,
                            Height = shape.XForm.Height.Value,
                            Text = shape.Text.Value.Text   // Plain text of the shape
                        };

                        // Extract custom properties (Props collection)
                        Dictionary<string, string> props = new Dictionary<string, string>();
                        foreach (Prop prop in shape.Props)
                        {
                            if (!string.IsNullOrEmpty(prop.Name))
                            {
                                string propValue = prop.Value?.Val ?? string.Empty;
                                props[prop.Name] = propValue;
                            }
                        }
                        info.CustomProperties = props;

                        // Extract user-defined cells (Users collection)
                        Dictionary<string, string> users = new Dictionary<string, string>();
                        foreach (User user in shape.Users)
                        {
                            if (!string.IsNullOrEmpty(user.NameU))
                            {
                                string userValue = user.Value?.Val ?? string.Empty;
                                users[user.NameU] = userValue;
                            }
                        }
                        info.UserFields = users;

                        // Add the populated shape info to the list
                        allShapes.Add(info);
                    }
                }

                // Serialize the shape information to JSON with indentation
                string json = JsonSerializer.Serialize(allShapes, new JsonSerializerOptions { WriteIndented = true });

                // Write the JSON output to the specified file
                File.WriteAllText(outputPath, json);
                Console.WriteLine($"Shape information exported to {outputPath}");
            }
        }
        catch (Exception ex)
        {
            // Write any errors that occur during processing to the error stream
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }

    // Helper class representing the JSON structure for a shape
    public class ShapeInfo
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string NameU { get; set; }
        public string Type { get; set; }
        public bool IsGroup { get; set; }
        public double PinX { get; set; }
        public double PinY { get; set; }
        public double Width { get; set; }
        public double Height { get; set; }
        public string Text { get; set; }
        public Dictionary<string, string> CustomProperties { get; set; } = new();
        public Dictionary<string, string> UserFields { get; set; } = new();
    }
}