using System;
using System.Data;
using Aspose.Diagram;

class Program
    {
        static void Main(string[] args)
        {
            try
            {

                // Path to the Visio file
                string visioPath = "input.vsdx";

                // Load the diagram
                Diagram diagram = new Diagram(visioPath);

                // Prepare an in‑memory DataTable to simulate the external database table
                DataTable customPropsTable = new DataTable("ShapeCustomProperties");
                customPropsTable.Columns.Add("ShapeId", typeof(long));
                customPropsTable.Columns.Add("ShapeName", typeof(string));
                customPropsTable.Columns.Add("PropertyName", typeof(string));
                customPropsTable.Columns.Add("PropertyValue", typeof(string));

                // Iterate through all pages, shapes and their custom properties (Props)
                foreach (Page page in diagram.Pages)
                {
                    foreach (Shape shape in page.Shapes)
                    {
                        // Skip deleted shapes
                        if (shape.Del == BOOL.True)
                            continue;

                        foreach (Prop prop in shape.Props)
                        {
                            // Each Prop contains Name, Label, Value, etc.
                            DataRow row = customPropsTable.NewRow();
                            row["ShapeId"] = shape.ID;
                            row["ShapeName"] = shape.NameU;
                            row["PropertyName"] = prop.Name;
                            row["PropertyValue"] = prop.Value.Val;
                            customPropsTable.Rows.Add(row);
                        }
                    }
                }

                // Example: display extracted data on the console
                Console.WriteLine("Extracted custom properties:");
                foreach (DataRow dr in customPropsTable.Rows)
                {
                    Console.WriteLine($"ShapeId: {dr["ShapeId"]}, ShapeName: {dr["ShapeName"]}, " +
                                      $"Property: {dr["PropertyName"]}, Value: {dr["PropertyValue"]}");
                }

                // -----------------------------------------------------------------
                // The following commented block shows how you would insert the data
                // into a real database using ADO.NET. Replace the placeholder connection
                // string and table/column names with your actual schema.
                // -----------------------------------------------------------------
                /*
                string connectionString = "your_connection_string_here";
                using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(connectionString))
                {
                    connection.Open();
                    foreach (DataRow dr in customPropsTable.Rows)
                    {
                        using (IDbCommand command = connection.CreateCommand())
                        {
                            command.CommandText = @"
                                INSERT INTO ShapeCustomProperties (ShapeId, ShapeName, PropertyName, PropertyValue)
                                VALUES (@ShapeId, @ShapeName, @PropertyName, @PropertyValue)";

                            var paramShapeId = command.CreateParameter();
                            paramShapeId.ParameterName = "@ShapeId";
                            paramShapeId.Value = dr["ShapeId"];
                            command.Parameters.Add(paramShapeId);

                            var paramShapeName = command.CreateParameter();
                            paramShapeName.ParameterName = "@ShapeName";
                            paramShapeName.Value = dr["ShapeName"];
                            command.Parameters.Add(paramShapeName);

                            var paramPropName = command.CreateParameter();
                            paramPropName.ParameterName = "@PropertyName";
                            paramPropName.Value = dr["PropertyName"];
                            command.Parameters.Add(paramPropName);

                            var paramPropValue = command.CreateParameter();
                            paramPropValue.ParameterName = "@PropertyValue";
                            paramPropValue.Value = dr["PropertyValue"];
                            command.Parameters.Add(paramPropValue);

                            command.ExecuteNonQuery();
                        }
                    }
                }
                */

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }