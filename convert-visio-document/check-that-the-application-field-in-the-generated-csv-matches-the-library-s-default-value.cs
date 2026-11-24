using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Validate arguments
        if (args.Length < 1)
        {
            Console.Error.WriteLine("Usage: Program <inputVisioFilePath> [outputCsvPath]");
            return;
        }

        string inputPath = args[0];
        // Guard: ensure the input Visio file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file does not exist: {inputPath}");
            return;
        }

        // Determine output CSV path (use provided or a temporary file)
        string outputCsvPath = args.Length >= 2 ? args[1] : Path.Combine(Path.GetTempPath(), $"output_{Guid.NewGuid()}.csv");

        try
        {
            // Load the Visio diagram
            Diagram diagram = new Diagram(inputPath);

            // Export the diagram to CSV format
            diagram.Save(outputCsvPath, SaveFileFormat.Csv);
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during diagram processing or CSV export: {ex.Message}");
            return;
        }

        // Verify that the CSV file was created
        if (!File.Exists(outputCsvPath))
        {
            Console.Error.WriteLine($"CSV file was not created: {outputCsvPath}");
            return;
        }

        try
        {
            // Read all lines from the generated CSV
            string[] lines = File.ReadAllLines(outputCsvPath);

            // Find the line that contains the Application field
            string applicationValue = null;
            foreach (string line in lines)
            {
                // Trim possible whitespace
                string trimmed = line.Trim();

                // CSV may use quotes; handle both quoted and unquoted formats
                if (trimmed.StartsWith("Application", StringComparison.OrdinalIgnoreCase) ||
                    trimmed.StartsWith("\"Application\"", StringComparison.OrdinalIgnoreCase))
                {
                    // Split by comma (first comma separates field name and value)
                    string[] parts = trimmed.Split(new[] { ',' }, 2);
                    if (parts.Length == 2)
                    {
                        // Remove surrounding quotes from the value if present
                        applicationValue = parts[1].Trim().Trim('\"');
                    }
                    break;
                }
            }

            // Expected default value as documented
            const string expectedValue = "Aspose.Diagram";

            if (applicationValue == null)
            {
                Console.Error.WriteLine("Application field not found in the CSV output.");
                return;
            }

            // Compare the actual value with the expected default
            if (applicationValue == expectedValue)
            {
                Console.WriteLine("Success: Application field matches the default value.");
            }
            else
            {
                throw new Exception($"Application field mismatch. Expected '{expectedValue}', but found '{applicationValue}'.");
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during CSV validation: {ex.Message}");
        }
    }
}