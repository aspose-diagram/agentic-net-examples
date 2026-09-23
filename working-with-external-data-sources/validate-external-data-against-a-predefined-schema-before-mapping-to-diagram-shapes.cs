using System;
using System.Collections.Generic;
using Aspose.Diagram;

namespace DiagramDataValidation
{
    // Represents a simple field definition for validation
    class SchemaField
    {
        public string Name { get; set; }
        public Type DataType { get; set; }
        public bool Required { get; set; }
    }

    class Program
    {
        static void Main()
        {
            try
            {

                // Paths to the template diagram and the output file
                string templatePath = "template.vsdx";
                string outputPath = "output.vsdx";

                // Load the base diagram (must contain a "Rectangle" master)
                Diagram diagram = new Diagram(templatePath);

                // Define the expected schema for external data
                List<SchemaField> schema = new List<SchemaField>
                {
                    new SchemaField { Name = "Id",   DataType = typeof(int),    Required = true },
                    new SchemaField { Name = "Name", DataType = typeof(string), Required = true },
                    new SchemaField { Name = "Value",DataType = typeof(double), Required = true }
                };

                // Simulated external data (could be read from a file, DB, etc.)
                List<Dictionary<string, string>> externalData = new List<Dictionary<string, string>>
                {
                    new Dictionary<string, string>
                    {
                        { "Id", "1" },
                        { "Name", "Alpha" },
                        { "Value", "12.34" }
                    },
                    new Dictionary<string, string>
                    {
                        { "Id", "2" },
                        { "Name", "Beta" },
                        { "Value", "56.78" }
                    },
                    // Add more rows as needed
                };

                // Validate each data row against the schema
                foreach (var row in externalData)
                {
                    foreach (var field in schema)
                    {
                        // Check required presence
                        if (field.Required && !row.ContainsKey(field.Name))
                        {
                            throw new Exception($"Missing required field '{field.Name}'.");
                        }

                        // If the field exists, validate its type
                        if (row.TryGetValue(field.Name, out string rawValue))
                        {
                            bool valid = field.DataType switch
                            {
                                Type t when t == typeof(int) => int.TryParse(rawValue, out _),
                                Type t when t == typeof(double) => double.TryParse(rawValue, out _),
                                Type t when t == typeof(string) => true, // any string is acceptable
                                _ => false
                            };

                            if (!valid)
                            {
                                throw new Exception($"Field '{field.Name}' has invalid value '{rawValue}'.");
                            }
                        }
                    }
                }

                // Mapping validated data to diagram shapes
                Page page = diagram.Pages[0];
                double startX = 1.0;   // inches from left
                double startY = 1.0;   // inches from top
                double verticalSpacing = 2.0; // inches between shapes

                for (int i = 0; i < externalData.Count; i++)
                {
                    var row = externalData[i];
                    double pinX = startX;
                    double pinY = startY + i * verticalSpacing;

                    // Add a rectangle shape; the fourth parameter isCalculate must be false
                    long shapeId = page.AddShape(pinX, pinY, "Rectangle", false);

                    // Retrieve the shape object to modify its properties
                    Shape shape = page.Shapes.GetShape(shapeId);

                    // Set the shape's text to display Name and Value
                    shape.Text.Value.Clear();
                    string displayText = $"{row["Name"]}: {row["Value"]}";
                    shape.Text.Value.Add(new Txt(displayText));
                }

                // Save the modified diagram
                diagram.Save(outputPath, SaveFileFormat.Vsdx);

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }
}