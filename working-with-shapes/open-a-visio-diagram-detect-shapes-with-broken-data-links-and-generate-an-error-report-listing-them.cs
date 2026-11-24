using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Validate arguments
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <inputVisioPath> <outputReportPath>");
            return;
        }

        string inputPath = args[0];
        string reportPath = args[1];

        // Guard: check if input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Error: Input file '{inputPath}' does not exist.");
            return;
        }

        try
        {
            // Load the Visio diagram inside a using block as required
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Prepare the report writer
                using (StreamWriter writer = new StreamWriter(reportPath, false))
                {
                    writer.WriteLine("Broken Data Links Report");
                    writer.WriteLine($"Generated on: {DateTime.Now}");
                    writer.WriteLine($"Source Diagram: {Path.GetFileName(inputPath)}");
                    writer.WriteLine(new string('=', 50));

                    bool anyBroken = false;

                    // Iterate through all data connections in the diagram
                    for (int i = 0; i < diagram.DataConnections.Count; i++)
                    {
                        DataConnection dataConn = diagram.DataConnections[i];

                        // A data connection is considered broken if its ConnectionString or Command is null/empty
                        bool connStringEmpty = string.IsNullOrWhiteSpace(dataConn.ConnectionString);
                        bool commandEmpty = string.IsNullOrWhiteSpace(dataConn.Command);

                        if (connStringEmpty || commandEmpty)
                        {
                            anyBroken = true;
                            writer.WriteLine($"Data Connection #{i + 1} appears broken.");
                            writer.WriteLine($"  ConnectionString: {(connStringEmpty ? "<empty>" : dataConn.ConnectionString)}");
                            writer.WriteLine($"  Command: {(commandEmpty ? "<empty>" : dataConn.Command)}");
                            writer.WriteLine();
                        }
                    }

                    // If no broken data connections were found, note that in the report
                    if (!anyBroken)
                    {
                        writer.WriteLine("No broken data connections were detected in the diagram.");
                    }

                    writer.WriteLine(new string('=', 50));
                    writer.WriteLine("End of Report");
                }

                Console.WriteLine($"Report generated successfully at '{reportPath}'.");
            }
        }
        catch (Exception ex)
        {
            // Write any unexpected errors to the error stream
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}