using System;
using System.IO;
using System.Collections.Generic;
using System.Data.SqlClient;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        // Path to the Visio file to be processed
        string visioPath = "input.vsdx";

        // Guard: ensure the Visio file exists before proceeding
        if (!File.Exists(visioPath))
        {
            Console.Error.WriteLine($"File not found: {visioPath}");
            return;
        }

        // Database connection string (adjust as needed)
        string connectionString = "Data Source=SERVER;Initial Catalog=Database;Integrated Security=True";

        // Collection to hold extracted custom property records (use long for ShapeId)
        var records = new List<(long ShapeId, string PropertyName, string PropertyValue)>();

        try
        {
            // Load the Visio diagram
            Diagram diagram = new Diagram(visioPath);

            // Iterate through all pages and shapes
            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    // Skip shapes that are marked as deleted
                    if (shape.Del == BOOL.True)
                        continue;

                    // Shape.ID is a long; store it accordingly
                    long shapeId = shape.ID;

                    // Extract custom properties (Props) from the shape
                    if (shape.Props != null)
                    {
                        foreach (Prop prop in shape.Props)
                        {
                            string name = prop.Name;
                            string value = prop.Value.Val ?? string.Empty;

                            // Add a tuple representing the property record
                            records.Add((shapeId, name, value));
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
            // Report any errors that occur during diagram processing
            Console.Error.WriteLine($"Error processing Visio file: {ex.Message}");
            return;
        }

        // Insert the extracted properties into the database
        try
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                foreach (var rec in records)
                {
                    string sql = "INSERT INTO CustomProperties (ShapeId, PropertyName, PropertyValue) VALUES (@ShapeId, @Name, @Value)";
                    using (SqlCommand cmd = new SqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@ShapeId", rec.ShapeId);
                        cmd.Parameters.AddWithValue("@Name", rec.PropertyName);
                        cmd.Parameters.AddWithValue("@Value", rec.PropertyValue);

                        int rowsAffected = cmd.ExecuteNonQuery();
                        if (rowsAffected != 1)
                        {
                            Console.WriteLine($"Failed to insert property '{rec.PropertyName}' for shape ID {rec.ShapeId}");
                        }
                    }
                }

                conn.Close();
            }
        }
        catch (Exception ex)
        {
            // Report any database-related errors
            Console.Error.WriteLine($"Database error: {ex.Message}");
            return;
        }

        Console.WriteLine("Custom property extraction and database insertion completed.");
    }
}