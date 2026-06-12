using System;
using System.Data.SqlClient;
using Aspose.Diagram;

class Program
    {
        static void Main(string[] args)
        {
            try
            {

                // Path to the Visio file to be processed
                string visioPath = @"C:\Input\diagram.vsdx";

                // Database connection string (replace with actual values)
                string connectionString = @"Server=YOUR_SERVER;Database=YOUR_DATABASE;Trusted_Connection=True;";

                // Load the Visio diagram
                Diagram diagram = new Diagram(visioPath);

                // Open a SQL connection once and reuse it for all inserts
                using (SqlConnection sqlConnection = new SqlConnection(connectionString))
                {
                    sqlConnection.Open();

                    // Prepare an INSERT command with parameters
                    using (SqlCommand cmd = new SqlCommand(
                        "INSERT INTO ShapeCustomProperties (ShapeId, PropertyName, PropertyValue) VALUES (@ShapeId, @PropName, @PropValue)",
                        sqlConnection))
                    {
                        // Define parameters (will be reused)
                        SqlParameter shapeIdParam = cmd.Parameters.Add("@ShapeId", System.Data.SqlDbType.BigInt);
                        SqlParameter propNameParam = cmd.Parameters.Add("@PropName", System.Data.SqlDbType.NVarChar, 200);
                        SqlParameter propValueParam = cmd.Parameters.Add("@PropValue", System.Data.SqlDbType.NVarChar, -1);

                        // Iterate through all pages
                        foreach (Page page in diagram.Pages)
                        {
                            // Iterate through all shapes on the page
                            foreach (Shape shape in page.Shapes)
                            {
                                // Skip deleted shapes
                                if (shape.Del == BOOL.True)
                                    continue;

                                // Iterate through custom properties (Props) of the shape
                                foreach (Prop prop in shape.Props)
                                {
                                    // Retrieve property name and value
                                    string propName = prop.Name;
                                    string propValue = prop.Value.Val ?? string.Empty;

                                    // Assign parameter values
                                    shapeIdParam.Value = shape.ID;
                                    propNameParam.Value = propName;
                                    propValueParam.Value = propValue;

                                    // Execute the INSERT command
                                    cmd.ExecuteNonQuery();
                                }
                            }
                        }
                    }

                    sqlConnection.Close();
                }

                Console.WriteLine("Custom properties extraction and database insertion completed.");

            }
            catch (System.IO.DirectoryNotFoundException ex)
            {
                Console.Error.WriteLine($"[DirectoryNotFoundException] {ex.Message}");
            }
    }
    }