using System;
using System.Data;
using System.Collections.Generic;
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

                // Simulate external data source (e.g., from a database)
                DataTable externalData = GetExternalData();

                // Build a lookup dictionary: shape name -> value
                Dictionary<string, double> shapeValues = new Dictionary<string, double>(StringComparer.OrdinalIgnoreCase);
                foreach (DataRow row in externalData.Rows)
                {
                    string shapeName = row["ShapeName"]?.ToString() ?? string.Empty;
                    if (double.TryParse(row["Value"]?.ToString(), out double val) && !string.IsNullOrEmpty(shapeName))
                    {
                        shapeValues[shapeName] = val;
                    }
                }

                // Iterate through all pages and shapes
                foreach (Page page in diagram.Pages)
                {
                    // Collect shape IDs first to avoid modification during enumeration
                    List<long> shapeIds = new List<long>();
                    foreach (Shape shape in page.Shapes)
                    {
                        shapeIds.Add(shape.ID);
                    }

                    foreach (long shapeId in shapeIds)
                    {
                        Shape shape = page.Shapes.GetShape(shapeId);

                        // Skip deleted shapes
                        if (shape.Del == BOOL.True)
                            continue;

                        // Use the universal name (NameU) to match external data
                        string shapeName = shape.NameU;
                        if (shapeValues.TryGetValue(shapeName, out double value))
                        {
                            // Apply conditional fill color based on thresholds
                            if (value > 100)
                            {
                                // Red fill for high values
                                shape.Fill.FillForegnd.Value = "#FF0000";
                            }
                            else if (value > 50)
                            {
                                // Orange fill for medium values
                                shape.Fill.FillForegnd.Value = "#FFA500";
                            }
                            else
                            {
                                // Green fill for low values
                                shape.Fill.FillForegnd.Value = "#00FF00";
                            }
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

        // Helper method to create a sample DataTable representing external data
        private static DataTable GetExternalData()
        {
            DataTable table = new DataTable();
            table.Columns.Add("ShapeName", typeof(string));
            table.Columns.Add("Value", typeof(double));

            // Sample rows – replace with real data retrieval logic as needed
            table.Rows.Add("Process", 120);
            table.Rows.Add("Decision", 75);
            table.Rows.Add("Start", 30);
            table.Rows.Add("End", 55);

            return table;
        }
    }