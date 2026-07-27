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

                // Input Visio file path
                string inputPath = "input.vsdx";
                // Output CSV file path
                string outputCsv = "output.csv";

                // Load the diagram
                Diagram diagram = new Diagram(inputPath);

                // Collect all distinct custom property names from all shapes
                HashSet<string> customPropNames = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
                foreach (Page page in diagram.Pages)
                {
                    foreach (Shape shape in page.Shapes)
                    {
                        if (shape.Props != null)
                        {
                            foreach (Prop prop in shape.Props)
                            {
                                if (!string.IsNullOrEmpty(prop.Name))
                                    customPropNames.Add(prop.Name);
                            }
                        }
                    }
                }

                // Prepare header columns: ShapeID, Name, NameU + custom properties
                List<string> headers = new List<string> { "ShapeID", "Name", "NameU" };
                headers.AddRange(customPropNames);

                // Write CSV
                using (StreamWriter writer = new StreamWriter(outputCsv))
                {
                    // Write header line
                    writer.WriteLine(string.Join(",", EscapeCsvList(headers)));

                    // Write data rows
                    foreach (Page page in diagram.Pages)
                    {
                        foreach (Shape shape in page.Shapes)
                        {
                            List<string> row = new List<string>
                            {
                                shape.ID.ToString(),
                                shape.Name ?? string.Empty,
                                shape.NameU ?? string.Empty
                            };

                            // Build a dictionary for quick lookup of this shape's custom properties
                            Dictionary<string, string> shapeProps = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
                            if (shape.Props != null)
                            {
                                foreach (Prop prop in shape.Props)
                                {
                                    if (!string.IsNullOrEmpty(prop.Name))
                                        shapeProps[prop.Name] = prop.Value?.Val ?? string.Empty;
                                }
                            }

                            // Add values for each custom property column
                            foreach (string propName in customPropNames)
                            {
                                shapeProps.TryGetValue(propName, out string value);
                                row.Add(value ?? string.Empty);
                            }

                            writer.WriteLine(string.Join(",", EscapeCsvList(row)));
                        }
                    }
                }

                Console.WriteLine($"CSV export completed: {outputCsv}");

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }

        // Escapes a list of strings for CSV output
        private static IEnumerable<string> EscapeCsvList(IEnumerable<string> values)
        {
            foreach (var v in values)
                yield return EscapeCsv(v);
        }

        // Escapes a single CSV field according to RFC 4180
        private static string EscapeCsv(string field)
        {
            if (field == null)
                return string.Empty;

            bool mustQuote = field.Contains(",") || field.Contains("\"") || field.Contains("\r") || field.Contains("\n");
            if (mustQuote)
            {
                string escaped = field.Replace("\"", "\"\"");
                return $"\"{escaped}\"";
            }
            else
            {
                return field;
            }
        }
    }