using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Expect two arguments: input Visio file path and output CSV file path.
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <inputVisioPath> <outputCsvPath>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

        // Guard: verify input file exists.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file does not exist: {inputPath}");
            return;
        }

        try
        {
            // Load the Visio diagram.
            Diagram diagram = new Diagram(inputPath);

            // Export the diagram to CSV format.
            diagram.Save(outputPath, SaveFileFormat.Csv);
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during diagram processing or CSV export: {ex.Message}");
            return;
        }

        // Guard: verify CSV was created.
        if (!File.Exists(outputPath))
        {
            Console.Error.WriteLine($"CSV export failed, file not found: {outputPath}");
            return;
        }

        try
        {
            // Read all lines from the generated CSV.
            string[] csvLines = File.ReadAllLines(outputPath);

            // Flags to track required header presence.
            bool hasApplicationHeader = false;
            bool hasCsvProducerHeader = false;

            // Scan each line for the required header fields.
            foreach (string line in csvLines)
            {
                // Trim leading/trailing whitespace for reliable comparison.
                string trimmed = line.Trim();

                // Check for Application field containing 'Aspose.Diagram'.
                if (trimmed.StartsWith("Application", StringComparison.OrdinalIgnoreCase) &&
                    trimmed.IndexOf("Aspose.Diagram", StringComparison.OrdinalIgnoreCase) >= 0)
                {
                    hasApplicationHeader = true;
                }

                // Check for CSV Producer field containing 'Aspose.Diagram'.
                if (trimmed.StartsWith("CSV Producer", StringComparison.OrdinalIgnoreCase) &&
                    trimmed.IndexOf("Aspose.Diagram", StringComparison.OrdinalIgnoreCase) >= 0)
                {
                    hasCsvProducerHeader = true;
                }

                // If both headers are found, no need to continue scanning.
                if (hasApplicationHeader && hasCsvProducerHeader)
                    break;
            }

            // Report verification result.
            if (hasApplicationHeader && hasCsvProducerHeader)
            {
                Console.WriteLine("CSV verification succeeded: required header rows are present.");
            }
            else
            {
                Console.Error.WriteLine("CSV verification failed: required header rows are missing.");
                if (!hasApplicationHeader)
                    Console.Error.WriteLine("- Missing or incorrect 'Application' header.");
                if (!hasCsvProducerHeader)
                    Console.Error.WriteLine("- Missing or incorrect 'CSV Producer' header.");
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error while reading or verifying CSV file: {ex.Message}");
        }
    }
}