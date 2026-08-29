using System;
using System.Data.SqlClient;
using Aspose.Diagram;

class Program
    {
        static void Main()
        {
            try
            {

                // Path to the Visio file to be processed
                string visioPath = @"C:\Input\Diagram.vsdx";

                // Connection string to the target database
                string connectionString = @"Server=YOUR_SERVER;Database=YOUR_DATABASE;Trusted_Connection=True;";

                // Open the Visio diagram
                using (Diagram diagram = new Diagram(visioPath))
                {
                    // Iterate through each page in the diagram
                    foreach (Page page in diagram.Pages)
                    {
                        // Iterate through each shape on the current page
                        foreach (Shape shape in page.Shapes)
                        {
                            // Skip shapes that are marked as deleted
                            if (shape.Del == BOOL.True)
                                continue;

                            // Retrieve the shape's unique identifier and name
                            long shapeId = shape.ID;
                            string shapeName = shape.Name ?? string.Empty;

                            // Iterate through the custom properties (Props) of the shape
                            foreach (Prop prop in shape.Props)
                            {
                                string propertyName = prop.Name ?? string.Empty;
                                string propertyValue = prop.Value?.Val ?? string.Empty;

                                // Insert the extracted data into the database
                                InsertShapeProperty(connectionString, shapeId, shapeName, propertyName, propertyValue);
                            }
                        }
                    }
                }

            }
            catch (System.IO.DirectoryNotFoundException ex)
            {
                Console.Error.WriteLine($"[DirectoryNotFoundException] {ex.Message}");
            }
    }

        /// <summary>
        /// Inserts a single shape property record into the external database.
        /// </summary>
        static void InsertShapeProperty(string connString, long shapeId, string shapeName, string propName, string propValue)
        {
            const string insertSql = @"
                INSERT INTO ShapeProperties (ShapeId, ShapeName, PropertyName, PropertyValue)
                VALUES (@ShapeId, @ShapeName, @PropertyName, @PropertyValue);";

            using (SqlConnection conn = new SqlConnection(connString))
            using (SqlCommand cmd = new SqlCommand(insertSql, conn))
            {
                cmd.Parameters.AddWithValue("@ShapeId", shapeId);
                cmd.Parameters.AddWithValue("@ShapeName", shapeName);
                cmd.Parameters.AddWithValue("@PropertyName", propName);
                cmd.Parameters.AddWithValue("@PropertyValue", propValue);

                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }
    }