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

            // Path to the source Visio file (adjust as needed)
            string sourcePath = "input.vsdx";

            // Path for the generated CSV file
            string csvPath = "output.csv";

            // Load the diagram from the file
            Diagram diagram = new Diagram(sourcePath);

            // Export the diagram to CSV format
            diagram.Save(csvPath, SaveFileFormat.Csv);

            // Verify that the CSV contains the required header row
            // Expected header (as defined by Aspose.Diagram CSV export specification)
            const string expectedHeaderStart = "Shape ID";

            // Read the first line of the CSV file
            string firstLine;
            using (var reader = new StreamReader(csvPath))
            {
                firstLine = reader.ReadLine();
            }

            // Perform the validation
            if (firstLine == null)
            {
                throw new Exception("CSV file is empty. Header row is missing.");
            }

            if (!firstLine.StartsWith(expectedHeaderStart, StringComparison.OrdinalIgnoreCase))
            {
                throw new Exception($"CSV header validation failed. Expected header to start with '{expectedHeaderStart}', but got: '{firstLine}'.");
            }

            Console.WriteLine("CSV header validation succeeded.");

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
