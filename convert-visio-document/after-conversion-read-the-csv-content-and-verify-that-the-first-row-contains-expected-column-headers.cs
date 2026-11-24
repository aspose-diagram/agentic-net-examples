using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Validate command‑line arguments.
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <inputVisioPath> <outputCsvPath>");
            return;
        }

        string inputPath = args[0];
        string outputCsvPath = args[1];

        // Guard: ensure the input Visio file exists.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the Visio diagram.
            Diagram diagram = new Diagram(inputPath);

            // Ensure the output directory exists.
            string outputDir = Path.GetDirectoryName(outputCsvPath);
            if (!string.IsNullOrEmpty(outputDir) && !Directory.Exists(outputDir))
            {
                Directory.CreateDirectory(outputDir);
            }

            // Export the diagram to CSV format.
            diagram.Save(outputCsvPath, SaveFileFormat.Csv);
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during conversion: {ex.Message}");
            return;
        }

        // Verify that the CSV file was created.
        if (!File.Exists(outputCsvPath))
        {
            Console.Error.WriteLine($"CSV file was not created: {outputCsvPath}");
            return;
        }

        try
        {
            // Read all lines from the CSV file.
            string[] lines = File.ReadAllLines(outputCsvPath);

            if (lines.Length == 0)
            {
                Console.Error.WriteLine("CSV file is empty.");
                return;
            }

            // Expected column headers (adjust as needed for your scenario).
            string[] expectedHeaders = new string[] { "Application", "CSV Producer" };

            // Split the first line by commas to obtain actual headers.
            string[] actualHeaders = lines[0].Split(',');

            // Simple verification: check that each expected header appears in the actual header list.
            bool allHeadersPresent = true;
            foreach (string expected in expectedHeaders)
            {
                bool found = false;
                foreach (string actual in actualHeaders)
                {
                    if (string.Equals(actual.Trim(), expected, StringComparison.OrdinalIgnoreCase))
                    {
                        found = true;
                        break;
                    }
                }

                if (!found)
                {
                    Console.Error.WriteLine($"Missing expected header: '{expected}'");
                    allHeadersPresent = false;
                }
            }

            if (allHeadersPresent)
            {
                Console.WriteLine("CSV header verification succeeded. All expected columns are present.");
            }
            else
            {
                Console.Error.WriteLine("CSV header verification failed.");
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error while reading/verifying CSV: {ex.Message}");
        }
    }
}