using System;
using System.IO;
using System.Data;
using System.Collections.Generic;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Validate command‑line arguments.
        if (args.Length < 1)
        {
            Console.Error.WriteLine("Usage: Program <VisioFilePath>");
            return;
        }

        string visioPath = args[0];

        // Guard: ensure the Visio file exists.
        if (!File.Exists(visioPath))
        {
            Console.Error.WriteLine($"Error: File not found - {visioPath}");
            return;
        }

        try
        {
            // Load the Visio diagram.
            Diagram diagram = new Diagram(visioPath);

            // Prepare an in‑memory DataTable that mimics a database table.
            DataTable shapePropsTable = new DataTable("ShapeProperties");
            shapePropsTable.Columns.Add("ShapeId", typeof(long));
            shapePropsTable.Columns.Add("ShapeName", typeof(string));
            shapePropsTable.Columns.Add("PropName", typeof(string));
            shapePropsTable.Columns.Add("PropValue", typeof(string));

            // Iterate through all pages.
            for (int pageIndex = 0; pageIndex < diagram.Pages.Count; pageIndex++)
            {
                Page page = diagram.Pages[pageIndex];

                // Iterate through all shapes on the current page.
                for (int shapeIndex = 0; shapeIndex < page.Shapes.Count; shapeIndex++)
                {
                    Shape shape = page.Shapes.GetShape(shapeIndex);
                    if (shape == null)
                        continue; // Safety check.

                    // Retrieve the shape's name (U‑name) if available.
                    string shapeName = shape.NameU ?? string.Empty;

                    // Iterate through the shape's custom properties (Props collection).
                    foreach (Prop prop in shape.Props)
                    {
                        // Property name.
                        string propName = prop.Name ?? string.Empty;

                        // Property value – the .Val field holds the actual string.
                        string propValue = prop.Value?.Val ?? string.Empty;

                        // Add a row to the DataTable.
                        DataRow row = shapePropsTable.NewRow();
                        row["ShapeId"] = shape.ID;
                        row["ShapeName"] = shapeName;
                        row["PropName"] = propName;
                        row["PropValue"] = propValue;
                        shapePropsTable.Rows.Add(row);
                    }
                }
            }

            // Simulate inserting each record into a database.
            // In a real scenario you would use ADO.NET (e.g., SqlConnection, SqlCommand) here.
            // Example (commented out because external DB drivers are not available):
            /*
            using (var connection = new System.Data.SqlClient.SqlConnection(connectionString))
            {
                connection.Open();
                foreach (DataRow row in shapePropsTable.Rows)
                {
                    using (var command = new System.Data.SqlClient.SqlCommand(
                        "INSERT INTO ShapeProperties (ShapeId, ShapeName, PropName, PropValue) VALUES (@ShapeId, @ShapeName, @PropName, @PropValue)", connection))
                    {
                        command.Parameters.AddWithValue("@ShapeId", (long)row["ShapeId"]);
                        command.Parameters.AddWithValue("@ShapeName", (string)row["ShapeName"]);
                        command.Parameters.AddWithValue("@PropName", (string)row["PropName"]);
                        command.Parameters.AddWithValue("@PropValue", (string)row["PropValue"]);
                        command.ExecuteNonQuery();
                    }
                }
            }
            */

            // For demonstration, write the rows to the console.
            Console.WriteLine("Extracted shape custom properties:");
            foreach (DataRow row in shapePropsTable.Rows)
            {
                Console.WriteLine($"ShapeId={row["ShapeId"]}, ShapeName={row["ShapeName"]}, PropName={row["PropName"]}, PropValue={row["PropValue"]}");
            }
        }
        catch (Exception ex)
        {
            // Report any errors that occur during processing.
            Console.Error.WriteLine($"Error processing Visio file: {ex.Message}");
        }
    }
}