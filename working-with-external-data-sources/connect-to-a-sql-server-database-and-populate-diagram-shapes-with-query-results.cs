using System;
using System.Data.SqlClient;
using Aspose.Diagram;

class Program
    {
        static void Main()
        {
            try
            {

                // Path to the Visio template or existing diagram file
                string diagramPath = "template.vsdx";

                // Load the diagram
                Diagram diagram = new Diagram(diagramPath);

                // Get the first page (index 0)
                Page page = diagram.Pages[0];

                // Database connection string (modify as needed)
                string connectionString = "Data Source=YOUR_SERVER;Initial Catalog=YOUR_DATABASE;Integrated Security=True";

                // SQL query to retrieve data
                string query = "SELECT Id, Name FROM YourTable";

                // Starting position for shapes
                double startX = 1.0; // inches from left
                double startY = 1.0; // inches from top
                double deltaY = 1.0; // vertical spacing between shapes

                int rowIndex = 0;

                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            // Calculate position for the current shape
                            double pinY = startY + rowIndex * deltaY;

                            // Add a rectangle shape; returns the shape ID (long)
                            long shapeId = page.AddShape(startX, pinY, "Rectangle");

                            // Retrieve the shape object using the ID
                            Shape shape = page.Shapes.GetShape(shapeId);

                            // Clear any existing text and add new text from the query result
                            shape.Text.Value.Clear();
                            string name = reader["Name"]?.ToString() ?? string.Empty;
                            shape.Text.Value.Add(new Txt(name));

                            // Store the Id value in Data1 (string property, assign directly)
                            shape.Data1 = reader["Id"]?.ToString() ?? string.Empty;

                            rowIndex++;
                        }
                    }
                }

                // Save the updated diagram
                string outputPath = "output.vsdx";
                diagram.Save(outputPath, SaveFileFormat.Vsdx);

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }