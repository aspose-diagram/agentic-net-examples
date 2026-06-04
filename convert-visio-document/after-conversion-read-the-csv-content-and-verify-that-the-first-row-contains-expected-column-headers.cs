using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
    {
        static void Main()
        {
            try
            {

                // Path to the source Visio diagram
                string diagramPath = "input.vsdx";

                // Load the diagram
                Diagram diagram = new Diagram(diagramPath);

                // Path for the exported CSV file
                string csvPath = "output.csv";

                // Export the diagram to CSV format
                diagram.Save(csvPath, SaveFileFormat.Csv);

                // Read all lines from the generated CSV file
                string[] lines = File.ReadAllLines(csvPath);

                // Ensure the CSV file is not empty
                if (lines.Length == 0)
                {
                    throw new Exception("CSV file is empty.");
                }

                // Split the first line to obtain column headers
                string[] actualHeaders = lines[0].Split(',');

                // Define the expected column headers
                string[] expectedHeaders = new[] { "Header1", "Header2", "Header3" };

                // Verify header count
                if (actualHeaders.Length != expectedHeaders.Length)
                {
                    throw new Exception($"Header count mismatch. Expected {expectedHeaders.Length}, but found {actualHeaders.Length}.");
                }

                // Verify each header value
                for (int i = 0; i < expectedHeaders.Length; i++)
                {
                    if (!string.Equals(actualHeaders[i].Trim(), expectedHeaders[i], StringComparison.OrdinalIgnoreCase))
                    {
                        throw new Exception($"Header mismatch at position {i + 1}. Expected '{expectedHeaders[i]}', but found '{actualHeaders[i].Trim()}'.");
                    }
                }

                // If all checks pass, output success message
                Console.WriteLine("CSV header verification succeeded.");

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }