using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Expect two arguments: Visio file path and Excel CSV file path
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <VisioFilePath> <ExcelCsvFilePath>");
            return;
        }

        string visioPath = args[0];
        string excelCsvPath = args[1];

        // Guard: check Visio file existence
        if (!File.Exists(visioPath))
        {
            Console.Error.WriteLine($"Visio file not found: {visioPath}");
            return;
        }

        // Guard: check Excel CSV file existence
        if (!File.Exists(excelCsvPath))
        {
            Console.Error.WriteLine($"Excel CSV file not found: {excelCsvPath}");
            return;
        }

        try
        {
            // Load the Visio diagram inside a using block as required
            using (Diagram diagram = new Diagram(visioPath))
            {
                // Read the external Excel values (assumed CSV for simplicity)
                // Expected format per line: ShapeName,DataGraphicValue
                var dataMap = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
                foreach (string line in File.ReadAllLines(excelCsvPath))
                {
                    // Skip empty lines
                    if (string.IsNullOrWhiteSpace(line))
                        continue;

                    string[] parts = line.Split(',');
                    if (parts.Length >= 2)
                    {
                        string shapeName = parts[0].Trim();
                        string graphicValue = parts[1].Trim();
                        dataMap[shapeName] = graphicValue;
                    }
                }

                // Iterate through all pages and shapes to apply data graphics (as custom properties)
                foreach (Page page in diagram.Pages)
                {
                    foreach (Shape shape in page.Shapes)
                    {
                        // Ensure the shape is not marked for deletion
                        if (shape.Del == BOOL.True)
                            continue;

                        // Match shape by its universal name
                        if (!string.IsNullOrEmpty(shape.NameU) && dataMap.TryGetValue(shape.NameU, out string value))
                        {
                            // Add a custom property to store the data graphic value
                            // This serves as a placeholder since explicit DataGraphic APIs are not available in the reference
                            Prop prop = new Prop();
                            prop.Name = "DataGraphic";
                            prop.Label.Value = "Data Graphic";
                            prop.Value.Val = value;
                            prop.Type.Value = TypePropValue.String;
                            shape.Props.Add(prop);
                        }
                    }
                }

                // Save the modified diagram to a new file
                string outputPath = Path.Combine(
                    Path.GetDirectoryName(visioPath) ?? string.Empty,
                    Path.GetFileNameWithoutExtension(visioPath) + "_Updated.vsdx");

                diagram.Save(outputPath, SaveFileFormat.Vsdx);
                Console.WriteLine($"Diagram saved successfully to: {outputPath}");
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}