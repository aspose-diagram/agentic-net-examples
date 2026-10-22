using System;
using System.IO;
using System.Data;
using System.Collections.Generic;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Input Visio file path (first argument or default)
        string visioPath = args.Length > 0 ? args[0] : "input.vsdx";

        // Guard: ensure the file exists before proceeding
        if (!File.Exists(visioPath))
        {
            Console.Error.WriteLine($"Error: Visio file not found at path '{visioPath}'.");
            return;
        }

        // Load the Visio diagram inside a try/catch to handle any Aspose exceptions
        Diagram diagram;
        try
        {
            diagram = new Diagram(visioPath);
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error loading Visio diagram: {ex.Message}");
            return;
        }

        // Prepare an in‑memory DataTable to simulate a database table
        DataTable table = new DataTable("ShapeCustomProperties");
        table.Columns.Add("ShapeId", typeof(long));
        table.Columns.Add("ShapeName", typeof(string));
        table.Columns.Add("PropertyName", typeof(string));
        table.Columns.Add("PropertyValue", typeof(string));

        // Iterate through all pages, shapes, and their custom properties
        foreach (Page page in diagram.Pages)
        {
            foreach (Shape shape in page.Shapes)
            {
                // Shape identifier and name (if available)
                long shapeId = shape.ID;
                string shapeName = shape.NameU ?? string.Empty;

                // Iterate over the shape's custom properties collection
                foreach (Prop prop in shape.Props)
                {
                    // Property name (as defined by the user)
                    string propName = prop.Name ?? string.Empty;

                    // Property value stored in the Value cell (use .Val)
                    string propValue = prop.Value?.Val ?? string.Empty;

                    // Add a row to the simulated table
                    DataRow row = table.NewRow();
                    row["ShapeId"] = shapeId;
                    row["ShapeName"] = shapeName;
                    row["PropertyName"] = propName;
                    row["PropertyValue"] = propValue;
                    table.Rows.Add(row);
                }
            }
        }

        // Simulate inserting rows into an external database.
        // In a real scenario you would use ADO.NET (e.g., SqlConnection, SqlCommand) like this:
        /*
        using (var connection = new SqlConnection("your-connection-string"))
        {
            connection.Open();
            foreach (DataRow row in table.Rows)
            {
                using (var command = new SqlCommand(
                    "INSERT INTO ShapeCustomProperties (ShapeId, ShapeName, PropertyName, PropertyValue) VALUES (@ShapeId, @ShapeName, @PropertyName, @PropertyValue)", connection))
                {
                    command.Parameters.AddWithValue("@ShapeId", (long)row["ShapeId"]);
                    command.Parameters.AddWithValue("@ShapeName", (string)row["ShapeName"]);
                    command.Parameters.AddWithValue("@PropertyName", (string)row["PropertyName"]);
                    command.Parameters.AddWithValue("@PropertyValue", (string)row["PropertyValue"]);
                    command.ExecuteNonQuery();
                }
            }
        }
        */

        // For demonstration, output the extracted data to the console
        Console.WriteLine("Extracted Shape Custom Properties:");
        foreach (DataRow row in table.Rows)
        {
            Console.WriteLine($"ShapeId: {row["ShapeId"]}, ShapeName: {row["ShapeName"]}, Property: {row["PropertyName"]}, Value: {row["PropertyValue"]}");
        }

        // Optionally, save the DataTable to a CSV file for verification
        string csvPath = "ExtractedProperties.csv";
        try
        {
            using (var writer = new StreamWriter(csvPath))
            {
                // Write header
                writer.WriteLine("ShapeId,ShapeName,PropertyName,PropertyValue");
                // Write rows
                foreach (DataRow row in table.Rows)
                {
                    string line = $"{row["ShapeId"]},{EscapeCsv((string)row["ShapeName"])},{EscapeCsv((string)row["PropertyName"])},{EscapeCsv((string)row["PropertyValue"])}";
                    writer.WriteLine(line);
                }
            }
            Console.WriteLine($"Data exported to CSV file: {csvPath}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error writing CSV file: {ex.Message}");
        }
    }

    // Helper method to escape CSV fields containing commas or quotes
    private static string EscapeCsv(string field)
    {
        if (field == null)
            return string.Empty;

        if (field.Contains(",") || field.Contains("\"") || field.Contains("\n"))
        {
            string escaped = field.Replace("\"", "\"\"");
            return $"\"{escaped}\"";
        }
        return field;
    }
}