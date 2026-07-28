using System;
using System.IO;
using System.Data.SqlClient;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Input Visio file path (first argument or default)
        string visioPath = args.Length > 0 ? args[0] : "input.vsdx";
        // Verify the Visio file exists before attempting to load
        if (!File.Exists(visioPath))
        {
            Console.Error.WriteLine($"File not found: {visioPath}");
            return;
        }

        // Database connection string (second argument or placeholder)
        string connectionString = args.Length > 1 ? args[1] : "Server=YOUR_SERVER;Database=YOUR_DB;Trusted_Connection=True;";

        // Load the Visio diagram
        Diagram diagram;
        try
        {
            diagram = new Diagram(visioPath);
        }
        catch (Exception ex)
        {
            throw new Exception($"Failed to load Visio file '{visioPath}': {ex.Message}");
        }

        // Prepare SQL insert command
        const string insertSql = @"
            INSERT INTO ShapeProperties (ShapeId, ShapeName, PropName, PropValue)
            VALUES (@ShapeId, @ShapeName, @PropName, @PropValue)";

        using (SqlConnection conn = new SqlConnection(connectionString))
        {
            conn.Open();

            using (SqlCommand cmd = new SqlCommand(insertSql, conn))
            {
                // Define parameters once
                cmd.Parameters.Add("@ShapeId", System.Data.SqlDbType.BigInt);
                cmd.Parameters.Add("@ShapeName", System.Data.SqlDbType.NVarChar, 255);
                cmd.Parameters.Add("@PropName", System.Data.SqlDbType.NVarChar, 255);
                cmd.Parameters.Add("@PropValue", System.Data.SqlDbType.NVarChar, -1); // -1 = MAX

                // Iterate through all pages and shapes
                foreach (Page page in diagram.Pages)
                {
                    foreach (Shape shape in page.Shapes)
                    {
                        // Ensure the shape has custom properties (Props collection)
                        if (shape.Props != null && shape.Props.Count > 0)
                        {
                            foreach (Prop prop in shape.Props)
                            {
                                // Retrieve property name and value
                                string propName = prop.Name;
                                string propValue = prop.Value?.Val ?? string.Empty;

                                // Set parameter values
                                cmd.Parameters["@ShapeId"].Value = shape.ID;
                                cmd.Parameters["@ShapeName"].Value = shape.Name ?? string.Empty;
                                cmd.Parameters["@PropName"].Value = propName ?? string.Empty;
                                cmd.Parameters["@PropValue"].Value = propValue;

                                // Execute insert
                                try
                                {
                                    cmd.ExecuteNonQuery();
                                }
                                catch (Exception dbEx)
                                {
                                    // Log database insertion errors
                                    Console.WriteLine($"Failed to insert property for shape ID {shape.ID}: {dbEx.Message}");
                                }
                            }
                        }
                    }
                }
            }

            conn.Close();
        }

        Console.WriteLine("Custom property extraction and database insertion completed.");
    }
}