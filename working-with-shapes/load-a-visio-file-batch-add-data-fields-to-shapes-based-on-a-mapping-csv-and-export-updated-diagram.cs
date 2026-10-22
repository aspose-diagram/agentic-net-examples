using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Validate command‑line arguments.
        if (args.Length < 3)
        {
            Console.Error.WriteLine("Usage: Program <inputVisioPath> <mappingCsvPath> <outputVisioPath>");
            return;
        }

        string inputVisioPath = args[0];
        string mappingCsvPath = args[1];
        string outputVisioPath = args[2];

        // Guard against missing input files.
        if (!File.Exists(inputVisioPath))
        {
            Console.Error.WriteLine($"Input Visio file not found: {inputVisioPath}");
            return;
        }

        if (!File.Exists(mappingCsvPath))
        {
            Console.Error.WriteLine($"Mapping CSV file not found: {mappingCsvPath}");
            return;
        }

        // Load the Visio diagram inside a using block as required.
        try
        {
            using (Diagram diagram = new Diagram(inputVisioPath))
            {
                // Read CSV mapping lines (skip empty lines and optional header).
                string[] csvLines = File.ReadAllLines(mappingCsvPath);
                List<string[]> mappings = new List<string[]>();

                foreach (string line in csvLines)
                {
                    if (string.IsNullOrWhiteSpace(line))
                        continue;

                    // Simple CSV split – assumes no commas inside quoted fields.
                    string[] parts = line.Split(',');

                    // Expect at least three columns: ShapeId, FieldName, FieldValue.
                    if (parts.Length < 3)
                        continue; // Skip malformed lines.

                    mappings.Add(parts);
                }

                // Process each mapping entry.
                foreach (string[] map in mappings)
                {
                    // Parse shape identifier (assumed to be a numeric ID).
                    if (!long.TryParse(map[0].Trim(), out long shapeId))
                        continue; // Skip if shape ID is not a valid number.

                    string fieldName = map[1].Trim();   // Currently not used – placeholder for future logic.
                    string fieldValue = map[2].Trim();

                    // Locate the shape across all pages.
                    Shape targetShape = null;
                    foreach (Page page in diagram.Pages)
                    {
                        // GetShape returns null if the ID does not exist on this page.
                        Shape shape = page.Shapes.GetShape(shapeId);
                        if (shape != null)
                        {
                            targetShape = shape;
                            break;
                        }
                    }

                    if (targetShape == null)
                    {
                        Console.Error.WriteLine($"Shape with ID {shapeId} not found in any page.");
                        continue;
                    }

                    // Create a new text field and set its value.
                    Field newField = new Field();

                    // Assign the field's value.
                    newField.Value.Val = fieldValue;
                    // Ensure unit is undefined as required by the API.
                    newField.Value.Ufev.Unit = MeasureConst.Undefined;
                    newField.Value.Ufev.F = "";

                    // Add the field to the shape's Fields collection.
                    targetShape.Fields.Add(newField);
                }

                // Save the updated diagram to the specified output path.
                diagram.Save(outputVisioPath, SaveFileFormat.Vsdx);
                Console.WriteLine($"Diagram saved successfully to: {outputVisioPath}");
            }
        }
        catch (Exception ex)
        {
            // Write any Aspose or IO errors to the error stream.
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}