using System;
using System.IO;
using System.Data;
using System.Diagnostics;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Validate command line arguments.
        if (args.Length == 0)
        {
            Console.Error.WriteLine("Usage: Program <VisioFilePath>");
            return;
        }

        string diagramPath = args[0];

        // Guard: ensure the input Visio file exists.
        if (!File.Exists(diagramPath))
        {
            Console.Error.WriteLine($"Error: File not found - {diagramPath}");
            return;
        }

        try
        {
            // Load the Visio diagram inside a using block to ensure proper disposal.
            using (Diagram diagram = new Diagram(diagramPath))
            {
                // Capture memory usage before loading the external dataset.
                Process currentProcess = Process.GetCurrentProcess();
                long memoryBeforeProcess = currentProcess.PrivateMemorySize64;
                long memoryBeforeGC = GC.GetTotalMemory(true);

                Console.WriteLine("Starting large dataset simulation...");

                // Simulate loading a large external dataset into memory.
                DataTable largeTable = new DataTable("LargeData");

                // Define columns (10 string columns).
                for (int col = 0; col < 10; col++)
                {
                    largeTable.Columns.Add($"Col{col}", typeof(string));
                }

                // Populate the table with a large number of rows (e.g., 100,000 rows).
                for (int row = 0; row < 100_000; row++)
                {
                    DataRow dataRow = largeTable.NewRow();
                    for (int col = 0; col < 10; col++)
                    {
                        dataRow[col] = $"Row{row}_Col{col}";
                    }
                    largeTable.Rows.Add(dataRow);
                }

                // Note: In a real scenario, you would bind this DataTable to a DataConnection
                // in the diagram. This example focuses on memory profiling only.

                // Capture memory usage after the dataset has been loaded.
                long memoryAfterProcess = currentProcess.PrivateMemorySize64;
                long memoryAfterGC = GC.GetTotalMemory(false);

                // Output memory usage statistics.
                Console.WriteLine($"Process Private Memory Before: {memoryBeforeProcess / (1024 * 1024)} MB");
                Console.WriteLine($"Process Private Memory After : {memoryAfterProcess / (1024 * 1024)} MB");
                Console.WriteLine($"GC Total Memory Before      : {memoryBeforeGC / (1024 * 1024)} MB");
                Console.WriteLine($"GC Total Memory After       : {memoryAfterGC / (1024 * 1024)} MB");

                // Save the diagram to verify that it can still be saved after the dataset load.
                string outputDirectory = Path.GetDirectoryName(diagramPath) ?? Directory.GetCurrentDirectory();
                string outputPath = Path.Combine(outputDirectory, "output.vsdx");

                // Ensure the output directory exists.
                if (!Directory.Exists(outputDirectory))
                {
                    Console.Error.WriteLine($"Error: Output directory does not exist - {outputDirectory}");
                    return;
                }

                // Save the diagram using the correct overload with SaveFileFormat.
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
                Console.WriteLine($"Diagram saved to: {outputPath}");
            }
        }
        catch (Exception ex)
        {
            // Write any errors to the error stream.
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}