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
        string outputPath = args[1];

        // Guard: ensure the input Visio file exists.
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
            Console.Error.WriteLine($"Error during diagram processing: {ex.Message}");
            return;
        }

        // Guard: ensure the CSV file was created.
        if (!File.Exists(outputPath))
        {
            Console.Error.WriteLine($"Failed to create CSV file: {outputPath}");
            return;
        }

        // Expected default value for the Application field.
        const string expectedApplication = "Aspose.Diagram";

        try
        {
            // Read all lines from the generated CSV.
            string[] lines = File.ReadAllLines(outputPath);
            string actualApplication = null;

            // Locate the line that starts with "Application".
            foreach (string line in lines)
            {
                // Trim leading spaces to avoid false negatives.
                string trimmedLine = line.TrimStart();

                if (trimmedLine.StartsWith("Application", StringComparison.OrdinalIgnoreCase))
                {
                    // Split by comma – the first part is the field name, the second is the value.
                    string[] parts = trimmedLine.Split(new[] { ',' }, 2);
                    if (parts.Length == 2)
                    {
                        actualApplication = parts[1].Trim();
                    }
                    break;
                }
            }

            // Verify that the Application field was found.
            if (actualApplication == null)
            {
                Console.Error.WriteLine("Application field not found in the CSV output.");
                return;
            }

            // Compare the actual value with the expected default.
            if (string.Equals(actualApplication, expectedApplication, StringComparison.Ordinal))
            {
                Console.WriteLine($"Success: Application field matches expected value \"{expectedApplication}\".");
            }
            else
            {
                throw new Exception($"Application field mismatch. Expected \"{expectedApplication}\", but found \"{actualApplication}\".");
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error while validating CSV: {ex.Message}");
        }
    }
}