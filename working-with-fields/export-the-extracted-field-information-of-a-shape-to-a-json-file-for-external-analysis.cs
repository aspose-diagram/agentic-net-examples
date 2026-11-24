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
        if (args.Length < 3)
        {
            Console.Error.WriteLine("Usage: Program <inputVisioPath> <shapeId> <outputJsonPath>");
            return;
        }

        string inputPath = args[0];
        string shapeIdArg = args[1];
        string outputPath = args[2];

        // Guard against missing input file.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Error: Input file \"{inputPath}\" does not exist.");
            return;
        }

        // Parse shape ID.
        if (!long.TryParse(shapeIdArg, out long targetShapeId))
        {
            Console.Error.WriteLine($"Error: Shape ID \"{shapeIdArg}\" is not a valid integer.");
            return;
        }

        try
        {
            // Load the Visio diagram inside a using block (required pattern).
            using (Diagram diagram = new Diagram(inputPath))
            {
                Shape targetShape = null;

                // Search for the shape with the specified ID across all pages.
                foreach (Page page in diagram.Pages)
                {
                    // GetShape returns null if the shape is not on this page.
                    Shape shape = page.Shapes.GetShape(targetShapeId);
                    if (shape != null)
                    {
                        targetShape = shape;
                        break;
                    }
                }

                if (targetShape == null)
                {
                    Console.Error.WriteLine($"Error: No shape with ID {targetShapeId} was found in the diagram.");
                    return;
                }

                // Collect field information from the shape.
                List<FieldInfo> fieldsInfo = new List<FieldInfo>();

                // Ensure the shape actually contains fields.
                if (targetShape.Fields != null && targetShape.Fields.Count > 0)
                {
                    foreach (Field field in targetShape.Fields)
                    {
                        // Extract the displayed value.
                        string value = field.Value?.Val ?? string.Empty;

                        // Extract the field type as a string.
                        string type = field.Type != null ? field.Type.Value.ToString() : "Undefined";

                        // Extract the underlying formula, if any.
                        string formula = field.Value?.Ufev?.F ?? string.Empty;

                        fieldsInfo.Add(new FieldInfo
                        {
                            Value = value,
                            Type = type,
                            Formula = formula
                        });
                    }
                }
                else
                {
                    Console.Error.WriteLine($"Warning: Shape ID {targetShapeId} contains no fields.");
                }

                // Serialize the collected field data to JSON.
                var jsonOptions = new JsonSerializerOptions { WriteIndented = true };
                string json = JsonSerializer.Serialize(fieldsInfo, jsonOptions);

                // Write JSON to the specified output file.
                File.WriteAllText(outputPath, json);
                Console.WriteLine($"Field information exported successfully to \"{outputPath}\".");
            }
        }
        catch (Exception ex)
        {
            // Capture any Aspose or I/O errors.
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }

    // DTO for JSON serialization of field data.
    private class FieldInfo
    {
        public string? Value { get; set; }
        public string? Type { get; set; }
        public string? Formula { get; set; }
    }
}