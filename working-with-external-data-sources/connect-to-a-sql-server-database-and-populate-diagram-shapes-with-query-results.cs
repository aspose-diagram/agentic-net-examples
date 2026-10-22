using System;
using System.IO;
using System.Data;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Expect two arguments: input diagram path and output diagram path
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <inputDiagramPath> <outputDiagramPath>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

        // Guard against missing input file
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the diagram inside a using block as required
            using (Diagram diagram = new Diagram(inputPath))
            {
                // ------------------------------------------------------------
                // STEP 1: Update the first SQL Server data connection (if any)
                // ------------------------------------------------------------
                if (diagram.DataConnections.Count > 0)
                {
                    // Update connection string and command text.
                    // In a real scenario replace the placeholder values with actual ones.
                    diagram.DataConnections[0].ConnectionString = "Data Source=SERVER;Initial Catalog=Database;Integrated Security=True";
                    diagram.DataConnections[0].Command = "SELECT Name, Value FROM SampleTable";

                    // Refresh the diagram to pull data from the database.
                    diagram.Refresh();
                }
                else
                {
                    Console.WriteLine("No data connections found in the diagram. Skipping data refresh.");
                }

                // ------------------------------------------------------------
                // STEP 2: Simulate query results using an in‑memory DataTable
                // ------------------------------------------------------------
                DataTable simulatedResult = new DataTable();
                simulatedResult.Columns.Add("Name", typeof(string));
                simulatedResult.Columns.Add("Value", typeof(string));

                // Sample rows – replace with actual data retrieval logic as needed
                simulatedResult.Rows.Add("Alpha", "100");
                simulatedResult.Rows.Add("Beta", "200");
                simulatedResult.Rows.Add("Gamma", "300");

                // ------------------------------------------------------------
                // STEP 3: Populate shapes with the simulated data
                // ------------------------------------------------------------
                // For demonstration, assign each row's data to a shape on the first page.
                // If there are more shapes than rows, remaining shapes are left unchanged.
                Page firstPage = diagram.Pages[0];
                int rowIndex = 0;

                foreach (Shape shape in firstPage.Shapes)
                {
                    if (rowIndex >= simulatedResult.Rows.Count)
                        break; // No more data rows to assign

                    DataRow row = simulatedResult.Rows[rowIndex];

                    // Clear existing text and add new text in the required format
                    shape.Text.Value.Clear();
                    shape.Text.Value.Add(new Txt($"{row["Name"]}: {row["Value"]}"));

                    rowIndex++;
                }

                // ------------------------------------------------------------
                // STEP 4: Save the modified diagram
                // ------------------------------------------------------------
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
                Console.WriteLine($"Diagram saved successfully to {outputPath}");
            }
        }
        catch (Exception ex)
        {
            // Write any errors to the error stream
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}