using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main()
    {
        // Path to the source Visio file
        string inputPath = "input.vsdx";
        // Guard to ensure the input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Path to the output Visio file after processing
        string outputPath = "output.vsdx";

        Console.WriteLine("=== External Data Import Process Started ===");

        Diagram diagram = null;
        try
        {
            // Step 1: Load the diagram
            Console.WriteLine("Loading diagram from: " + inputPath);
            diagram = new Diagram(inputPath);
            Console.WriteLine("Diagram loaded successfully. Pages: " + diagram.Pages.Count);
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine("Error loading diagram: " + ex.Message);
            return;
        }

        // Step 2: List existing external data connections
        Console.WriteLine("Enumerating external data connections...");
        if (diagram.DataConnections.Count == 0)
        {
            Console.WriteLine("No external data connections found.");
        }
        else
        {
            for (int i = 0; i < diagram.DataConnections.Count; i++)
            {
                var conn = diagram.DataConnections[i];
                Console.WriteLine($"Connection {i}:");
                // DataConnection does not expose a Name property; omit it
                Console.WriteLine($"  ConnectionString: {conn.ConnectionString}");
                Console.WriteLine($"  Command: {conn.Command}");
            }
        }

        // Step 3: Update a specific data connection (example: first connection)
        if (diagram.DataConnections.Count > 0)
        {
            int targetIndex = 0; // modify as needed
            var targetConn = diagram.DataConnections[targetIndex];

            Console.WriteLine($"Updating connection {targetIndex}...");

            // Example new connection details
            string newConnectionString = "Data Source=MyServer;Initial Catalog=MyDatabase;Integrated Security=True";
            string newCommand = "SELECT * FROM MyTable";

            // Apply new connection details
            targetConn.ConnectionString = newConnectionString;
            targetConn.Command = newCommand;

            Console.WriteLine("New ConnectionString set to: " + targetConn.ConnectionString);
            Console.WriteLine("New Command set to: " + targetConn.Command);
        }
        else
        {
            Console.WriteLine("No connections to update.");
        }

        // Step 4: Refresh the diagram to import data from the updated source
        try
        {
            Console.WriteLine("Refreshing diagram to import external data...");
            diagram.Refresh();
            Console.WriteLine("Refresh completed.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine("Error during refresh: " + ex.Message);
            return;
        }

        // Step 5: Save the updated diagram
        try
        {
            Console.WriteLine("Saving updated diagram to: " + outputPath);
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine("Diagram saved successfully.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine("Error saving diagram: " + ex.Message);
            return;
        }

        Console.WriteLine("=== External Data Import Process Completed ===");
    }
}