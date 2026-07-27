using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Define output CSV path
        string csvPath = "output.csv";

        try
        {
            // Create a new diagram with default settings
            Diagram diagram = new Diagram();

            // Add a simple shape to ensure the diagram has content
            // Parameters: PinX, PinY, Master name, Page index
            long shapeId = diagram.AddShape(1.0, 1.0, "Rectangle", 0);

            // Save the diagram as CSV using the correct SaveFileFormat enum member
            diagram.Save(csvPath, SaveFileFormat.Csv);
        }
        catch (Exception ex)
        {
            // Report any Aspose.Diagram errors and exit
            Console.Error.WriteLine($"Aspose.Diagram error: {ex.Message}");
            return;
        }

        // The library's default Application value is empty when not explicitly set
        string defaultApplication = string.Empty;

        // Guard: ensure the generated CSV file exists before reading
        if (!File.Exists(csvPath))
        {
            Console.Error.WriteLine($"File not found: {csvPath}");
            return;
        }

        // Read all lines from the generated CSV file
        string[] csvLines = File.ReadAllLines(csvPath);
        if (csvLines.Length < 2)
        {
            throw new Exception("CSV file does not contain enough data for validation.");
        }

        // The first line contains headers, the second line contains values
        string headerLine = csvLines[0];
        string valueLine = csvLines[1];

        // Split by commas (CSV delimiter)
        string[] headers = headerLine.Split(',');
        string[] values = valueLine.Split(',');

        // Find the index of the "Application" column (case‑insensitive)
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
        {
            throw new Exception("Application column not found in CSV header.");
        }

        if (appIndex >= values.Length)
        {
            throw new Exception("Application value missing in CSV data row.");
        }

        // Retrieve the Application field value from the CSV row
        string csvApplication = values[appIndex].Trim();

        // Validate that the CSV Application field matches the library’s default value
        if (!csvApplication.Equals(defaultApplication, StringComparison.Ordinal))
        {
            throw new Exception($"Application field mismatch. Expected: '{defaultApplication}', Found: '{csvApplication}'.");
        }

        Console.WriteLine("Application field matches the library's default value.");
    }
}