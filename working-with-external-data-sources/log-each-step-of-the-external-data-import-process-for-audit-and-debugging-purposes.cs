using System.IO;
using System;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        try
        {

            // Paths (adjust as needed)
            string inputPath = "input.vsdx";
            string outputPath = "output.vsdx";

            // Step 1: Load the diagram
            Console.WriteLine("Loading diagram from: " + inputPath);
            Diagram diagram = new Diagram(inputPath);
            Console.WriteLine("Diagram loaded successfully.");

            // Step 2: Log existing data connections
            Console.WriteLine("Existing Data Connections:");
            for (int i = 0; i < diagram.DataConnections.Count; i++)
            {
                var conn = diagram.DataConnections[i];
                Console.WriteLine($"  [{i}] ConnectionString = '{conn.ConnectionString}', Command = '{conn.Command}'");
            }

            // Step 3: Update the first data connection (example)
            if (diagram.DataConnections.Count > 0)
            {
                Console.WriteLine("Updating first data connection...");
                diagram.DataConnections[0].ConnectionString = "Data Source=MyServer;Initial Catalog=MyDatabase;Integrated Security=True";
                diagram.DataConnections[0].Command = "SELECT * FROM MyTable";
                Console.WriteLine("First data connection updated.");
            }
            else
            {
                Console.WriteLine("No data connections found to update.");
            }

            // Step 4: Refresh the diagram to apply changes
            Console.WriteLine("Refreshing diagram to synchronize data record sets...");
            diagram.Refresh();
            Console.WriteLine("Refresh completed.");

            // Step 5: Save the updated diagram
            Console.WriteLine("Saving updated diagram to: " + outputPath);
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine("Diagram saved successfully.");

            // Cleanup
            diagram.Dispose();
            Console.WriteLine("Process completed.");

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
