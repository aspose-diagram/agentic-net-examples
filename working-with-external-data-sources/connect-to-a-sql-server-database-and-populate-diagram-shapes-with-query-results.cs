using System;
using System.Data;
using System.Data.SqlClient;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
    {
        static void Main()
        {
            try
            {

                // Path to a Visio template that contains the required masters (e.g., "Rectangle")
                string templatePath = "template.vsdx";

                // Load the diagram from the template file
                Diagram diagram = new Diagram(templatePath);

                // Get the first page where shapes will be added
                Page page = diagram.Pages[0];

                // -----------------------------------------------------------------
                // Real database connection (commented out for environments without SQL Server)
                // -----------------------------------------------------------------
                // string connectionString = "Data Source=SERVER_NAME;Initial Catalog=DATABASE_NAME;Integrated Security=True;";
                // string query = "SELECT Id, Name, Value FROM SampleTable";
                // DataTable dataTable = new DataTable();
                // using (SqlConnection conn = new SqlConnection(connectionString))
                // {
                //     using (SqlCommand cmd = new SqlCommand(query, conn))
                //     {
                //         conn.Open();
                //         using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                //         {
                //             adapter.Fill(dataTable);
                //         }
                //     }
                // }

                // -----------------------------------------------------------------
                // Simulated data for demonstration purposes
                // -----------------------------------------------------------------
                DataTable dataTable = new DataTable();
                dataTable.Columns.Add("Id", typeof(int));
                dataTable.Columns.Add("Name", typeof(string));
                dataTable.Columns.Add("Value", typeof(string));

                dataTable.Rows.Add(1, "Alpha", "100");
                dataTable.Rows.Add(2, "Beta", "200");
                dataTable.Rows.Add(3, "Gamma", "300");

                // Starting coordinates for shape placement
                double startX = 2.0;   // inches from left
                double startY = 2.0;   // inches from top
                double verticalSpacing = 1.5; // inches between shapes

                // Iterate over each data row and create a rectangle shape with text
                foreach (DataRow row in dataTable.Rows)
                {
                    // Add a rectangle shape using the master name "Rectangle"
                    // The fourth parameter (isCalculate) is set to false
                    long shapeId = page.AddShape(startX, startY, "Rectangle", false);

                    // Retrieve the shape object for further modifications
                    Shape shape = page.Shapes.GetShape(shapeId);

                    // Build the display text from the row data
                    string displayText = $"ID: {row["Id"]}\nName: {row["Name"]}\nValue: {row["Value"]}";

                    // Clear any existing text and add the new text run
                    shape.Text.Value.Clear();
                    shape.Text.Value.Add(new Txt(displayText));

                    // Optionally adjust shape size (width and height in inches)
                    shape.XForm.Width.Value = 2.5;
                    shape.XForm.Height.Value = 1.0;

                    // Move to the next vertical position for the following shape
                    startY += verticalSpacing;
                }

                // Save the populated diagram to a new file
                string outputPath = "output.vsdx";
                diagram.Save(outputPath, SaveFileFormat.Vsdx);

                Console.WriteLine($"Diagram saved to '{outputPath}'.");

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }