using System;
using System.IO;
using System.Data;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Path to the Visio file (adjust as needed)
        string visioPath = "input.vsdx";
        if (!System.IO.File.Exists(visioPath))
        {
            Console.Error.WriteLine($"File not found: {visioPath}");
            return;
        }

        // Guard: ensure the file exists before proceeding
        if (!File.Exists(visioPath))
        {
            Console.Error.WriteLine($"Error: File not found - {visioPath}");
            return;
        }

        // Prepare an in‑memory DataTable to simulate a database table
        DataTable shapePropsTable = new DataTable("ShapeProperties");
        shapePropsTable.Columns.Add("PageName", typeof(string));
        shapePropsTable.Columns.Add("ShapeID", typeof(long));
        shapePropsTable.Columns.Add("ShapeName", typeof(string));
        shapePropsTable.Columns.Add("PropName", typeof(string));
        shapePropsTable.Columns.Add("PropLabel", typeof(string));
        shapePropsTable.Columns.Add("PropValue", typeof(string));

        try
        {
            // Load the Visio diagram
            Diagram diagram = new Diagram(visioPath);

            // Iterate through each page in the diagram
            foreach (Page page in diagram.Pages)
            {
                // Iterate through each shape on the current page
                foreach (Shape shape in page.Shapes)
                {
                    // Skip deleted shapes
                    if (shape.Del == BOOL.True)
                        continue;

                    // Retrieve shape name (if any)
                    string shapeName = shape.NameU ?? string.Empty;

                    // Iterate through custom properties (Props collection)
                    foreach (Prop prop in shape.Props)
                    {
                        // Extract property details
                        string propName = prop.Name ?? string.Empty;
                        string propLabel = prop.Label?.Value ?? string.Empty;
                        string propValue = prop.Value?.Val ?? string.Empty;

                        // Add a row to the simulated database table
                        DataRow row = shapePropsTable.NewRow();
                        row["PageName"] = page.NameU ?? string.Empty;
                        row["ShapeID"] = shape.ID;
                        row["ShapeName"] = shapeName;
                        row["PropName"] = propName;
                        row["PropLabel"] = propLabel;
                        row["PropValue"] = propValue;
                        shapePropsTable.Rows.Add(row);
                    }
                }
            }

            // Simulated database operation: display the collected data
            Console.WriteLine("Extracted Shape Custom Properties:");
            foreach (DataRow dr in shapePropsTable.Rows)
            {
                Console.WriteLine($"Page: {dr["PageName"]}, ShapeID: {dr["ShapeID"]}, ShapeName: {dr["ShapeName"]}, " +
                                  $"PropName: {dr["PropName"]}, Label: {dr["PropLabel"]}, Value: {dr["PropValue"]}");
            }

            // -----------------------------------------------------------------
            // Real database insertion example (commented out because external
            // providers are not available in this environment):
            //
            // using (var connection = new System.Data.SqlClient.SqlConnection(connectionString))
            // {
            //     connection.Open();
            //     foreach (DataRow dr in shapePropsTable.Rows)
            //     {
            //         using (var command = new System.Data.SqlClient.SqlCommand(
            //             "INSERT INTO ShapeProperties (PageName, ShapeID, ShapeName, PropName, PropLabel, PropValue) " +
            //             "VALUES (@PageName, @ShapeID, @ShapeName, @PropName, @PropLabel, @PropValue)", connection))
            //         {
            //             command.Parameters.AddWithValue("@PageName", dr["PageName"]);
            //             command.Parameters.AddWithValue("@ShapeID", dr["ShapeID"]);
            //             command.Parameters.AddWithValue("@ShapeName", dr["ShapeName"]);
            //             command.Parameters.AddWithValue("@PropName", dr["PropName"]);
            //             command.Parameters.AddWithValue("@PropLabel", dr["PropLabel"]);
            //             command.Parameters.AddWithValue("@PropValue", dr["PropValue"]);
            //             command.ExecuteNonQuery();
            //         }
            //     }
            // }
            // -----------------------------------------------------------------
        }
        catch (Exception ex)
        {
            // Log any errors that occur during processing
            Console.Error.WriteLine($"Error processing Visio file: {ex.Message}");
        }
    }
}