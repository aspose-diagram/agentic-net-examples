using System.IO;
using System;
using Aspose.Diagram;
using System.Data.SqlClient;

class Program
{
    static void Main()
    {
        try
        {

            // Path to the Visio file
            string visioPath = "input.vsdx";

            // Load the diagram
            Diagram diagram = new Diagram(visioPath);

            // Database connection string (replace with actual values)
            string connectionString = "Data Source=SERVER;Initial Catalog=Database;Integrated Security=True";

            // Insert custom property data into the database
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string insertSql = @"INSERT INTO ShapeCustomProperties (ShapeId, ShapeName, PropertyName, PropertyValue)
                                     VALUES (@ShapeId, @ShapeName, @PropName, @PropValue)";

                using (SqlCommand command = new SqlCommand(insertSql, connection))
                {
                    // Define parameters
                    command.Parameters.Add("@ShapeId", System.Data.SqlDbType.BigInt);
                    command.Parameters.Add("@ShapeName", System.Data.SqlDbType.NVarChar, 255);
                    command.Parameters.Add("@PropName", System.Data.SqlDbType.NVarChar, 255);
                    command.Parameters.Add("@PropValue", System.Data.SqlDbType.NVarChar, -1); // -1 = MAX

                    // Iterate through pages
                    foreach (Page page in diagram.Pages)
                    {
                        // Iterate through shapes on the page
                        foreach (Shape shape in page.Shapes)
                        {
                            // Skip shapes that are marked as deleted
                            if (shape.Del == BOOL.True)
                                continue;

                            long shapeId = shape.ID;
                            string shapeName = shape.Name ?? string.Empty;

                            // Iterate through custom properties (Props)
                            foreach (Prop prop in shape.Props)
                            {
                                string propName = prop.Name ?? string.Empty;
                                string propValue = prop.Value != null ? prop.Value.Val : string.Empty;

                                // Set parameter values
                                command.Parameters["@ShapeId"].Value = shapeId;
                                command.Parameters["@ShapeName"].Value = shapeName;
                                command.Parameters["@PropName"].Value = propName;
                                command.Parameters["@PropValue"].Value = propValue;

                                // Execute the insert
                                command.ExecuteNonQuery();
                            }
                        }
                    }
                }
            }

            Console.WriteLine("Custom properties have been exported to the database.");

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
