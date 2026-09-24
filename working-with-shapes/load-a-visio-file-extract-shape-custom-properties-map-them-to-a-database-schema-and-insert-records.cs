using System;
using System.Data;
using System.Collections.Generic;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
    {
        static void Main(string[] args)
        {
            try
            {

                // Input Visio file path (adjust as needed)
                string inputPath = args.Length > 0 ? args[0] : "input.vsdx";
                // Output Visio file path after processing (optional)
                string outputPath = "output.vsdx";

                // Load the Visio diagram
                Diagram diagram = new Diagram(inputPath);

                // Prepare an in‑memory DataTable to simulate database insertion
                DataTable shapePropsTable = new DataTable("ShapeProperties");
                shapePropsTable.Columns.Add("ShapeId", typeof(long));
                shapePropsTable.Columns.Add("ShapeName", typeof(string));
                shapePropsTable.Columns.Add("PropName", typeof(string));
                shapePropsTable.Columns.Add("PropValue", typeof(string));

                // Iterate through all pages and shapes to extract custom properties (Props)
                foreach (Page page in diagram.Pages)
                {
                    foreach (Shape shape in page.Shapes)
                    {
                        // Skip deleted shapes
                        if (shape.Del == BOOL.True)
                            continue;

                        // Ensure the shape has a Props collection
                        if (shape.Props == null)
                            continue;

                        foreach (Prop prop in shape.Props)
                        {
                            // Create a new DataRow for each custom property
                            DataRow row = shapePropsTable.NewRow();
                            row["ShapeId"] = shape.ID;
                            row["ShapeName"] = shape.NameU ?? string.Empty;
                            row["PropName"] = prop.Name ?? string.Empty;
                            row["PropValue"] = prop.Value?.Val ?? string.Empty;
                            shapePropsTable.Rows.Add(row);
                        }
                    }
                }

                // Simulated database insertion
                // In a real scenario you would use ADO.NET, e.g.:
                // using (SqlConnection conn = new SqlConnection(connectionString))
                // {
                //     conn.Open();
                //     foreach (DataRow dr in shapePropsTable.Rows)
                //     {
                //         using (SqlCommand cmd = new SqlCommand("INSERT INTO ShapeProperties (ShapeId, ShapeName, PropName, PropValue) VALUES (@ShapeId, @ShapeName, @PropName, @PropValue)", conn))
                //         {
                //             cmd.Parameters.AddWithValue("@ShapeId", dr["ShapeId"]);
                //             cmd.Parameters.AddWithValue("@ShapeName", dr["ShapeName"]);
                //             cmd.Parameters.AddWithValue("@PropName", dr["PropName"]);
                //             cmd.Parameters.AddWithValue("@PropValue", dr["PropValue"]);
                //             cmd.ExecuteNonQuery();
                //         }
                //     }
                // }

                // For demonstration, output the extracted data to the console
                Console.WriteLine("Extracted Shape Custom Properties:");
                foreach (DataRow dr in shapePropsTable.Rows)
                {
                    Console.WriteLine($"ShapeId: {dr["ShapeId"]}, ShapeName: {dr["ShapeName"]}, Property: {dr["PropName"]}, Value: {dr["PropValue"]}");
                }

                // Optionally, save the diagram (unchanged) to a new file
                diagram.Save(outputPath, SaveFileFormat.Vsdx);

            }
            catch (Aspose.Diagram.DiagramException ex)
            {
                Console.Error.WriteLine($"[DiagramException] {ex.Message}");
            }
    }
    }