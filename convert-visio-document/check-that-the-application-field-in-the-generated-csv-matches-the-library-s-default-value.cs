using Aspose.Diagram;
using System;
using System.IO;

class Program
{
    static void Main()
    {
        // Create a new diagram
        Diagram diagram = new Diagram();

        // Path for the CSV output
        string csvPath = "output.csv";

        // Save the diagram as CSV
        diagram.Save(csvPath, SaveFileFormat.Csv);

        // Read all lines from the generated CSV
        string[] lines = File.ReadAllLines(csvPath);
        if (lines.Length == 0)
        {
            Console.WriteLine("CSV file is empty.");
            return;
        }

        // First line contains headers
        string headerLine = lines[0];
        string[] headers = headerLine.Split(',');

        // Locate the "Application" column index
        int appIndex = Array.IndexOf(headers, "Application");
        if (appIndex == -1)
        {
            Console.WriteLine("Application column not found in CSV.");
            return;
        }

        // Second line contains data (if present)
        string dataLine = lines.Length > 1 ? lines[1] : string.Empty;
        string[] data = dataLine.Split(',');

        // Retrieve the Application field value
        string appValue = appIndex < data.Length ? data[appIndex] : string.Empty;

        // Expected default value for the Application field
        const string defaultAppValue = "Aspose.Diagram";

        // Verify the Application field matches the default
        if (string.Equals(appValue, defaultAppValue, StringComparison.OrdinalIgnoreCase))
        {
            Console.WriteLine("Application field matches the default value.");
        }
        else
        {
            Console.WriteLine($"Application field mismatch. Expected: {defaultAppValue}, Found: {appValue}");
        }
    }
}
