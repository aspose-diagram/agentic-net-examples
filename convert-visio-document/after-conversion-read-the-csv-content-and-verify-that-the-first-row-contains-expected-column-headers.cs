using System;
using System.IO;
using System.Linq;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Validate arguments: expect input Visio file path and output CSV file path.
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <inputVisioPath> <outputCsvPath>");
            return;
        }

        string visioPath = args[0];
        string csvPath = args[1];

        // Guard: check if the input Visio file exists.
        if (!File.Exists(visioPath))
        {
            Console.Error.WriteLine($"Input file not found: {visioPath}");
            return;
        }

        try
        {
            // Load the Visio diagram.
            Diagram diagram = new Diagram(visioPath);

            // Export the diagram to CSV format.
            diagram.Save(csvPath, SaveFileFormat.Csv);
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during Visio to CSV conversion: {ex.Message}");
            return;
        }

        // Guard: check if the CSV file was created.
        if (!File.Exists(csvPath))
        {
            Console.Error.WriteLine($"CSV file was not created: {csvPath}");
            return;
        }

        // Define the expected column headers for verification.
        // Adjust these headers according to the specific Visio export expectations.
        string[] expectedHeaders = new string[]
        {
            "Shape ID",
            "Shape Name",
            "Shape Type",
            "Text"
        };

        try
        {
            // Read all lines from the CSV file.
            string[] allLines = File.ReadAllLines(csvPath);

            if (allLines.Length == 0)
            {
                Console.Error.WriteLine("CSV file is empty.");
                return;
            }

            // Split the first line (header row) by commas.
            string[] actualHeaders = allLines[0].Split(',');

            // Trim whitespace from each header for robust comparison.
            actualHeaders = actualHeaders.Select(h => h.Trim()).ToArray();

            // Verify that the actual headers match the expected headers.
            bool headersMatch = expectedHeaders.Length == actualHeaders.Length &&
                                expectedHeaders.Zip(actualHeaders, (exp, act) => exp == act).All(equal => equal);

            if (!headersMatch)
            {
                Console.Error.WriteLine("CSV header verification failed.");
                Console.Error.WriteLine("Expected headers:");
                Console.Error.WriteLine(string.Join(", ", expectedHeaders));
                Console.Error.WriteLine("Actual headers:");
                Console.Error.WriteLine(string.Join(", ", actualHeaders));
                // Throwing an exception to indicate failure as per guidelines.
                throw new Exception("CSV headers do not match expected values.");
            }
            else
            {
                Console.WriteLine("CSV header verification succeeded.");
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during CSV verification: {ex.Message}");
        }
    }
}