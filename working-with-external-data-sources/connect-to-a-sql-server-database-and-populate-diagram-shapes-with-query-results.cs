using System;
using System.IO;
using System.Data;
using System.Collections.Generic;
using Aspose.Diagram;
using Aspose.Diagram.Manipulation;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Input Visio file path
        string inputPath = "input.vsdx";
        if (!System.IO.File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Output Visio file path
        string outputPath = "output.vsdx";

        try
        {
            // Load the diagram inside a using block
            using (Diagram diagram = new Diagram(inputPath))
            {
                // ------------------------------------------------------------
                // Simulate a SQL Server query result using an in‑memory DataTable.
                // In a real scenario you would use ADO.NET like:
                //   using (SqlConnection conn = new SqlConnection(connectionString))
                //   {
                //       conn.Open();
                //       using (SqlCommand cmd = new SqlCommand(query, conn))
                //       using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                //       {
                //           DataTable dt = new DataTable();
                //           da.Fill(dt);
                //           // use dt below
                //       }
                //   }
                // ------------------------------------------------------------
                DataTable queryResult = GetSampleData();

                // Choose the first page (index 0) for adding shapes
                int pageIndex = 0;
                Page page = diagram.Pages[pageIndex];

                // Positioning variables for new shapes
                double startX = 1.0;
                double startY = 1.0;
                double offsetY = 2.0;

                // Keep track of created shape IDs for later connection
                List<long> createdShapeIds = new List<long>();

                // Iterate over each row and create a rectangle shape with the row data
                foreach (DataRow row in queryResult.Rows)
                {
                    string name = row["Name"].ToString();

                    // Add a rectangle shape; diagram.AddShape returns the shape ID (long)
                    long shapeId = diagram.AddShape(startX, startY, "Rectangle", pageIndex);

                    // Retrieve the shape object to modify its properties
                    Shape shape = page.Shapes.GetShape(shapeId);

                    // Clear any existing text and set the shape's text to the value from the query
                    shape.Text.Value.Clear();
                    shape.Text.Value.Add(new Txt(name));

                    // Store the shape ID for later connector creation
                    createdShapeIds.Add(shapeId);

                    // Move down for the next shape
                    startY += offsetY;
                }

                // If more than one shape was created, connect them sequentially
                if (createdShapeIds.Count > 1)
                {
                    for (int i = 0; i < createdShapeIds.Count - 1; i++)
                    {
                        // Add a dynamic connector shape
                        long connectorId = diagram.AddShape(0, 0, "Dynamic connector", pageIndex);
                        Shape connector = page.Shapes.GetShape(connectorId);

                        // Connect the current shape to the next shape using the connector
                        page.ConnectShapesViaConnector(
                            createdShapeIds[i],
                            ConnectionPointPlace.Bottom,
                            createdShapeIds[i + 1],
                            ConnectionPointPlace.Top,
                            connectorId);
                    }
                }

                // Save the modified diagram to the output file (VSDX format)
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
            }
        }
        catch (Exception ex)
        {
            // Write any errors to the error stream
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }

    // Helper method to create a sample DataTable mimicking a SQL query result
    static DataTable GetSampleData()
    {
        DataTable table = new DataTable();
        table.Columns.Add("Name", typeof(string));
        table.Rows.Add("Alice");
        table.Rows.Add("Bob");
        table.Rows.Add("Charlie");
        return table;
    }
}