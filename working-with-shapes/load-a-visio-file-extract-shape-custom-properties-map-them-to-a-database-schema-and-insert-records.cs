using System;
using System.IO;
using System.Data;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Validate input arguments
        if (args.Length == 0)
        {
            Console.Error.WriteLine("Please provide the path to the Visio file as the first argument.");
            return;
        }

        string visioPath = args[0];

        // Guard: ensure the Visio file exists
        if (!File.Exists(visioPath))
        {
            Console.Error.WriteLine($"Visio file not found: {visioPath}");
            return;
        }

        // Prepare an in‑memory DataTable that mimics a database table for custom properties
        DataTable customPropsTable = new DataTable("ShapeCustomProperties");
        customPropsTable.Columns.Add("ShapeId", typeof(long));
        customPropsTable.Columns.Add("ShapeName", typeof(string));
        customPropsTable.Columns.Add("PropertyName", typeof(string));
        customPropsTable.Columns.Add("PropertyValue", typeof(string));

        try
        {
            // Load the Visio diagram
            Diagram diagram = new Diagram(visioPath);

            // Iterate through all pages in the diagram
            for (int pageIndex = 0; pageIndex < diagram.Pages.Count; pageIndex++)
            {
                Page page = diagram.Pages[pageIndex];

                // Iterate through all shapes on the current page
                for (int shapeIndex = 0; shapeIndex < page.Shapes.Count; shapeIndex++)
                {
                    // Retrieve the shape by index
                    Shape shape = page.Shapes.GetShape(shapeIndex);
                    if (shape == null)
                        continue; // Safety check

                    long shapeId = shape.ID;
                    string shapeName = shape.NameU ?? string.Empty;

                    // Iterate through the custom properties collection (Props)
                    for (int propIndex = 0; propIndex < shape.Props.Count; propIndex++)
                    {
                        Prop prop = shape.Props[propIndex];
                        if (prop == null)
                            continue;

                        string propName = prop.Name ?? string.Empty;
                        string propValue = prop.Value?.Val ?? string.Empty;

                        // Add a row to the DataTable representing this custom property
                        DataRow row = customPropsTable.NewRow();
                        row["ShapeId"] = shapeId;
                        row["ShapeName"] = shapeName;
                        row["PropertyName"] = propName;
                        row["PropertyValue"] = propValue;
                        customPropsTable.Rows.Add(row);
                    }
                }
            }

            // Simulate database insertion
            // In a real scenario you would use ADO.NET (e.g., SqlConnection, SqlCommand) to insert the rows.
            // Example (commented out because external DB drivers are not available in this environment):
            /*
            using (var connection = new SqlConnection("your-connection-string"))
            {
                connection.Open();
                foreach (DataRow row in customPropsTable.Rows)
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

            // For demonstration, output the collected custom properties to the console
            Console.WriteLine("Extracted custom properties:");
            foreach (DataRow row in customPropsTable.Rows)
            {
                Console.WriteLine($"ShapeId: {row["ShapeId"]}, ShapeName: {row["ShapeName"]}, Property: {row["PropertyName"]}, Value: {row["PropertyValue"]}");
            }
        }
        catch (Exception ex)
        {
            // Log any errors that occur during processing
            Console.Error.WriteLine($"An error occurred while processing the Visio file: {ex.Message}");
        }
    }
}