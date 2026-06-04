using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
    {
        static void Main(string[] args)
        {
            try
            {

                // Input Visio file path
                string inputPath = "input.vsdx";
                // Output CSV file path
                string outputCsv = "output.csv";

                // Load the diagram (assuming VSDX format)
                Diagram diagram = new Diagram(inputPath, LoadFileFormat.Vsdx);

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
                                // Prop.Label.Value holds the property name
                                if (!string.IsNullOrWhiteSpace(prop.Label.Value))
                                {
                                    customPropNames.Add(prop.Label.Value);
                                }
                            }
                        }
                    }
                }

                // Prepare header columns: ShapeID, ShapeName, then custom properties
                List<string> headerColumns = new List<string>
                {
                    "ShapeID",
                    "ShapeName"
                };
                headerColumns.AddRange(customPropNames);

                // Write CSV
                using (StreamWriter writer = new StreamWriter(outputCsv))
                {
                    // Write header
                    writer.WriteLine(string.Join(",", headerColumns));

                    // Write rows for each shape
                    foreach (Page page in diagram.Pages)
                    {
                        foreach (Shape shape in page.Shapes)
                        {
                            List<string> row = new List<string>
                            {
                                shape.ID.ToString(),
                                shape.Name ?? string.Empty
                            };

                            // Build a dictionary of this shape's custom properties for quick lookup
                            Dictionary<string, string> shapeProps = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
                            if (shape.Props != null)
                            {
                                foreach (Prop prop in shape.Props)
                                {
                                    string propName = prop.Label.Value;
                                    string propValue = prop.Value.Val ?? string.Empty;
                                    if (!string.IsNullOrWhiteSpace(propName))
                                    {
                                        shapeProps[propName] = propValue;
                                    }
                                }
                            }

                            // Add values for each header custom property (empty if not present)
                            foreach (string propName in customPropNames)
                            {
                                if (shapeProps.TryGetValue(propName, out string value))
                                {
                                    // Escape commas and quotes
                                    string escaped = value.Replace("\"", "\"\"");
                                    if (escaped.Contains(",") || escaped.Contains("\""))
                                    {
                                        escaped = $"\"{escaped}\"";
                                    }
                                    row.Add(escaped);
                                }
                                else
                                {
                                    row.Add(string.Empty);
                                }
                            }

                            writer.WriteLine(string.Join(",", row));
                        }
                    }
                }

                // Optionally, also save the diagram as CSV using built‑in export (without custom columns)
                // diagram.Save("basic_output.csv", SaveFileFormat.Csv);

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }