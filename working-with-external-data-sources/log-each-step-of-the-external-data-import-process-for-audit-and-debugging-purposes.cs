using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        // Paths for input and output diagrams
        string inputPath = "input.vsdx";
        // Guard to ensure the input file exists
        if (!File.Exists(inputPath)) { Console.Error.WriteLine($"File not found: {inputPath}"); return; }
        string outputPath = "output.vsdx";

        Console.WriteLine("=== External Data Import Process Started ===");

        try
        {
            // Step 1: Load the diagram
            Console.WriteLine($"Loading diagram from '{inputPath}'...");
            Diagram diagram = new Diagram(inputPath);
            Console.WriteLine("Diagram loaded successfully.");

            // Step 2: Log existing data connections (Name property is not available)
            Console.WriteLine($"Diagram contains {diagram.DataConnections.Count} data connection(s).");
            for (int i = 0; i < diagram.DataConnections.Count; i++)
            {
                var conn = diagram.DataConnections[i];
                // Log connection details without using the non‑existent Name property
                Console.WriteLine($"[Connection {i}] ConnectionString: '{conn.ConnectionString}', Command: '{conn.Command}'");
            }

            // Step 3: Update the first data connection (if any)
            if (diagram.DataConnections.Count > 0)
            {
                var conn = diagram.DataConnections[0];
                Console.WriteLine("Updating first data connection...");

                // Example new connection details
                string newConnectionString = "Data Source=MyServer;Initial Catalog=MyDB;Integrated Security=True";
                string newCommand = "SELECT * FROM MyTable";

                conn.ConnectionString = newConnectionString;
                conn.Command = newCommand;

                Console.WriteLine($"Updated ConnectionString to: '{conn.ConnectionString}'");
                Console.WriteLine($"Updated Command to: '{conn.Command}'");
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
            Console.WriteLine($"Saving updated diagram to '{outputPath}'...");
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine("Diagram saved successfully.");
        }
        catch (Exception ex)
        {
            // Log any errors that occur during processing
            Console.Error.WriteLine($"Error: {ex.Message}");
        }

        Console.WriteLine("=== External Data Import Process Completed ===");
    }
}