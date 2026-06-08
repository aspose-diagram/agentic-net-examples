using System;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Oracle connection details – replace with actual values
        string connectionString = "User Id=myUser;Password=myPassword;Data Source=MyOracleDB";

        // Query to fetch data – adjust column names as needed
        string query = "SELECT ID, NAME FROM MY_TABLE";

        // Create a new empty diagram
        Diagram diagram;
        try
        {
            diagram = new Diagram();
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Aspose error creating diagram: {ex.Message}");
            return;
        }

        // Ensure there is at least one page
        Page page;
        try
        {
            page = diagram.Pages[0];
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Aspose error accessing page: {ex.Message}");
            return;
        }

        // Positioning variables for shape placement
        double startX = 2.0;   // inches from left
        double startY = 2.0;   // inches from top
        double verticalSpacing = 1.5; // inches between shapes

        try
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand(query, conn))
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        // Retrieve column values
                        string id = reader["ID"].ToString();
                        string name = reader["NAME"].ToString();

                        try
                        {
                            // Add a rectangle shape for each record
                            // Using the built‑in "Rectangle" master with default size
                            long shapeId = page.AddShape(startX, startY, 2.0, 1.0, "Rectangle");

                            // Retrieve the shape object
                            Shape shape = page.Shapes.GetShape(shapeId);

                            // Set shape text to display the fetched data
                            shape.Text.Value.Clear();
                            shape.Text.Value.Add(new Txt($"ID: {id}\nName: {name}"));

                            // Optionally store the ID in a custom user cell
                            User userCell = new User();
                            userCell.Name = "RecordID";
                            userCell.Value.Val = id;
                            shape.Users.Add(userCell);
                        }
                        catch (Exception ex)
                        {
                            Console.Error.WriteLine($"Aspose error processing record ID {id}: {ex.Message}");
                        }

                        // Move to next vertical position
                        startY += verticalSpacing;
                    }
                }
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error accessing database: {ex.Message}");
            return;
        }

        // Save the diagram to VSDX format
        string outputPath = "OutputDiagram.vsdx";
        try
        {
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Diagram saved to {outputPath}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Aspose error saving diagram: {ex.Message}");
        }
    }
}