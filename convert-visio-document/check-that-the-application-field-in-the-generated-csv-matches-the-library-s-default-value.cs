using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        try
        {

            // Path to the source Visio file (replace with actual file path)
            string sourcePath = "input.vsdx";

            // Path for the generated CSV file
            string csvPath = "output.csv";

            // Load the diagram
            Diagram diagram = new Diagram(sourcePath);

            // Export the diagram to CSV using the library's default settings
            diagram.Save(csvPath, SaveFileFormat.Csv);

            // Expected default value for the Application field in CSV export
            const string expectedApplicationValue = "Aspose.Diagram";

            // Read the CSV file and verify the Application column values
            using (StreamReader reader = new StreamReader(csvPath))
            {
                // Read header line
                string headerLine = reader.ReadLine();
                if (headerLine == null)
                    throw new Exception("CSV file is empty.");

                string[] headers = headerLine.Split(',');

                // Find the index of the "Application" column
                int appIndex = -1;
                for (int i = 0; i < headers.Length; i++)
                {
                    if (headers[i].Trim().Equals("Application", StringComparison.OrdinalIgnoreCase))
                    {
                        appIndex = i;
                        break;
                    }
                }

                if (appIndex == -1)
                    throw new Exception("Application column not found in CSV header.");

                // Verify each data row
                string line;
                int rowNumber = 1; // Starting after header
                while ((line = reader.ReadLine()) != null)
                {
                    rowNumber++;
                    string[] fields = line.Split(',');

                    // Guard against malformed rows
                    if (fields.Length <= appIndex)
                        throw new Exception($"Row {rowNumber} does not contain an Application field.");

                    string actualValue = fields[appIndex].Trim();

                    if (!actualValue.Equals(expectedApplicationValue, StringComparison.Ordinal))
                    {
                        throw new Exception($"Row {rowNumber}: Application field value '{actualValue}' does not match expected '{expectedApplicationValue}'.");
                    }
                }
            }

            Console.WriteLine("All Application fields in the generated CSV match the library's default value.");

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
