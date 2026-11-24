using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Expect three arguments: input Visio file, mapping CSV file, output Visio file.
        if (args.Length != 3)
        {
            Console.Error.WriteLine("Usage: Program <inputVisioPath> <mappingCsvPath> <outputVisioPath>");
            return;
        }

        string visioPath = args[0];
        string csvPath = args[1];
        string outputPath = args[2];

        // Guard against missing files.
        if (!File.Exists(visioPath))
        {
            Console.Error.WriteLine($"Input Visio file not found: {visioPath}");
            return;
        }
        if (!File.Exists(csvPath))
        {
            Console.Error.WriteLine($"Mapping CSV file not found: {csvPath}");
            return;
        }

        try
        {
            // Load the Visio diagram inside a using block as required.
            using (Diagram diagram = new Diagram(visioPath))
            {
                // Read the CSV mapping. Expected columns: ShapeId,FieldName,FieldValue
                // Simple CSV parsing assuming no commas inside fields.
                using (StreamReader reader = new StreamReader(csvPath))
                {
                    string headerLine = reader.ReadLine(); // Skip header if present.
                    while (!reader.EndOfStream)
                    {
                        string line = reader.ReadLine();
                        if (string.IsNullOrWhiteSpace(line))
                            continue;

                        string[] parts = line.Split(',');
                        if (parts.Length < 3)
                        {
                            Console.Error.WriteLine($"Invalid CSV line (expected 3 columns): {line}");
                            continue;
                        }

                        // Parse shape ID.
                        if (!long.TryParse(parts[0].Trim(), out long shapeId))
                        {
                            Console.Error.WriteLine($"Invalid ShapeId in line: {line}");
                            continue;
                        }

                        string fieldName = parts[1].Trim();
                        string fieldValue = parts[2].Trim();

                        // Locate the shape across all pages.
                        Shape targetShape = null;
                        foreach (Page page in diagram.Pages)
                        {
                            try
                            {
                                targetShape = page.Shapes.GetShape(shapeId);
                                if (targetShape != null)
                                    break;
                            }
                            catch
                            {
                                // Shape not found on this page; continue searching.
                            }
                        }

                        if (targetShape == null)
                        {
                            Console.Error.WriteLine($"Shape with ID {shapeId} not found in any page.");
                            continue;
                        }

                        // Create a new custom property (Prop) and add it to the shape.
                        Prop prop = new Prop();
                        // Set the label (display name) of the property.
                        prop.Label.Value = fieldName;
                        // Set the value of the property.
                        prop.Value.Val = fieldValue;
                        // Define the property type as string.
                        prop.Type.Value = TypePropValue.String;

                        // Add the property to the shape's Props collection.
                        targetShape.Props.Add(prop);
                    }
                }

                // Save the updated diagram to the specified output path.
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
                Console.WriteLine($"Diagram saved successfully to: {outputPath}");
            }
        }
        catch (Exception ex)
        {
            // Write any unexpected errors to the error console.
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}