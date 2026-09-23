using System;
using System.Collections.Generic;
using System.Data;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
    {
        static void Main()
        {
            try
            {

                // Load an existing Visio diagram
                Diagram diagram = new Diagram("input.vsdx");

                // Simulate external data source (e.g., a database or CSV)
                // Here we create a DataTable with Category and Color columns
                DataTable categoryTable = new DataTable();
                categoryTable.Columns.Add("Category", typeof(string));
                categoryTable.Columns.Add("ColorHex", typeof(string));
                categoryTable.Rows.Add("Finance", "#FF5733");   // orange
                categoryTable.Rows.Add("HR", "#33FF57");       // green
                categoryTable.Rows.Add("IT", "#3357FF");       // blue

                // Build a lookup dictionary from the DataTable
                Dictionary<string, string> categoryColorMap = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
                foreach (DataRow row in categoryTable.Rows)
                {
                    string cat = row["Category"]?.ToString() ?? string.Empty;
                    string color = row["ColorHex"]?.ToString() ?? "#FFFFFF";
                    if (!string.IsNullOrEmpty(cat))
                    {
                        categoryColorMap[cat] = color;
                    }
                }

                // Iterate through all pages and shapes
                foreach (Page page in diagram.Pages)
                {
                    foreach (Shape shape in page.Shapes)
                    {
                        // Skip deleted shapes
                        if (shape.Del == BOOL.True)
                            continue;

                        // Ensure the shape has custom properties (Props) collection
                        if (shape.Props == null)
                            continue;

                        // Find a custom property named "Category"
                        string shapeCategory = null;
                        foreach (Prop prop in shape.Props)
                        {
                            if (prop.Name.Equals("Category", StringComparison.OrdinalIgnoreCase))
                            {
                                shapeCategory = prop.Value.Val;
                                break;
                            }
                        }

                        // If the shape has a category and a matching color, apply the fill
                        if (!string.IsNullOrEmpty(shapeCategory) && categoryColorMap.TryGetValue(shapeCategory, out string fillColor))
                        {
                            // Set solid fill pattern
                            shape.Fill.FillPattern.Value = 1; // Solid
                            // Apply the foreground fill color (hex string)
                            shape.Fill.FillForegnd.Value = fillColor;
                        }
                    }
                }

                // Save the modified diagram
                diagram.Save("output.vsdx", SaveFileFormat.Vsdx);

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }