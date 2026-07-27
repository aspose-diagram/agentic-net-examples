using System;
using System.IO;
using System.Linq;
using Aspose.Diagram;

class Program
    {
        static void Main(string[] args)
        {
            try
            {

                // Paths to the source Visio diagram and the CSV output
                string diagramPath = "input.vsdx";
                string csvPath = "output.csv";

                // Load the Visio diagram
                Diagram diagram = new Diagram(diagramPath);

                // Export the diagram to CSV format
                diagram.Save(csvPath, SaveFileFormat.Csv);

                // Read all lines from the generated CSV file
                string[] lines = File.ReadAllLines(csvPath);
                if (lines.Length == 0)
                {
                    throw new Exception("The CSV file is empty.");
                }

                // Split the first line to obtain the actual column headers
                string[] actualHeaders = lines[0].Split(',');

                // Define the expected column headers
                string[] expectedHeaders = new[] { "Header1", "Header2", "Header3" };

                // Verify that the actual headers match the expected ones
                bool headersMatch = actualHeaders.Length == expectedHeaders.Length &&
                                    actualHeaders.SequenceEqual(expectedHeaders);

                if (!headersMatch)
                {
                    string expected = string.Join(",", expectedHeaders);
                    string actual = string.Join(",", actualHeaders);
                    throw new Exception($"CSV header verification failed. Expected: {expected}. Actual: {actual}.");
                }

                Console.WriteLine("CSV header verification succeeded.");

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }