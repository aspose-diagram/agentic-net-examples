using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        try
        {

            // Path to the source Visio diagram
            string inputPath = "input.vsdx";

            // Path where the CSV will be saved
            string csvPath = "output.csv";

            // Load the diagram from file
            Diagram diagram = new Diagram(inputPath);

            // Export the diagram to CSV format
            diagram.Save(csvPath, SaveFileFormat.Csv);

            // Define the expected column headers
            string[] expectedHeaders = new[] { "Column1", "Column2", "Column3" };

            // Read the first line (header row) from the generated CSV file
            using (var reader = new StreamReader(csvPath))
            {
                string headerLine = reader.ReadLine();

                if (headerLine == null)
                    throw new Exception("CSV file is empty.");

                // Split the header line by commas
                string[] actualHeaders = headerLine.Split(',');

                // Verify the number of columns matches
                if (actualHeaders.Length != expectedHeaders.Length)
                    throw new Exception($"Header count mismatch. Expected {expectedHeaders.Length}, got {actualHeaders.Length}.");

                // Verify each header value
                for (int i = 0; i < expectedHeaders.Length; i++)
                {
                    if (!string.Equals(actualHeaders[i].Trim(), expectedHeaders[i], StringComparison.OrdinalIgnoreCase))
                        throw new Exception($"Header mismatch at position {i}. Expected '{expectedHeaders[i]}', got '{actualHeaders[i]}'");
                }

                Console.WriteLine("CSV header verification passed.");
            }

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
