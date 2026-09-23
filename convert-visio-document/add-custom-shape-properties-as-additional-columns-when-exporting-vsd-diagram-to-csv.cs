using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        try
        {

            // Path to the source Visio file
            string inputPath = "input.vsdx";
            // Path to the output CSV file
            string outputPath = "output.csv";

            // Load the diagram
            Diagram diagram = new Diagram(inputPath);

            // Collect all unique custom property names across all shapes
            HashSet<string> customPropNames = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    if (shape.Props != null)
                    {
                        foreach (Prop prop in shape.Props)
                        {
                            if (!string.IsNullOrWhiteSpace(prop.Name))
                            {
                                customPropNames.Add(prop.Name);
                            }
                        }
                    }
                }
            }

            // Prepare CSV header
            List<string> headerColumns = new List<string>
            {
                "ShapeID",
                "ShapeNameU",
                "MasterName"
            };
            headerColumns.AddRange(customPropNames);

            // Write CSV
            using (StreamWriter writer = new StreamWriter(outputPath))
            {
                // Write header line
                writer.WriteLine(string.Join(",", headerColumns));

                // Write data rows
                foreach (Page page in diagram.Pages)
                {
                    foreach (Shape shape in page.Shapes)
                    {
                        List<string> row = new List<string>
                        {
                            shape.ID.ToString(),
                            EscapeCsv(shape.NameU),
                            shape.Master != null ? EscapeCsv(shape.Master.Name) : ""
                        };

                        // Add custom property values in the same order as headerColumns
                        foreach (string propName in customPropNames)
                        {
                            string value = "";
                            if (shape.Props != null)
                            {
                                foreach (Prop prop in shape.Props)
                                {
                                    if (string.Equals(prop.Name, propName, StringComparison.OrdinalIgnoreCase))
                                    {
                                        value = prop.Value?.Val ?? "";
                                        break;
                                    }
                                }
                            }
                            row.Add(EscapeCsv(value));
                        }

                        writer.WriteLine(string.Join(",", row));
                    }
                }
            }

            Console.WriteLine($"CSV export completed: {outputPath}");

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }

    // Helper to escape commas and quotes in CSV fields
    private static string EscapeCsv(string field)
    {
        if (field == null)
            return "";

        if (field.Contains("\""))
            field = field.Replace("\"", "\"\"");

        if (field.Contains(",") || field.Contains("\"") || field.Contains("\n") || field.Contains("\r"))
            field = $"\"{field}\"";

        return field;
    }
}
