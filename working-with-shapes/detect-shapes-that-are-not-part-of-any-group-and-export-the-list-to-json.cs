using System;
using System.IO;
using System.Collections.Generic;
using System.Text.Json;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Validate input arguments
        if (args.Length < 1)
        {
            Console.Error.WriteLine("Usage: Program <inputVisioFilePath> [outputJsonPath]");
            return;
        }

        string inputPath = args[0];
        string outputPath = args.Length >= 2 ? args[1] : "ungrouped_shapes.json";

        // Guard against missing input file
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file does not exist: {inputPath}");
            return;
        }

        try
        {
            // Load the Visio diagram
            Diagram diagram = new Diagram(inputPath);

            // HashSet to store IDs of shapes that belong to any group (including nested groups)
            HashSet<long> groupedShapeIds = new HashSet<long>();

            // Recursive helper to collect child shape IDs from a group shape
            void CollectGroupShapeIds(Shape groupShape)
            {
                // Iterate over direct child shapes of the group
                foreach (Shape child in groupShape.Shapes)
                {
                    // Add child shape ID to the set
                    groupedShapeIds.Add(child.ID);

                    // If the child itself is a group, recurse into it
                    if (child.Type == TypeValue.Group)
                    {
                        CollectGroupShapeIds(child);
                    }
                }
            }

            // First pass: find all group shapes and collect IDs of their members
            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    if (shape.Type == TypeValue.Group)
                    {
                        // The group shape itself is not considered an "ungrouped" shape,
                        // but its children are part of a group.
                        CollectGroupShapeIds(shape);
                    }
                }
            }

            // List to hold information about shapes that are not part of any group
            var ungroupedShapes = new List<object>();

            // Second pass: identify shapes that are not groups and not in any group collection
            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    // Skip group shapes themselves
                    if (shape.Type == TypeValue.Group)
                        continue;

                    // If the shape ID is not in the grouped set, it is ungrouped
                    if (!groupedShapeIds.Contains(shape.ID))
                    {
                        // Gather basic information about the shape
                        string shapeText = shape.Text != null && shape.Text.Value != null
                            ? shape.Text.Value.ToString()
                            : string.Empty;

                        ungroupedShapes.Add(new
                        {
                            Id = shape.ID,
                            Type = shape.Type.ToString(),
                            Text = shapeText,
                            Page = page.Name
                        });
                    }
                }
            }

            // Serialize the list to JSON with indentation for readability
            string json = JsonSerializer.Serialize(ungroupedShapes, new JsonSerializerOptions { WriteIndented = true });

            // Write JSON to the output file
            File.WriteAllText(outputPath, json);
            Console.WriteLine($"Exported {ungroupedShapes.Count} ungrouped shape(s) to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            // Capture any errors from Aspose.Diagram or I/O operations
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}