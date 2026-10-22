using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Determine the input Visio file path
        string inputPath;
        if (args.Length > 0)
        {
            inputPath = args[0];
        }
        else
        {
            Console.Write("Enter the path to the Visio diagram: ");
            inputPath = Console.ReadLine();
        }

        // Guard: ensure the file exists before proceeding
        if (string.IsNullOrWhiteSpace(inputPath) || !File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Error: File not found - '{inputPath}'.");
            return;
        }

        // Prepare the path for the error report file (same folder as the diagram)
        string reportPath = Path.Combine(Path.GetDirectoryName(inputPath) ?? string.Empty, "error_report.txt");

        try
        {
            // Load the Visio diagram inside a using block as required by the API
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Header for the report
                string reportContent = $"Error Report for '{Path.GetFileName(inputPath)}'{Environment.NewLine}";
                reportContent += $"Generated on: {DateTime.Now:u}{Environment.NewLine}{Environment.NewLine}";

                bool anyBroken = false;

                // Iterate over all data connections defined in the diagram
                for (int i = 0; i < diagram.DataConnections.Count; i++)
                {
                    DataConnection conn = diagram.DataConnections[i];

                    // A data connection is considered broken if its ConnectionString or Command is missing/empty
                    bool isBroken = string.IsNullOrWhiteSpace(conn.ConnectionString) ||
                                    string.IsNullOrWhiteSpace(conn.Command);

                    if (isBroken)
                    {
                        anyBroken = true;
                        reportContent += $"Data Connection #{i + 1}:{Environment.NewLine}";
                        reportContent += $"  ConnectionString: {(string.IsNullOrWhiteSpace(conn.ConnectionString) ? "<missing>" : conn.ConnectionString)}{Environment.NewLine}";
                        reportContent += $"  Command: {(string.IsNullOrWhiteSpace(conn.Command) ? "<missing>" : conn.Command)}{Environment.NewLine}";
                        reportContent += Environment.NewLine;
                    }
                }

                if (!anyBroken)
                {
                    reportContent += "No broken data connections were detected." + Environment.NewLine;
                }

                // Output the report to the console
                Console.WriteLine(reportContent);

                // Attempt to write the report to a file
                try
                {
                    File.WriteAllText(reportPath, reportContent);
                    Console.WriteLine($"Error report saved to: {reportPath}");
                }
                catch (Exception ex)
                {
                    Console.Error.WriteLine($"Failed to write report file: {ex.Message}");
                }
            }
        }
        catch (Exception ex)
        {
            // Capture any Aspose.Diagram related errors
            Console.Error.WriteLine($"An error occurred while processing the diagram: {ex.Message}");
        }
    }
}