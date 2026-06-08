using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Paths for input and output diagrams
        string inputPath = "input.vsdx";
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }
        string outputPath = "output.vsdx";

        Console.WriteLine("=== External Data Import Process Started ===");

        try
        {
            // Step 1: Load the diagram
            Console.WriteLine($"Loading diagram from '{inputPath}'...");
            Diagram diagram = new Diagram(inputPath);
            Console.WriteLine("Diagram loaded successfully.");

            // Step 2: Enumerate existing data connections
            Console.WriteLine($"Diagram contains {diagram.DataConnections.Count} data connection(s).");
            for (int i = 0; i < diagram.DataConnections.Count; i++)
            {
                var conn = diagram.DataConnections[i];
                Console.WriteLine($"[Connection {i}] ConnectionString: '{conn.ConnectionString}', Command: '{conn.Command}'");
            }

            // Step 3: Update a data connection (example updates the first one)
            if (diagram.DataConnections.Count > 0)
            {
                var conn = diagram.DataConnections[0];
                Console.WriteLine("Updating first data connection...");
                conn.ConnectionString = "Data Source=MyServer;Initial Catalog=MyDB;Integrated Security=True";
                conn.Command = "SELECT * FROM MyTable";
                Console.WriteLine($"Updated ConnectionString: '{conn.ConnectionString}'");
                Console.WriteLine($"Updated Command: '{conn.Command}'");
            }
            else
            {
                Console.WriteLine("No data connections found to update.");
            }

            // Step 4: Refresh the diagram to apply changes
            Console.WriteLine("Refreshing diagram to synchronize data record sets...");
            diagram.Refresh();
            Console.WriteLine("Diagram refreshed.");

            // Step 5: Save the updated diagram
            Console.WriteLine($"Saving updated diagram to '{outputPath}'...");
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine("Diagram saved successfully.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during processing: {ex.Message}");
        }

        Console.WriteLine("=== External Data Import Process Completed ===");
    }
}