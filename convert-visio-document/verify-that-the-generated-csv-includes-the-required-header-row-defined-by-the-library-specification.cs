using System;
using System.IO;
using System.Linq;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Validate command‑line arguments.
        if (args == null || args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <inputVisioFile> <outputCsvFile>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

        // Guard: ensure the input Visio file exists.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file does not exist: {inputPath}");
            return;
        }

        // Perform the conversion from Visio to CSV.
        try
        {
            // Load the Visio diagram.
            Diagram diagram = new Diagram(inputPath);

            // Save the diagram as CSV.
            diagram.Save(outputPath, SaveFileFormat.Csv);
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during Visio to CSV conversion: {ex.Message}");
            return;
        }

        // Guard: ensure the CSV file was created.
        if (!File.Exists(outputPath))
        {
            Console.Error.WriteLine($"Failed to create CSV file: {outputPath}");
            return;
        }

        // Verify that the CSV contains the required header row.
        try
        {
            // Read the first line of the CSV file.
            string headerLine = File.ReadLines(outputPath).FirstOrDefault();

            if (headerLine == null)
            {
                Console.Error.WriteLine("CSV file is empty; no header row found.");
                return;
            }

            // Check for required header fields defined by the library specification.
            bool hasApplication = headerLine.Contains("Application", StringComparison.OrdinalIgnoreCase);
            bool hasCsvProducer = headerLine.Contains("CSV Producer", StringComparison.OrdinalIgnoreCase);

            if (hasApplication && hasCsvProducer)
            {
                Console.WriteLine("CSV header verification passed.");
            }
            else
            {
                Console.Error.WriteLine("CSV header verification failed.");
                Console.Error.WriteLine($"Header line content: {headerLine}");
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error while verifying CSV header: {ex.Message}");
        }
    }
}