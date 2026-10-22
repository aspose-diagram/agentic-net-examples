using System;
using System.IO;
using System.Data;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Expect two arguments: path to Visio diagram and path to a large dataset file (e.g., CSV)
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <VisioDiagramPath> <DatasetPath>");
            return;
        }

        string diagramPath = args[0];
        string datasetPath = args[1];

        // Guard against missing files
        if (!File.Exists(diagramPath))
        {
            Console.Error.WriteLine($"Diagram file not found: {diagramPath}");
            return;
        }

        if (!File.Exists(datasetPath))
        {
            Console.Error.WriteLine($"Dataset file not found: {datasetPath}");
            return;
        }

        // Record initial memory usage
        long memoryBefore = GC.GetTotalMemory(true);
        Console.WriteLine($"Initial memory (bytes): {memoryBefore}");

        // Load the Visio diagram inside a using block as required
        try
        {
            using (Diagram diagram = new Diagram(diagramPath))
            {
                Console.WriteLine("Diagram loaded successfully.");

                // Record memory after diagram load
                long memoryAfterDiagram = GC.GetTotalMemory(true);
                Console.WriteLine($"Memory after diagram load (bytes): {memoryAfterDiagram}");
                Console.WriteLine($"Diagram load memory increase (bytes): {memoryAfterDiagram - memoryBefore}");

                // Save the diagram to a temporary file to ensure full processing
                string tempOutputPath = Path.Combine(Path.GetTempPath(), "temp_output.vsdx");
                try
                {
                    diagram.Save(tempOutputPath, SaveFileFormat.Vsdx);
                    Console.WriteLine($"Diagram saved to temporary file: {tempOutputPath}");
                }
                catch (Exception ex)
                {
                    Console.Error.WriteLine($"Error saving temporary diagram: {ex.Message}");
                }

                // Load the large external dataset (simulated by reading all lines into a DataTable)
                DataTable dataTable = new DataTable("ExternalData");
                try
                {
                    using (var reader = new StreamReader(datasetPath))
                    {
                        // Assume first line contains column headers separated by commas
                        string headerLine = reader.ReadLine();
                        if (headerLine == null)
                        {
                            Console.Error.WriteLine("Dataset file is empty.");
                            return;
                        }

                        string[] headers = headerLine.Split(',');
                        foreach (string header in headers)
                        {
                            dataTable.Columns.Add(header.Trim(), typeof(string));
                        }

                        // Read remaining lines and add rows
                        string line;
                        while ((line = reader.ReadLine()) != null)
                        {
                            string[] fields = line.Split(',');
                            DataRow row = dataTable.NewRow();
                            for (int i = 0; i < headers.Length && i < fields.Length; i++)
                            {
                                row[i] = fields[i].Trim();
                            }
                            dataTable.Rows.Add(row);
                        }
                    }

                    Console.WriteLine($"Dataset loaded: {dataTable.Rows.Count} rows, {dataTable.Columns.Count} columns.");
                }
                catch (Exception ex)
                {
                    Console.Error.WriteLine($"Error loading dataset: {ex.Message}");
                    return;
                }

                // Record memory after dataset load
                long memoryAfterDataset = GC.GetTotalMemory(true);
                Console.WriteLine($"Memory after dataset load (bytes): {memoryAfterDataset}");
                Console.WriteLine($"Dataset load memory increase (bytes): {memoryAfterDataset - memoryAfterDiagram}");

                // Link the dataset to the diagram (simplified)
                try
                {
                    // Ensure there is at least one data connection slot
                    if (diagram.DataConnections.Count == 0)
                    {
                        // Add a new data connection (placeholder values)
                        var conn = new DataConnection();
                        conn.ConnectionString = $"Provider=Microsoft.ACE.OLEDB.12.0;Data Source={datasetPath};Extended Properties=\"text;HDR=YES;FMT=Delimited\";";
                        conn.Command = $"SELECT * FROM [{Path.GetFileName(datasetPath)}]";
                        // IsEnabled property does not exist; omit it
                        diagram.DataConnections.Add(conn);
                        Console.WriteLine("Data connection added to diagram.");
                    }
                    else
                    {
                        // Update the first existing connection to point to the new dataset
                        var conn = diagram.DataConnections[0];
                        conn.ConnectionString = $"Provider=Microsoft.ACE.OLEDB.12.0;Data Source={datasetPath};Extended Properties=\"text;HDR=YES;FMT=Delimited\";";
                        conn.Command = $"SELECT * FROM [{Path.GetFileName(datasetPath)}]";
                        Console.WriteLine("Existing data connection updated.");
                    }

                    // Refresh the diagram to pull data from the external source
                    diagram.Refresh();
                    Console.WriteLine("Diagram refreshed with external data.");
                }
                catch (Exception ex)
                {
                    Console.Error.WriteLine($"Error linking dataset to diagram: {ex.Message}");
                }

                // Record final memory usage
                long finalMemory = GC.GetTotalMemory(true);
                Console.WriteLine($"Final memory (bytes): {finalMemory}");
                Console.WriteLine($"Total memory increase (bytes): {finalMemory - memoryBefore}");

                // Clean up temporary file
                try
                {
                    if (File.Exists(tempOutputPath))
                    {
                        File.Delete(tempOutputPath);
                        Console.WriteLine("Temporary file deleted.");
                    }
                }
                catch (Exception ex)
                {
                    Console.Error.WriteLine($"Error deleting temporary file: {ex.Message}");
                }
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error loading diagram: {ex.Message}");
            return;
        }
    }
}