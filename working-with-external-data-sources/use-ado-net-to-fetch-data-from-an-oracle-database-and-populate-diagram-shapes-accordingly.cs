using System;
using System.IO;
using System.Data;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Define input Visio template and output file paths
        string inputPath = "template.vsdx";
        if (!System.IO.File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }
        string outputPath = "output.vsdx";

        // Guard: ensure the template file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // -----------------------------------------------------------------
        // Simulate fetching data from an Oracle database.
        // In a real scenario you would use Oracle.ManagedDataAccess or another provider.
        // Example (commented out because external providers are not available):
        // using (var connection = new OracleConnection(connectionString))
        // {
        //     connection.Open();
        //     using (var command = new OracleCommand("SELECT Id, Name FROM Employees", connection))
        //     using (var adapter = new OracleDataAdapter(command))
        //     {
        //         DataTable dt = new DataTable();
        //         adapter.Fill(dt);
        //         // Use dt below
        //     }
        // }
        // -----------------------------------------------------------------

        // Create an in-memory DataTable with sample data
        DataTable dataTable = new DataTable();
        dataTable.Columns.Add("Id", typeof(int));
        dataTable.Columns.Add("Name", typeof(string));
        dataTable.Rows.Add(1, "Alice");
        dataTable.Rows.Add(2, "Bob");
        dataTable.Rows.Add(3, "Charlie");

        try
        {
            // Load the Visio template inside a using block as required
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Use the first page of the diagram
                Page page = diagram.Pages[0];

                // Starting coordinates for placing shapes
                double startX = 1.0;
                double startY = 1.0;
                double offsetX = 2.5; // horizontal spacing between shapes

                // Iterate over each data row and add a rectangle shape with the name as text
                for (int i = 0; i < dataTable.Rows.Count; i++)
                {
                    DataRow row = dataTable.Rows[i];
                    string shapeText = row["Name"].ToString();

                    // Calculate position for the current shape
                    double posX = startX + i * offsetX;
                    double posY = startY;

                    // Add a rectangle shape to the page.
                    // AddShape returns the shape ID (long), not a Shape object.
                    long shapeId = page.AddShape(posX, posY, "Rectangle", false);

                    // Retrieve the Shape instance using the returned ID
                    Shape shape = page.Shapes.GetShape(shapeId);

                    // Clear any existing text and set the shape's text to the employee name
                    shape.Text.Value.Clear();
                    shape.Text.Value.Add(new Txt(shapeText));

                    // Store the Id from the data row in a custom user-defined cell named "EmployeeId"
                    User userCell = new User
                    {
                        NameU = "EmployeeId",
                        Value = { Val = row["Id"].ToString() }
                    };
                    shape.Users.Add(userCell);
                }

                // Refresh the diagram to apply any changes related to data connections (not used here but good practice)
                diagram.Refresh();

                // Save the modified diagram to the output file using the correct overload
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
                Console.WriteLine($"Diagram saved successfully to {outputPath}");
            }
        }
        catch (Exception ex)
        {
            // Write any Aspose.Diagram errors to the error stream
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}