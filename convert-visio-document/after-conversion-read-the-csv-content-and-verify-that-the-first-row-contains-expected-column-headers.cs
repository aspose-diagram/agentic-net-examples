using System;
using System.IO;
using System.Linq;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        try
        {

            // Load the Visio diagram (example file)
            // Note: Replace "input.vsdx" with the actual diagram file path.
            Diagram diagram = new Diagram("input.vsdx");

            // Perform conversion to CSV (placeholder - actual conversion logic depends on your scenario)
            // For demonstration, assume the conversion has already produced "output.csv".
            string csvPath = "output.csv";

            // Verify that the CSV file exists
            if (!File.Exists(csvPath))
            {
                Console.WriteLine($"CSV file not found: {csvPath}");
                return;
            }

            // Expected column headers (adjust as needed)
            string[] expectedHeaders = new[] { "Id", "Name", "Value", "Description" };

            // Read the first line of the CSV file
            string firstLine;
            using (var reader = new StreamReader(csvPath))
            {
                firstLine = reader.ReadLine();
            }

            if (firstLine == null)
            {
                Console.WriteLine("CSV file is empty.");
                return;
            }

            // Split the header line by commas (handles simple CSV without quoted commas)
            string[] actualHeaders = firstLine.Split(',');

            // Trim whitespace from each header
            actualHeaders = actualHeaders.Select(h => h.Trim()).ToArray();

            // Compare expected and actual headers
            bool headersMatch = expectedHeaders.SequenceEqual(actualHeaders, StringComparer.OrdinalIgnoreCase);

            if (headersMatch)
            {
                Console.WriteLine("CSV header verification succeeded.");
            }
            else
            {
                Console.WriteLine("CSV header verification failed.");
                Console.WriteLine("Expected: " + string.Join(", ", expectedHeaders));
                Console.WriteLine("Actual:   " + string.Join(", ", actualHeaders));
            }

            // Optionally, save any changes to the diagram (if needed)
            // diagram.Save("modified.vsdx", SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
