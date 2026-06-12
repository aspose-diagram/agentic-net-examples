using System;
using System.Data;
using System.Data.SqlClient;
using Aspose.Diagram;

class Program
    {
        static void Main(string[] args)
        {
            try
            {

                // Path to the Visio file to be processed
                string visioFilePath = "input.vsdx";

                // Connection string for the target database
                string connectionString = "Data Source=SERVER_NAME;Initial Catalog=DatabaseName;Integrated Security=True";

                // Load the Visio diagram
                Diagram diagram = new Diagram(visioFilePath);

                // Open a database connection
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    // Prepare the INSERT command (parameterized to avoid SQL injection)
                    using (SqlCommand command = new SqlCommand(
                        "INSERT INTO ShapeProperties (ShapeId, PropName, PropValue) VALUES (@ShapeId, @PropName, @PropValue)",
                        connection))
                    {
                        // Define parameters with appropriate SQL types
                        SqlParameter shapeIdParam = command.Parameters.Add("@ShapeId", SqlDbType.BigInt);
                        SqlParameter propNameParam = command.Parameters.Add("@PropName", SqlDbType.NVarChar, 255);
                        SqlParameter propValueParam = command.Parameters.Add("@PropValue", SqlDbType.NVarChar, -1); // -1 = MAX

                        // Iterate through all pages in the diagram
                        foreach (Page page in diagram.Pages)
                        {
                            // Iterate through all shapes on the current page
                            foreach (Shape shape in page.Shapes)
                            {
                                // Ensure the shape has a Props collection
                                if (shape.Props != null)
                                {
                                    // Iterate through each custom property (Prop) of the shape
                                    foreach (Prop prop in shape.Props)
                                    {
                                        // Set parameter values for the current record
                                        shapeIdParam.Value = shape.ID;
                                        propNameParam.Value = prop.Name;
                                        propValueParam.Value = prop.Value.Val ?? string.Empty;

                                        // Execute the INSERT command
                                        command.ExecuteNonQuery();
                                    }
                                }
                            }
                        }
                    }

                    connection.Close();
                }

                // Optionally, save the diagram if any modifications were made (not required for extraction)
                // diagram.Save("output.vsdx", SaveFileFormat.Vsdx);

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }